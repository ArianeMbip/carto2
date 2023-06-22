namespace ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;

using AutoBogus;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public sealed class FakePieceJointeForCreationDto : AutoFaker<PieceJointeForCreationDto>
{
    public FakePieceJointeForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(p => p.ExampleIntProperty, p => p.Random.Number(50, 100000));
        //RuleFor(p => p.ExampleDateProperty, p => p.Date.Past());
    }
}