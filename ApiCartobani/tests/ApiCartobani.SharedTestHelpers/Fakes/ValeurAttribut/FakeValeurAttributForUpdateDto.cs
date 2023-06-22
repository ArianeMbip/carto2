namespace ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;

using AutoBogus;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeValeurAttributForUpdateDto : AutoFaker<ValeurAttributForUpdateDto>
{
    public FakeValeurAttributForUpdateDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(v => v.ExampleIntProperty, v => v.Random.Number(50, 100000));
        //RuleFor(v => v.ExampleDateProperty, v => v.Date.Past());
    }
}