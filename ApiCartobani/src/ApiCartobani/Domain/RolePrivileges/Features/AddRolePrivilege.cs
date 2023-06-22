namespace ApiCartobani.Domain.RolePrivileges.Features;

using ApiCartobani.Domain.RolePrivileges.Services;
using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddRolePrivilege
{
    public sealed class Command : IRequest<RolePrivilegeDto>
    {
        public readonly RolePrivilegeForCreationDto RolePrivilegeToAdd;

        public Command(RolePrivilegeForCreationDto rolePrivilegeToAdd)
        {
            RolePrivilegeToAdd = rolePrivilegeToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, RolePrivilegeDto>
    {
        private readonly IRolePrivilegeRepository _rolePrivilegeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IRolePrivilegeRepository rolePrivilegeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RolePrivilegeDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var rolePrivilege = RolePrivilege.Create(request.RolePrivilegeToAdd);
            await _rolePrivilegeRepository.Add(rolePrivilege, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var rolePrivilegeAdded = await _rolePrivilegeRepository.GetById(rolePrivilege.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RolePrivilegeDto>(rolePrivilegeAdded);
        }
    }
}