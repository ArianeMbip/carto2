namespace ApiCartobani.SharedTestHelpers.Fakes.Flux;

using AutoBogus;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeFluxForUpdateDto : AutoFaker<FluxForUpdateDto>
{
    public FakeFluxForUpdateDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(f => f.ExampleIntProperty, f => f.Random.Number(50, 100000));
        //RuleFor(f => f.ExampleDateProperty, f => f.Date.Past());
    }
}