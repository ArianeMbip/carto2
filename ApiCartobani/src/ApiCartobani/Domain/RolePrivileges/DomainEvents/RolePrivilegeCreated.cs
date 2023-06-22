namespace ApiCartobani.Domain.RolePrivileges.DomainEvents;

public sealed class RolePrivilegeCreated : DomainEvent
{
    public RolePrivilege RolePrivilege { get; set; } 
}
            