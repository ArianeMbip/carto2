namespace ApiCartobani.Domain.TypeElements.Features;

using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Domain.TypeElements.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateTypeElement
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly TypeElementForUpdateDto UpdatedTypeElementData;

        public Command(Guid id, TypeElementForUpdateDto updatedTypeElementData)
        {
            Id = id;
            UpdatedTypeElementData = updatedTypeElementData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly ITypeElementRepository _typeElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITypeElementRepository typeElementRepository, IUnitOfWork unitOfWork)
        {
            _typeElementRepository = typeElementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var typeElementToUpdate = await _typeElementRepository.GetById(request.Id, cancellationToken: cancellationToken);

            typeElementToUpdate.Update(request.UpdatedTypeElementData);
            _typeElementRepository.Update(typeElementToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}