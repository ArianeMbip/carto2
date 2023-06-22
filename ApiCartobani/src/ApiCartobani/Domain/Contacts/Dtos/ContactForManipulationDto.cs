namespace ApiCartobani.Domain.Contacts.Dtos;

public abstract class ContactForManipulationDto 
{
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Entite { get; set; }
        public string Fonction { get; set; }
        public string Telephone { get; set; }
}
