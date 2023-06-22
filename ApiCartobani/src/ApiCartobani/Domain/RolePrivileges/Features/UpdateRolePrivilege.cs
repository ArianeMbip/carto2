namespace ApiCartobani.Domain.RolePrivileges.Features;

using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Domain.RolePrivileges.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateRolePrivilege
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly RolePrivilegeForUpdateDto UpdatedRolePrivilegeData;

        public Command(Guid id, RolePrivilegeForUpdateDto updatedRolePrivilegeData)
        {
            Id = id;
            UpdatedRolePrivilegeData = updatedRolePrivilegeData;
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
            var rolePrivilegeToUpdate = await _rolePrivilegeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            rolePrivilegeToUpdate.Update(request.UpdatedRolePrivilegeData);
            _rolePrivilegeRepository.Update(rolePrivilegeToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}