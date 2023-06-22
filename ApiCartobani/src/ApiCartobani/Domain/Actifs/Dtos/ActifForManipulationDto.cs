namespace ApiCartobani.Domain.Actifs.Dtos;

public abstract class ActifForManipulationDto 
{
        public string Nom { get; set; }
        public string Criticite { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Icone { get; set; }
        public string Statut { get; set; }
        public Guid TypeActif { get; set; }
        public Guid PreVersion { get; set; }
        public Guid Hierarchie { get; set; }

}
