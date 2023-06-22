namespace ApiCartobani.Domain.RolePrivileges.Dtos;

public sealed class RolePrivilegeDto 
{
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public bool Lire { get; set; }
        public bool Ecrire { get; set; }
        public bool Modifier { get; set; }
        public bool Supprimer { get; set; }
        public bool Valider { get; set; }
        public bool Archiver { get; set; }
        public bool Generer { get; set; }
}
