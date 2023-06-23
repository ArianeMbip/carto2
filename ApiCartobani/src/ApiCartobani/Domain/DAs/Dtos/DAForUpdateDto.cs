using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.Universs;

namespace ApiCartobani.Domain.DAs.Dtos;

public sealed class DAForUpdateDto : DAForManipulationDto
{
   public string Status { get; set; }


}
