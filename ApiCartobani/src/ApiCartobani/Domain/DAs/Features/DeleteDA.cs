namespace ApiCartobani.Domain.DAs.Features;

using ApiCartobani.Domain.DAs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteDA
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
        private readonly IDARepository _dARepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IDARepository dARepository, IUnitOfWork unitOfWork)
        {
            _dARepository = dARepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _dARepository.GetById(request.Id, cancellationToken: cancellationToken);

            _dARepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}