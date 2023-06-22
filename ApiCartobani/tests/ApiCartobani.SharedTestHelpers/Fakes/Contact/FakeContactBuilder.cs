namespace ApiCartobani.SharedTestHelpers.Fakes.Contact;

using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.Dtos;

public class FakeContactBuilder
{
    private ContactForCreationDto _creationData = new FakeContactForCreationDto().Generate();

    public FakeContactBuilder WithDto(ContactForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Contact Build()
    {
        var result = Contact.Create(_creationData);
        return result;
    }
}