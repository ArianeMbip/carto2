namespace ApiCartobani.Domain.ValeurAttributs;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.ValeurAttributs.Validators;
using ApiCartobani.Domain.ValeurAttributs.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Environnements;


public class ValeurAttribut : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Name")]
    public virtual string Valeur { get; private set; }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Attribut")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid Attribut { get; private set; }
    public virtual Attribut attribut { get; private set; }

    [Required]
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Environnement")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid Environnement { get; private set; }
    public virtual Environnement environnement { get; private set; }


    public static ValeurAttribut Create(ValeurAttributForCreationDto valeurAttributForCreationDto)
    {
        new ValeurAttributForCreationDtoValidator().ValidateAndThrow(valeurAttributForCreationDto);

        var newValeurAttribut = new ValeurAttribut();

        newValeurAttribut.Valeur = valeurAttributForCreationDto.Valeur;
        newValeurAttribut.Attribut = valeurAttributForCreationDto.Attribut;
        newValeurAttribut.Environnement = valeurAttributForCreationDto.Environnement;

        newValeurAttribut.QueueDomainEvent(new ValeurAttributCreated(){ ValeurAttribut = newValeurAttribut });
        
        return newValeurAttribut;
    }

    public ValeurAttribut Update(ValeurAttributForUpdateDto valeurAttributForUpdateDto)
    {
        new ValeurAttributForUpdateDtoValidator().ValidateAndThrow(valeurAttributForUpdateDto);

        Valeur = valeurAttributForUpdateDto.Valeur;
        Attribut = valeurAttributForUpdateDto.Attribut;
        Environnement = valeurAttributForUpdateDto.Environnement;

        QueueDomainEvent(new ValeurAttributUpdated(){ Id = Id });
        return this;
    }
    
    protected ValeurAttribut() { } // For EF + Mocking
}