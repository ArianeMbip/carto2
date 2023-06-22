namespace ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;

using AutoBogus;
using ApiCartobani.Domain.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakeGestionnaireActifForCreationDto : AutoFaker<GestionnaireActifForCreationDto>
{
    public FakeGestionnaireActifForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(G => G.ExampleIntProperty, G => G.Random.Number(50, 100000));
        //RuleFor(G => G.ExampleDateProperty, G => G.Date.Past());
    }
}