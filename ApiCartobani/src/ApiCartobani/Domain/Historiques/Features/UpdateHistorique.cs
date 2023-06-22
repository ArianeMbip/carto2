namespace ApiCartobani.Domain.Historiques.Features;

using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Domain.Historiques.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateHistorique
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly HistoriqueForUpdateDto UpdatedHistoriqueData;

        public Command(Guid id, HistoriqueForUpdateDto updatedHistoriqueData)
        {
            Id = id;
            UpdatedHistoriqueData = updatedHistoriqueData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IHistoriqueRepository _historiqueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IHistoriqueRepository historiqueRepository, IUnitOfWork unitOfWork)
        {
            _historiqueRepository = historiqueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var historiqueToUpdate = await _historiqueRepository.GetById(request.Id, cancellationToken: cancellationToken);

            historiqueToUpdate.Update(request.UpdatedHistoriqueData);
            _historiqueRepository.Update(historiqueToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}