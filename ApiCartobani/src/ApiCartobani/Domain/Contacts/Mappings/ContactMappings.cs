namespace ApiCartobani.Domain.Contacts.Mappings;

using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Domain.Contacts;
using Mapster;

public sealed class ContactMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Contact, ContactDto>();
        config.NewConfig<ContactForCreationDto, Contact>()
            .TwoWays();
        config.NewConfig<ContactForUpdateDto, Contact>()
            .TwoWays();
    }
}