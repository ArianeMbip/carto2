namespace ApiCartobani.Domain.TypeElements.Features;

using ApiCartobani.Domain.TypeElements.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteTypeElement
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
        private readonly ITypeElementRepository _typeElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITypeElementRepository typeElementRepository, IUnitOfWork unitOfWork)
        {
            _typeElementRepository = typeElementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _typeElementRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _typeElementRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}