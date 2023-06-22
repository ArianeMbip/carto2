namespace ApiCartobani.SharedTestHelpers.Fakes.Historique;

using AutoBogus;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.Dtos;

public sealed class FakeHistorique
{
    public static Historique Generate(HistoriqueForCreationDto historiqueForCreationDto)
    {
        return Historique.Create(historiqueForCreationDto);
    }

    public static Historique Generate()
    {
        return Generate(new FakeHistoriqueForCreationDto().Generate());
    }
}