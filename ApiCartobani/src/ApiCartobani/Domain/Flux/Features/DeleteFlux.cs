namespace ApiCartobani.Domain.Flux.Features;

using ApiCartobani.Domain.Flux.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteFlux
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
        private readonly IFluxRepository _fluxRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFluxRepository fluxRepository, IUnitOfWork unitOfWork)
        {
            _fluxRepository = fluxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _fluxRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _fluxRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}