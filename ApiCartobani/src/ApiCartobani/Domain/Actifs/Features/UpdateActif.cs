namespace ApiCartobani.Domain.Actifs.Features;

using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.Actifs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateActif
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ActifForUpdateDto UpdatedActifData;

        public Command(Guid id, ActifForUpdateDto updatedActifData)
        {
            Id = id;
            UpdatedActifData = updatedActifData;
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
            var actifToUpdate = await _actifRepository.GetById(request.Id, cancellationToken: cancellationToken);

            actifToUpdate.Update(request.UpdatedActifData);
            _actifRepository.Update(actifToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}