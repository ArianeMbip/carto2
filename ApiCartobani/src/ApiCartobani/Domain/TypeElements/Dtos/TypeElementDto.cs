using ApiCartobani.Domain.Attributs.Dtos;

namespace ApiCartobani.Domain.TypeElements.Dtos;

public sealed class TypeElementDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        public string Icone { get; set; }
        public ICollection<AttributDto> Attributs { get; set; }

}
