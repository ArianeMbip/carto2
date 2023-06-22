namespace ApiCartobani.Domain.Historiques.Dtos;

public abstract class HistoriqueForManipulationDto 
{
        public DateTime DateModification { get; set; }
        public string PartieModifiee { get; set; }
        public string AncienneValeur { get; set; }
        public string NouvelleValeur { get; set; }
        public string NomUtilisateur { get; set; }
        public string CUID { get; set; }
}
