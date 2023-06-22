namespace ApiCartobani.Domain.Attributs.Features;

using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Attributs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateAttribut
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly AttributForUpdateDto UpdatedAttributData;

        public Command(Guid id, AttributForUpdateDto updatedAttributData)
        {
            Id = id;
            UpdatedAttributData = updatedAttributData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IAttributRepository _attributRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IAttributRepository attributRepository, IUnitOfWork unitOfWork)
        {
            _attributRepository = attributRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var attributToUpdate = await _attributRepository.GetById(request.Id, cancellationToken: cancellationToken);

            attributToUpdate.Update(request.UpdatedAttributData);
            _attributRepository.Update(attributToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}