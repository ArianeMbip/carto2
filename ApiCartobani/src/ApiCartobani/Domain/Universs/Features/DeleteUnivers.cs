namespace ApiCartobani.Domain.Universs.Features;

using ApiCartobani.Domain.Universs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUnivers
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
        private readonly IUniversRepository _universRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUniversRepository universRepository, IUnitOfWork unitOfWork)
        {
            _universRepository = universRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _universRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _universRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}