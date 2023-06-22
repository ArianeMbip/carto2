namespace ApiCartobani.Domain.ValeurAttributs.Dtos;

public abstract class ValeurAttributForManipulationDto 
{
        public string Valeur { get; set; }
        public Guid Attribut { get; set; }
        public Guid Environnement { get; set; }
}
