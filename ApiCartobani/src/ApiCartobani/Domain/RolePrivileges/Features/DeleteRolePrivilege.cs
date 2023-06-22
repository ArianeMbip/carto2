namespace ApiCartobani.Domain.RolePrivileges.Features;

using ApiCartobani.Domain.RolePrivileges.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteRolePrivilege
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
        private readonly IRolePrivilegeRepository _rolePrivilegeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IRolePrivilegeRepository rolePrivilegeRepository, IUnitOfWork unitOfWork)
        {
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _rolePrivilegeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _rolePrivilegeRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}