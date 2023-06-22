namespace ApiCartobani.Domain.Attributs.Features;

using ApiCartobani.Domain.Attributs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteAttribut
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
        private readonly IAttributRepository _attributRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IAttributRepository attributRepository, IUnitOfWork unitOfWork)
        {
            _attributRepository = attributRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _attributRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _attributRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}