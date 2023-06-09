namespace ApiCartobani.SharedTestHelpers.Fakes.DA;

using AutoBogus;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeDAForUpdateDto : AutoFaker<DAForUpdateDto>
{
    public FakeDAForUpdateDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(d => d.ExampleIntProperty, d => d.Random.Number(50, 100000));
        //RuleFor(d => d.ExampleDateProperty, d => d.Date.Past());
        RuleFor(d => d.Status, f => f.PickRandom<StatusEnum>(StatusEnum.List).Name);
    }
}