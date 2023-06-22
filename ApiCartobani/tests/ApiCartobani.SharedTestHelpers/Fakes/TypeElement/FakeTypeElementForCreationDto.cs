namespace ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

using AutoBogus;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeTypeElementForCreationDto : AutoFaker<TypeElementForCreationDto>
{
    public FakeTypeElementForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(t => t.ExampleIntProperty, t => t.Random.Number(50, 100000));
        //RuleFor(t => t.ExampleDateProperty, t => t.Date.Past());
        RuleFor(t => t.Type, f => f.PickRandom<TypeEltEnum>(TypeEltEnum.List).Name);
    }
}