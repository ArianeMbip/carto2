using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.GestionnaireActifs.Dtos;

namespace ApiCartobani.Domain.Actifs.Dtos;

public sealed class ActifDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Criticite { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Icone { get; set; }
        public string Statut { get; set; }
        public Guid TypeActif { get; set; }
        public Guid PreVersion { get; set; }
        public Guid Hierarchie { get; set; }
        public ICollection<ValeurAttributDto> ValeurAttributs { get; set; }
        public ICollection<GestionnaireActifDto> GestionnaireActif { get; set; }

}
