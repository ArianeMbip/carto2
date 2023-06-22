namespace ApiCartobani.Domain.Actifs.Features;

using ApiCartobani.Domain.Actifs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteActif
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
        private readonly IActifRepository _actifRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IActifRepository actifRepository, IUnitOfWork unitOfWork)
        {
            _actifRepository = actifRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _actifRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _actifRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}