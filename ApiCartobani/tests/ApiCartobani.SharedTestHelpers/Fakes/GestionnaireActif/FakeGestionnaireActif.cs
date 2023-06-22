namespace ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;

using AutoBogus;
using ApiCartobani.Domain.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Dtos;

public sealed class FakeGestionnaireActif
{
    public static GestionnaireActif Generate(GestionnaireActifForCreationDto gestionnaireActifForCreationDto)
    {
        return GestionnaireActif.Create(gestionnaireActifForCreationDto);
    }

    public static GestionnaireActif Generate()
    {
        return Generate(new FakeGestionnaireActifForCreationDto().Generate());
    }
}