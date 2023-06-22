namespace ApiCartobani.Domain.ValeurAttributs.Features;

using ApiCartobani.Domain.ValeurAttributs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteValeurAttribut
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
        private readonly IValeurAttributRepository _valeurAttributRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IValeurAttributRepository valeurAttributRepository, IUnitOfWork unitOfWork)
        {
            _valeurAttributRepository = valeurAttributRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _valeurAttributRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _valeurAttributRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}