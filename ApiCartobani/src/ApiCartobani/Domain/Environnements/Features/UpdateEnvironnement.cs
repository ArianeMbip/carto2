namespace ApiCartobani.Domain.Environnements.Features;

using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Domain.Environnements.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateEnvironnement
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly EnvironnementForUpdateDto UpdatedEnvironnementData;

        public Command(Guid id, EnvironnementForUpdateDto updatedEnvironnementData)
        {
            Id = id;
            UpdatedEnvironnementData = updatedEnvironnementData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IEnvironnementRepository _environnementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IEnvironnementRepository environnementRepository, IUnitOfWork unitOfWork)
        {
            _environnementRepository = environnementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var environnementToUpdate = await _environnementRepository.GetById(request.Id, cancellationToken: cancellationToken);

            environnementToUpdate.Update(request.UpdatedEnvironnementData);
            _environnementRepository.Update(environnementToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}