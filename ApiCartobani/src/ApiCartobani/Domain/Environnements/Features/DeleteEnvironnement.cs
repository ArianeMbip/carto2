namespace ApiCartobani.Domain.Environnements.Features;

using ApiCartobani.Domain.Environnements.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteEnvironnement
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
        private readonly IEnvironnementRepository _environnementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IEnvironnementRepository environnementRepository, IUnitOfWork unitOfWork)
        {
            _environnementRepository = environnementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _environnementRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _environnementRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}