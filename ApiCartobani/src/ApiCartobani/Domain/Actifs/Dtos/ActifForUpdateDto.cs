namespace ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using ApiCartobani.Domain.ValeurAttributs;

public sealed class ActifForUpdateDto : ActifForManipulationDto
{
    public ICollection<GestionnaireActifDto> GestionnaireActif { get; set; }

}
