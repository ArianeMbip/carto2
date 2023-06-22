namespace ApiCartobani.Domain.Contacts.Dtos;

public sealed class ContactDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Entite { get; set; }
        public string Fonction { get; set; }
        public string Telephone { get; set; }
}
