namespace ApiCartobani.Domain.Actifs;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.Actifs.Validators;
using ApiCartobani.Domain.Actifs.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.ValeurAttributs;
using MongoFramework;


public class Actif : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    private CriticiteEnum _criticite;
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Criticite
    {
        get => _criticite.Name;
        private set
        {
            if (!CriticiteEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Criticite), value);

            _criticite = parsed;
        }
    }

    [Required]
    public virtual string Description { get; private set; }

    [Required]
    public virtual string Version { get; private set; }

    public virtual string Icone { get; private set; }

    private StatutEnum _statut;
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Statut
    {
        get => _statut.Name;
        private set
        {
            if (!StatutEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Statut), value);

            _statut = parsed;
        }
    }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("TypeElement")]
    public virtual Guid TypeActif { get; private set; }
    public virtual TypeElement TypeElement { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Actif")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid PreVersion { get; private set; }
    public virtual Actif ParentActif { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Actif")]
    public virtual Guid Hierarchie { get; private set; }
    public virtual Actif parentActif { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<GestionnaireActif> GestionnaireActif { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ValeurAttribut> ValeurAttributs { get; private set; }


    public static Actif Create(ActifForCreationDto actifForCreationDto)
    {
        new ActifForCreationDtoValidator().ValidateAndThrow(actifForCreationDto);

        var newActif = new Actif();

        newActif.Nom = actifForCreationDto.Nom;
        newActif.Criticite = actifForCreationDto.Criticite;
        newActif.Description = actifForCreationDto.Description;
        newActif.Version = actifForCreationDto.Version;
        newActif.Icone = actifForCreationDto.Icone;
        newActif.Statut = actifForCreationDto.Statut;
        newActif.TypeActif = actifForCreationDto.TypeActif;
        newActif.PreVersion = actifForCreationDto.PreVersion;
        newActif.Hierarchie = actifForCreationDto.Hierarchie;

        newActif.QueueDomainEvent(new ActifCreated(){ Actif = newActif });
        
        return newActif;
    }

    public Actif Update(ActifForUpdateDto actifForUpdateDto)
    {
        new ActifForUpdateDtoValidator().ValidateAndThrow(actifForUpdateDto);

        Nom = actifForUpdateDto.Nom;
        Criticite = actifForUpdateDto.Criticite;
        Description = actifForUpdateDto.Description;
        Version = actifForUpdateDto.Version;
        Icone = actifForUpdateDto.Icone;
        Statut = actifForUpdateDto.Statut;
        TypeActif = actifForUpdateDto.TypeActif;
        PreVersion = actifForUpdateDto.PreVersion;
        Hierarchie = actifForUpdateDto.Hierarchie;

        QueueDomainEvent(new ActifUpdated(){ Id = Id });
        return this;
    }
    protected Actif() { } // For EF + Mocking
}