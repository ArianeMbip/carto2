namespace ApiCartobani.Domain.Attributs;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Attributs.Validators;
using ApiCartobani.Domain.Attributs.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Attribut : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool Requis { get; private set; }

    [Required]
    public virtual string TypeDonnee { get; private set; }


    public static Attribut Create(AttributForCreationDto attributForCreationDto)
    {
        new AttributForCreationDtoValidator().ValidateAndThrow(attributForCreationDto);

        var newAttribut = new Attribut();

        newAttribut.Nom = attributForCreationDto.Nom;
        newAttribut.Requis = attributForCreationDto.Requis;
        newAttribut.TypeDonnee = attributForCreationDto.TypeDonnee;

        newAttribut.QueueDomainEvent(new AttributCreated(){ Attribut = newAttribut });
        
        return newAttribut;
    }

    public Attribut Update(AttributForUpdateDto attributForUpdateDto)
    {
        new AttributForUpdateDtoValidator().ValidateAndThrow(attributForUpdateDto);

        Nom = attributForUpdateDto.Nom;
        Requis = attributForUpdateDto.Requis;
        TypeDonnee = attributForUpdateDto.TypeDonnee;

        QueueDomainEvent(new AttributUpdated(){ Id = Id });
        return this;
    }
    
    protected Attribut() { } // For EF + Mocking
}