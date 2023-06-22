namespace ApiCartobani.Domain.Icones;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Domain.Icones.Validators;
using ApiCartobani.Domain.Icones.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Icone : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Name")]
    public virtual string Url { get; private set; }


    public static Icone Create(IconeForCreationDto iconeForCreationDto)
    {
        new IconeForCreationDtoValidator().ValidateAndThrow(iconeForCreationDto);

        var newIcone = new Icone();

        newIcone.Url = iconeForCreationDto.Url;

        newIcone.QueueDomainEvent(new IconeCreated(){ Icone = newIcone });
        
        return newIcone;
    }

    public Icone Update(IconeForUpdateDto iconeForUpdateDto)
    {
        new IconeForUpdateDtoValidator().ValidateAndThrow(iconeForUpdateDto);

        Url = iconeForUpdateDto.Url;

        QueueDomainEvent(new IconeUpdated(){ Id = Id });
        return this;
    }
    
    protected Icone() { } // For EF + Mocking
}