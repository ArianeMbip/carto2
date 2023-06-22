using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;

namespace ApiCartobani.Domain.Actifs.Dtos;

public sealed class ActifForCreationDto : ActifForManipulationDto
{
    public ICollection<ValeurAttributDto> ValeurAttributs { get; set; }

}
