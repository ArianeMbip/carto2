namespace ApiCartobani.SharedTestHelpers.Fakes.Actif;

using AutoBogus;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeActifForUpdateDto : AutoFaker<ActifForUpdateDto>
{
    public FakeActifForUpdateDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(a => a.ExampleIntProperty, a => a.Random.Number(50, 100000));
        //RuleFor(a => a.ExampleDateProperty, a => a.Date.Past());
        RuleFor(a => a.Criticite, f => f.PickRandom<CriticiteEnum>(CriticiteEnum.List).Name);
        RuleFor(a => a.Statut, f => f.PickRandom<StatutEnum>(StatutEnum.List).Name);
    }
}