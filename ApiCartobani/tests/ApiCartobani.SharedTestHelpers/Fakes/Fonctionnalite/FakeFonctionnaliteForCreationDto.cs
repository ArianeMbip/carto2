namespace ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;

using AutoBogus;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeFonctionnaliteForCreationDto : AutoFaker<FonctionnaliteForCreationDto>
{
    public FakeFonctionnaliteForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(f => f.ExampleIntProperty, f => f.Random.Number(50, 100000));
        //RuleFor(f => f.ExampleDateProperty, f => f.Date.Past());
        RuleFor(f => f.Type, f => f.PickRandom<TypeEnum>(TypeEnum.List).Name);
    }
}