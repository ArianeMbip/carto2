namespace ApiCartobani.SharedTestHelpers.Fakes.Icone;

using AutoBogus;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeIconeForUpdateDto : AutoFaker<IconeForUpdateDto>
{
    public FakeIconeForUpdateDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(i => i.ExampleIntProperty, i => i.Random.Number(50, 100000));
        //RuleFor(i => i.ExampleDateProperty, i => i.Date.Past());
    }
}