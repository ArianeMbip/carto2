namespace ApiCartobani.Domain.DAs.Features;

using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Domain.DAs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateDA
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly DAForUpdateDto UpdatedDAData;

        public Command(Guid id, DAForUpdateDto updatedDAData)
        {
            Id = id;
            UpdatedDAData = updatedDAData;
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
            var dAToUpdate = await _dARepository.GetById(request.Id, cancellationToken: cancellationToken);

            dAToUpdate.Update(request.UpdatedDAData);
            _dARepository.Update(dAToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}