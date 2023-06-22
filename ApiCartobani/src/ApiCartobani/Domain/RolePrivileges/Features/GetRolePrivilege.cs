namespace ApiCartobani.Domain.RolePrivileges.Features;

using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Domain.RolePrivileges.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetRolePrivilege
{
    public sealed class Query : IRequest<RolePrivilegeDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, RolePrivilegeDto>
    {
        private readonly IRolePrivilegeRepository _rolePrivilegeRepository;
        private readonly IMapper _mapper;

        public Handler(IRolePrivilegeRepository rolePrivilegeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
        }

        public async Task<RolePrivilegeDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _rolePrivilegeRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RolePrivilegeDto>(result);
        }
    }
}