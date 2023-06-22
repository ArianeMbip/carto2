namespace ApiCartobani.Domain.Composants;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Domain.Composants.Validators;
using ApiCartobani.Domain.Composants.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.ValeurAttributs;


public class Composant : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("TypeElement")]
    public virtual Guid TypeComposant { get; private set; }
    public virtual TypeElement TypeElement { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<ValeurAttribut> ValeurAttribut { get; private set; }


    public static Composant Create(ComposantForCreationDto composantForCreationDto)
    {
        new ComposantForCreationDtoValidator().ValidateAndThrow(composantForCreationDto);

        var newComposant = new Composant();

        newComposant.Nom = composantForCreationDto.Nom;
        newComposant.TypeComposant = composantForCreationDto.TypeComposant;

        newComposant.QueueDomainEvent(new ComposantCreated(){ Composant = newComposant });
        
        return newComposant;
    }

    public Composant Update(ComposantForUpdateDto composantForUpdateDto)
    {
        new ComposantForUpdateDtoValidator().ValidateAndThrow(composantForUpdateDto);

        Nom = composantForUpdateDto.Nom;
        TypeComposant = composantForUpdateDto.TypeComposant;

        QueueDomainEvent(new ComposantUpdated(){ Id = Id });
        return this;
    }
    
    protected Composant() { } // For EF + Mocking
}