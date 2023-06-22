namespace ApiCartobani.Domain.ValeurAttributs.Features;

using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.ValeurAttributs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateValeurAttribut
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ValeurAttributForUpdateDto UpdatedValeurAttributData;

        public Command(Guid id, ValeurAttributForUpdateDto updatedValeurAttributData)
        {
            Id = id;
            UpdatedValeurAttributData = updatedValeurAttributData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IValeurAttributRepository _valeurAttributRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IValeurAttributRepository valeurAttributRepository, IUnitOfWork unitOfWork)
        {
            _valeurAttributRepository = valeurAttributRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var valeurAttributToUpdate = await _valeurAttributRepository.GetById(request.Id, cancellationToken: cancellationToken);

            valeurAttributToUpdate.Update(request.UpdatedValeurAttributData);
            _valeurAttributRepository.Update(valeurAttributToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}