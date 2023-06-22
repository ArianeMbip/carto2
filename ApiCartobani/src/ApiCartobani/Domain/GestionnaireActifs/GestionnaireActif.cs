namespace ApiCartobani.Domain.GestionnaireActifs;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using ApiCartobani.Domain.GestionnaireActifs.Validators;
using ApiCartobani.Domain.GestionnaireActifs.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.RolePrivileges;


public class GestionnaireActif : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string CUID { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<RolePrivilege> RolePrivilege { get; private set; }


    public static GestionnaireActif Create(GestionnaireActifForCreationDto gestionnaireActifForCreationDto)
    {
        new GestionnaireActifForCreationDtoValidator().ValidateAndThrow(gestionnaireActifForCreationDto);

        var newGestionnaireActif = new GestionnaireActif();

        newGestionnaireActif.Nom = gestionnaireActifForCreationDto.Nom;
        newGestionnaireActif.CUID = gestionnaireActifForCreationDto.CUID;

        newGestionnaireActif.QueueDomainEvent(new GestionnaireActifCreated(){ GestionnaireActif = newGestionnaireActif });
        
        return newGestionnaireActif;
    }

    public GestionnaireActif Update(GestionnaireActifForUpdateDto gestionnaireActifForUpdateDto)
    {
        new GestionnaireActifForUpdateDtoValidator().ValidateAndThrow(gestionnaireActifForUpdateDto);

        Nom = gestionnaireActifForUpdateDto.Nom;
        CUID = gestionnaireActifForUpdateDto.CUID;

        QueueDomainEvent(new GestionnaireActifUpdated(){ Id = Id });
        return this;
    }
    
    protected GestionnaireActif() { } // For EF + Mocking
}