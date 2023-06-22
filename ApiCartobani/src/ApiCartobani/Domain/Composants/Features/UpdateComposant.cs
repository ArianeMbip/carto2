namespace ApiCartobani.Domain.Composants.Features;

using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Domain.Composants.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateComposant
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ComposantForUpdateDto UpdatedComposantData;

        public Command(Guid id, ComposantForUpdateDto updatedComposantData)
        {
            Id = id;
            UpdatedComposantData = updatedComposantData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IComposantRepository _composantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IComposantRepository composantRepository, IUnitOfWork unitOfWork)
        {
            _composantRepository = composantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var composantToUpdate = await _composantRepository.GetById(request.Id, cancellationToken: cancellationToken);

            composantToUpdate.Update(request.UpdatedComposantData);
            _composantRepository.Update(composantToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}