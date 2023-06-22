using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.Universs;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.Contacts;

namespace ApiCartobani.Domain.DAs.Dtos;


public sealed class DADto 
{
        public Guid Id { get; set; }
        public string Contexte { get; set; }
        public string Objectifs { get; set; }
        public string Status { get; set; }
        public string DomaineFonctionnel { get; set; }
        public string SousDomaineFonctionnel { get; set; }
        public string Fonction{ get; set; }
        public string Acteurs { get; set; }
        public string CasUtilisation { get; set; }
        public string DiagrammeSequence { get; set; }
        public string ArchitectureFonctionnelle { get; set; }
        public string ArchitectureTechnique { get; set; }
        public string ArchitectureApplicative { get; set; }
        public Guid IdActif { get; set; }
        public string ArchitectureDonnee { get; set; }
        public ICollection<Historique> Historiques { get; set; }
        public ICollection<PieceJointe> PieceJointes { get; set; }
        public ICollection<Univers> Univers { get; set; }
        public ICollection<InterfaceUtilisateur> InterfaceUtilisateurs { get; set; }
        public ICollection<Contact> Contacts { get; set; }
}
