namespace ApiCartobani.Domain.Fonctionnalites.Features;

using ApiCartobani.Domain.Fonctionnalites.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteFonctionnalite
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
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
            var recordToDelete = await _fonctionnaliteRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _fonctionnaliteRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}