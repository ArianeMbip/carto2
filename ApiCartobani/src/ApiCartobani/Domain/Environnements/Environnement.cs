namespace ApiCartobani.Domain.Environnements;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Domain.Environnements.Validators;
using ApiCartobani.Domain.Environnements.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class Environnement : BaseEntity
{
    [Column("Name")]
    public virtual string Nom { get; private set; }


    public static Environnement Create(EnvironnementForCreationDto environnementForCreationDto)
    {
        new EnvironnementForCreationDtoValidator().ValidateAndThrow(environnementForCreationDto);

        var newEnvironnement = new Environnement();

        newEnvironnement.Nom = environnementForCreationDto.Nom;

        newEnvironnement.QueueDomainEvent(new EnvironnementCreated(){ Environnement = newEnvironnement });
        
        return newEnvironnement;
    }

    public Environnement Update(EnvironnementForUpdateDto environnementForUpdateDto)
    {
        new EnvironnementForUpdateDtoValidator().ValidateAndThrow(environnementForUpdateDto);

        Nom = environnementForUpdateDto.Nom;

        QueueDomainEvent(new EnvironnementUpdated(){ Id = Id });
        return this;
    }
    
    protected Environnement() { } // For EF + Mocking
}