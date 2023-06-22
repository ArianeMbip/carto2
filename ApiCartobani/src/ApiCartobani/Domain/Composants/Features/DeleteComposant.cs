namespace ApiCartobani.Domain.Composants.Features;

using ApiCartobani.Domain.Composants.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteComposant
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
        private readonly IComposantRepository _composantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IComposantRepository composantRepository, IUnitOfWork unitOfWork)
        {
            _composantRepository = composantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _composantRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _composantRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}