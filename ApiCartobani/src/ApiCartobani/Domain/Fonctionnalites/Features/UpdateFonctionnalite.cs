namespace ApiCartobani.Domain.Fonctionnalites.Features;

using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Domain.Fonctionnalites.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateFonctionnalite
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly FonctionnaliteForUpdateDto UpdatedFonctionnaliteData;

        public Command(Guid id, FonctionnaliteForUpdateDto updatedFonctionnaliteData)
        {
            Id = id;
            UpdatedFonctionnaliteData = updatedFonctionnaliteData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IFonctionnaliteRepository _fonctionnaliteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFonctionnaliteRepository fonctionnaliteRepository, IUnitOfWork unitOfWork)
        {
            _fonctionnaliteRepository = fonctionnaliteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var fonctionnaliteToUpdate = await _fonctionnaliteRepository.GetById(request.Id, cancellationToken: cancellationToken);

            fonctionnaliteToUpdate.Update(request.UpdatedFonctionnaliteData);
            _fonctionnaliteRepository.Update(fonctionnaliteToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}