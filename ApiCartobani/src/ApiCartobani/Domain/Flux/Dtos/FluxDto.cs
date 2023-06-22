namespace ApiCartobani.Domain.Flux.Dtos;

public sealed class FluxDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public Guid Entree { get; set; }
        public Guid Sortie { get; set; }
        public string Description { get; set; }
        public Guid TypeFlux { get; set; }

}
