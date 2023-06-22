namespace ApiCartobani.SharedTestHelpers.Fakes.Composant;

using AutoBogus;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.Dtos;

public sealed class FakeComposant
{
    public static Composant Generate(ComposantForCreationDto composantForCreationDto)
    {
        return Composant.Create(composantForCreationDto);
    }

    public static Composant Generate()
    {
        return Generate(new FakeComposantForCreationDto().Generate());
    }
}