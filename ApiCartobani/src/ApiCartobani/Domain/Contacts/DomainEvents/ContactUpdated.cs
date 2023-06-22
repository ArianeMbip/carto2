namespace ApiCartobani.Domain.Contacts.DomainEvents;

public sealed class ContactUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            