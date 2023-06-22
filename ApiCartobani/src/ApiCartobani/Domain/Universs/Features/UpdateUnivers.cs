namespace ApiCartobani.Domain.Universs.Features;

using ApiCartobani.Domain.Universs;
using ApiCartobani.Domain.Universs.Dtos;
using ApiCartobani.Domain.Universs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateUnivers
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly UniversForUpdateDto UpdatedUniversData;

        public Command(Guid id, UniversForUpdateDto updatedUniversData)
        {
            Id = id;
            UpdatedUniversData = updatedUniversData;
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
            var universToUpdate = await _universRepository.GetById(request.Id, cancellationToken: cancellationToken);

            universToUpdate.Update(request.UpdatedUniversData);
            _universRepository.Update(universToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}