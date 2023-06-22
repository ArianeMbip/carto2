using ApiCartobani.Domain.Attributs.Dtos;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ApiCartobani.Domain.TypeElements.Dtos;

public abstract class TypeElementForManipulationDto 
{
        public string Nom { get; set; }
        public string Type { get; set; }
        public string Icone { get; set; }
        public ICollection<AttributDto> Attributs { get; set; }
        

}
