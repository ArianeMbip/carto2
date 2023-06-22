namespace ApiCartobani.Domain.RolePrivileges.DomainEvents;

public sealed class RolePrivilegeUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            