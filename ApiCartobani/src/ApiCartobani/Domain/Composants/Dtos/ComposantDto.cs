namespace ApiCartobani.Domain.Composants.Dtos;

public sealed class ComposantDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public Guid TypeComposant { get; set; }

}
