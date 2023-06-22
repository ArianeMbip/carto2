namespace ApiCartobani.SharedTestHelpers.Fakes.Attribut;

using AutoBogus;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeAttributForUpdateDto : AutoFaker<AttributForUpdateDto>
{
    public FakeAttributForUpdateDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(a => a.ExampleIntProperty, a => a.Random.Number(50, 100000));
        //RuleFor(a => a.ExampleDateProperty, a => a.Date.Past());
    }
}