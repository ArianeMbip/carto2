namespace ApiCartobani.Domain.Contacts.DomainEvents;

public sealed class ContactCreated : DomainEvent
{
    public Contact Contact { get; set; } 
}
            