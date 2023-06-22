namespace ApiCartobani.Domain.Attributs.Dtos;

public sealed class AttributDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public bool Requis { get; set; }
        public string TypeDonnee { get; set; }
}
