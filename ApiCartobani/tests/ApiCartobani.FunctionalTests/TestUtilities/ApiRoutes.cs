namespace ApiCartobani.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class RolePrivileges
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/rolePrivileges";
        public const string GetRecord = $"{Base}/rolePrivileges/{Id}";
        public const string Create = $"{Base}/rolePrivileges";
        public const string Delete = $"{Base}/rolePrivileges/{Id}";
        public const string Put = $"{Base}/rolePrivileges/{Id}";
        public const string CreateBatch = $"{Base}/rolePrivileges/batch";
    }

    public static class GestionnaireActif
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/gestionnaireActif";
        public const string GetRecord = $"{Base}/gestionnaireActif/{Id}";
        public const string Create = $"{Base}/gestionnaireActif";
        public const string Delete = $"{Base}/gestionnaireActif/{Id}";
        public const string Put = $"{Base}/gestionnaireActif/{Id}";
        public const string CreateBatch = $"{Base}/gestionnaireActif/batch";
    }

    public static class Environnements
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/environnements";
        public const string GetRecord = $"{Base}/environnements/{Id}";
        public const string Create = $"{Base}/environnements";
        public const string Delete = $"{Base}/environnements/{Id}";
        public const string Put = $"{Base}/environnements/{Id}";
        public const string CreateBatch = $"{Base}/environnements/batch";
    }

    public static class Historiques
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/historiques";
        public const string GetRecord = $"{Base}/historiques/{Id}";
        public const string Create = $"{Base}/historiques";
        public const string Delete = $"{Base}/historiques/{Id}";
        public const string Put = $"{Base}/historiques/{Id}";
        public const string CreateBatch = $"{Base}/historiques/batch";
    }

    public static class PiecesJointes
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/piecesJointes";
        public const string GetRecord = $"{Base}/piecesJointes/{Id}";
        public const string Create = $"{Base}/piecesJointes";
        public const string Delete = $"{Base}/piecesJointes/{Id}";
        public const string Put = $"{Base}/piecesJointes/{Id}";
        public const string CreateBatch = $"{Base}/piecesJointes/batch";
    }

    public static class Contacts
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/contacts";
        public const string GetRecord = $"{Base}/contacts/{Id}";
        public const string Create = $"{Base}/contacts";
        public const string Delete = $"{Base}/contacts/{Id}";
        public const string Put = $"{Base}/contacts/{Id}";
        public const string CreateBatch = $"{Base}/contacts/batch";
    }

    public static class Fonctionnalites
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/fonctionnalites";
        public const string GetRecord = $"{Base}/fonctionnalites/{Id}";
        public const string Create = $"{Base}/fonctionnalites";
        public const string Delete = $"{Base}/fonctionnalites/{Id}";
        public const string Put = $"{Base}/fonctionnalites/{Id}";
        public const string CreateBatch = $"{Base}/fonctionnalites/batch";
    }

    public static class InterfacesUtilisateur
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/interfacesUtilisateur";
        public const string GetRecord = $"{Base}/interfacesUtilisateur/{Id}";
        public const string Create = $"{Base}/interfacesUtilisateur";
        public const string Delete = $"{Base}/interfacesUtilisateur/{Id}";
        public const string Put = $"{Base}/interfacesUtilisateur/{Id}";
        public const string CreateBatch = $"{Base}/interfacesUtilisateur/batch";
    }

    public static class Composants
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/composants";
        public const string GetRecord = $"{Base}/composants/{Id}";
        public const string Create = $"{Base}/composants";
        public const string Delete = $"{Base}/composants/{Id}";
        public const string Put = $"{Base}/composants/{Id}";
        public const string CreateBatch = $"{Base}/composants/batch";
    }

    public static class Flux
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/flux";
        public const string GetRecord = $"{Base}/flux/{Id}";
        public const string Create = $"{Base}/flux";
        public const string Delete = $"{Base}/flux/{Id}";
        public const string Put = $"{Base}/flux/{Id}";
        public const string CreateBatch = $"{Base}/flux/batch";
    }

    public static class Univers
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/univers";
        public const string GetRecord = $"{Base}/univers/{Id}";
        public const string Create = $"{Base}/univers";
        public const string Delete = $"{Base}/univers/{Id}";
        public const string Put = $"{Base}/univers/{Id}";
        public const string CreateBatch = $"{Base}/univers/batch";
    }

    public static class Icones
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/icones";
        public const string GetRecord = $"{Base}/icones/{Id}";
        public const string Create = $"{Base}/icones";
        public const string Delete = $"{Base}/icones/{Id}";
        public const string Put = $"{Base}/icones/{Id}";
        public const string CreateBatch = $"{Base}/icones/batch";
    }

    public static class DAs
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/dAs";
        public const string GetRecord = $"{Base}/dAs/{Id}";
        public const string Create = $"{Base}/dAs";
        public const string Delete = $"{Base}/dAs/{Id}";
        public const string Put = $"{Base}/dAs/{Id}";
        public const string CreateBatch = $"{Base}/dAs/batch";
    }

    public static class Actifs
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/actifs";
        public const string GetRecord = $"{Base}/actifs/{Id}";
        public const string Create = $"{Base}/actifs";
        public const string Delete = $"{Base}/actifs/{Id}";
        public const string Put = $"{Base}/actifs/{Id}";
        public const string CreateBatch = $"{Base}/actifs/batch";
    }

    public static class ValeurAttributs
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/valeurAttributs";
        public const string GetRecord = $"{Base}/valeurAttributs/{Id}";
        public const string Create = $"{Base}/valeurAttributs";
        public const string Delete = $"{Base}/valeurAttributs/{Id}";
        public const string Put = $"{Base}/valeurAttributs/{Id}";
        public const string CreateBatch = $"{Base}/valeurAttributs/batch";
    }

    public static class Attributs
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/attributs";
        public const string GetRecord = $"{Base}/attributs/{Id}";
        public const string Create = $"{Base}/attributs";
        public const string Delete = $"{Base}/attributs/{Id}";
        public const string Put = $"{Base}/attributs/{Id}";
        public const string CreateBatch = $"{Base}/attributs/batch";
    }

    public static class TypeElements
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/typeElements";
        public const string GetRecord = $"{Base}/typeElements/{Id}";
        public const string Create = $"{Base}/typeElements";
        public const string Delete = $"{Base}/typeElements/{Id}";
        public const string Put = $"{Base}/typeElements/{Id}";
        public const string CreateBatch = $"{Base}/typeElements/batch";
    }

    public static class Users
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/users";
        public const string GetRecord = $"{Base}/users/{Id}";
        public const string Create = $"{Base}/users";
        public const string Delete = $"{Base}/users/{Id}";
        public const string Put = $"{Base}/users/{Id}";
        public const string CreateBatch = $"{Base}/users/batch";
        public const string AddRole = $"{Base}/users/{Id}/addRole";
        public const string RemoveRole = $"{Base}/users/{Id}/removeRole";
    }

    public static class RolePermissions
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/rolePermissions";
        public const string GetRecord = $"{Base}/rolePermissions/{Id}";
        public const string Create = $"{Base}/rolePermissions";
        public const string Delete = $"{Base}/rolePermissions/{Id}";
        public const string Put = $"{Base}/rolePermissions/{Id}";
        public const string CreateBatch = $"{Base}/rolePermissions/batch";
    }
}
