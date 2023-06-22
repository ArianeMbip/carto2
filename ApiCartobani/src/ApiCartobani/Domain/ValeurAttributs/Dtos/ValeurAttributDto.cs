using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Environnements.Dtos;

namespace ApiCartobani.Domain.ValeurAttributs.Dtos;

public sealed class ValeurAttributDto
{
    public Guid Id { get; set; }
    public string Valeur { get; set; }
    //public Guid Attribut { get; set; }
    public AttributDto Attribut { get; set; }
    //public Guid Environnement { get; set; }
    public EnvironnementDto Environnement{ get; set; }
}
