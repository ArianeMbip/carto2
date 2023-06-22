namespace ApiCartobani.Domain.Attributs.Dtos;

public abstract class AttributForManipulationDto 
{
    public string Nom { get; set; }
    public bool Requis { get; set; }
    public string TypeDonnee { get; set; }
}
