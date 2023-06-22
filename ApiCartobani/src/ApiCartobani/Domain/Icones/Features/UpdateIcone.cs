namespace ApiCartobani.Domain.Icones.Features;

using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Domain.Icones.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateIcone
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly IconeForUpdateDto UpdatedIconeData;

        public Command(Guid id, IconeForUpdateDto updatedIconeData)
        {
            Id = id;
            UpdatedIconeData = updatedIconeData;
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
            var iconeToUpdate = await _iconeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            iconeToUpdate.Update(request.UpdatedIconeData);
            _iconeRepository.Update(iconeToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}