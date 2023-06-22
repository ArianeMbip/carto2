namespace ApiCartobani.SharedTestHelpers.Fakes.Historique;

using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.Dtos;

public class FakeHistoriqueBuilder
{
    private HistoriqueForCreationDto _creationData = new FakeHistoriqueForCreationDto().Generate();

    public FakeHistoriqueBuilder WithDto(HistoriqueForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Historique Build()
    {
        var result = Historique.Create(_creationData);
        return result;
    }
}