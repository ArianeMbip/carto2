namespace ApiCartobani.Domain.Flux;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Domain.Flux.Validators;
using ApiCartobani.Domain.Flux.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.ValeurAttributs;


public class Flux : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Actif")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid Entree { get; private set; }
    public virtual Actif Actif { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Actif")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid Sortie { get; private set; }
    public virtual Actif actif { get; private set; }

    [Required]
    public virtual string Description { get; private set; }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("TypeElement")]
    public virtual Guid TypeFlux { get; private set; }
    public virtual TypeElement TypeElement { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ValeurAttribut> ValeurAttribut { get; private set; }


    public static Flux Create(FluxForCreationDto fluxForCreationDto)
    {
        new FluxForCreationDtoValidator().ValidateAndThrow(fluxForCreationDto);

        var newFlux = new Flux();

        newFlux.Nom = fluxForCreationDto.Nom;
        newFlux.Entree = fluxForCreationDto.Entree;
        newFlux.Sortie = fluxForCreationDto.Sortie;
        newFlux.Description = fluxForCreationDto.Description;
        newFlux.TypeFlux = fluxForCreationDto.TypeFlux;

        newFlux.QueueDomainEvent(new FluxCreated(){ Flux = newFlux });
        
        return newFlux;
    }

    public Flux Update(FluxForUpdateDto fluxForUpdateDto)
    {
        new FluxForUpdateDtoValidator().ValidateAndThrow(fluxForUpdateDto);

        Nom = fluxForUpdateDto.Nom;
        Entree = fluxForUpdateDto.Entree;
        Sortie = fluxForUpdateDto.Sortie;
        Description = fluxForUpdateDto.Description;
        TypeFlux = fluxForUpdateDto.TypeFlux;

        QueueDomainEvent(new FluxUpdated(){ Id = Id });
        return this;
    }
    
    protected Flux() { } // For EF + Mocking
}