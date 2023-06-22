namespace ApiCartobani.SharedTestHelpers.Fakes.Contact;

using AutoBogus;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.Dtos;

public sealed class FakeContact
{
    public static Contact Generate(ContactForCreationDto contactForCreationDto)
    {
        return Contact.Create(contactForCreationDto);
    }

    public static Contact Generate()
    {
        return Generate(new FakeContactForCreationDto().Generate());
    }
}