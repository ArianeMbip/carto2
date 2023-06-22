using ApiCartobani.Domain.ValeurAttributs.Dtos;

namespace ApiCartobani.Domain.Flux.Dtos;

public abstract class FluxForManipulationDto 
{
        public string Nom { get; set; }
        public Guid Entree { get; set; }
        public Guid Sortie { get; set; }
        public string Description { get; set; }
        public Guid TypeFlux { get; set; }
        public ICollection<ValeurAttributDto> ValeurAttributs { get; set; }

}
