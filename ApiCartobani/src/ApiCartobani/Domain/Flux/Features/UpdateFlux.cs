namespace ApiCartobani.Domain.Flux.Features;

using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Domain.Flux.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateFlux
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly FluxForUpdateDto UpdatedFluxData;

        public Command(Guid id, FluxForUpdateDto updatedFluxData)
        {
            Id = id;
            UpdatedFluxData = updatedFluxData;
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
            var fluxToUpdate = await _fluxRepository.GetById(request.Id, cancellationToken: cancellationToken);

            fluxToUpdate.Update(request.UpdatedFluxData);
            _fluxRepository.Update(fluxToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}