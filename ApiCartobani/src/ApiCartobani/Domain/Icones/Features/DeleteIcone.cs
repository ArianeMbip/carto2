namespace ApiCartobani.Domain.Icones.Features;

using ApiCartobani.Domain.Icones.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteIcone
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
        private readonly IIconeRepository _iconeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IIconeRepository iconeRepository, IUnitOfWork unitOfWork)
        {
            _iconeRepository = iconeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _iconeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _iconeRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}