namespace ApiCartobani.SharedTestHelpers.Fakes.Contact;

using AutoBogus;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeContactForCreationDto : AutoFaker<ContactForCreationDto>
{
    public FakeContactForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(c => c.ExampleIntProperty, c => c.Random.Number(50, 100000));
        //RuleFor(c => c.ExampleDateProperty, c => c.Date.Past());
    }
}