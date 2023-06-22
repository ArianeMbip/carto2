namespace ApiCartobani.SharedTestHelpers.Fakes.Environnement;

using AutoBogus;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.Dtos;

public sealed class FakeEnvironnement
{
    public static Environnement Generate(EnvironnementForCreationDto environnementForCreationDto)
    {
        return Environnement.Create(environnementForCreationDto);
    }

    public static Environnement Generate()
    {
        return Generate(new FakeEnvironnementForCreationDto().Generate());
    }
}