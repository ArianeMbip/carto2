namespace ApiCartobani.Domain.DAs;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Domain.DAs.Validators;
using ApiCartobani.Domain.DAs.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.Universs;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.PiecesJointes;


public class DA : BaseEntity
{
    [Required]
    [Column("Name")]
    public virtual string Contexte { get; private set; }

    [Required]
    public virtual string Objectifs { get; private set; }

    private StatusEnum _status;
    [Required]
    public virtual string Status
    {
        get => _status.Name;
        private set
        {
            if (!StatusEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Status), value);

            _status = parsed;
        }
    }

    public virtual string DomaineFonctionnel { get; private set; }

    public virtual string SousDomaineFonctionnel { get; private set; }

    public virtual string Fonction { get; private set; }

    [Required]
    public virtual string Acteurs { get; private set; }

    [Required]
    public virtual string CasUtilisation { get; private set; }

    [Required]
    public virtual string DiagrammeSequence { get; private set; }

    [Required]
    public virtual string ArchitectureFonctionnelle { get; private set; }

    [Required]
    public virtual string ArchitectureTechnique { get; private set; }

    [Required]
    public virtual string ArchitectureApplicative { get; private set; }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Actif")]
    public virtual Guid IdActif { get; private set; }
    public virtual Actif Actif { get; private set; }

    [Required]
    public virtual string ArchitectureDonnee { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<Historique> Historiques { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<PieceJointe> PieceJointes { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<Univers> Univers { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<InterfaceUtilisateur> InterfaceUtilisateurs { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<Contact> Contacts { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<Composant> Composants { get; private set; }

   


    public static DA Create(DAForCreationDto dAForCreationDto)
    {
        new DAForCreationDtoValidator().ValidateAndThrow(dAForCreationDto);

        var newDA = new DA();

        //newDA.Contexte = dAForCreationDto.Contexte;
        //newDA.Objectifs = dAForCreationDto.Objectifs;
        //newDA.Status = dAForCreationDto.Status;
        //newDA.DomaineFonctionnel = dAForCreationDto.DomaineFonctionnel;
        //newDA.SousDomaineFonctionnel = dAForCreationDto.SousDomaineFonctionnel;
        //newDA.Fonction = dAForCreationDto.Fonction                    ;
        //newDA.Acteurs = dAForCreationDto.Acteurs;
        //newDA.CasUtilisation = dAForCreationDto.CasUtilisation;
        //newDA.DiagrammeSequence = dAForCreationDto.DiagrammeSequence;
        //newDA.ArchitectureFonctionnelle = dAForCreationDto.ArchitectureFonctionnelle;
        //newDA.ArchitectureTechnique = dAForCreationDto.ArchitectureTechnique;
        //newDA.ArchitectureApplicative = dAForCreationDto.ArchitectureApplicative;
        ////newDA.IdActif = dAForCreationDto.IdActif;
        //newDA.ArchitectureDonnee = dAForCreationDto.ArchitectureDonnee;
       

        newDA.QueueDomainEvent(new DACreated(){ DA = newDA });
        
        return newDA;
    }

    public DA Update(DAForUpdateDto dAForUpdateDto)
    {
        new DAForUpdateDtoValidator().ValidateAndThrow(dAForUpdateDto);

        Contexte = dAForUpdateDto.Contexte;
        Objectifs = dAForUpdateDto.Objectifs;
        Status = dAForUpdateDto.Status;
        DomaineFonctionnel = dAForUpdateDto.DomaineFonctionnel;
        SousDomaineFonctionnel = dAForUpdateDto.SousDomaineFonctionnel;
        Fonction = dAForUpdateDto.Fonction                    ;
        Acteurs = dAForUpdateDto.Acteurs;
        CasUtilisation = dAForUpdateDto.CasUtilisation;
        DiagrammeSequence = dAForUpdateDto.DiagrammeSequence;
        ArchitectureFonctionnelle = dAForUpdateDto.ArchitectureFonctionnelle;
        ArchitectureTechnique = dAForUpdateDto.ArchitectureTechnique;
        ArchitectureApplicative = dAForUpdateDto.ArchitectureApplicative;
        //IdActif = dAForUpdateDto.IdActif;
        ArchitectureDonnee = dAForUpdateDto.ArchitectureDonnee;
       

        QueueDomainEvent(new DAUpdated(){ Id = Id });
        return this;
    }
    
    protected DA() { } // For EF + Mocking
}