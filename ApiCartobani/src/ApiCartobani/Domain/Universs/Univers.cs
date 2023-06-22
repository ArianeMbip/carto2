namespace ApiCartobani.Domain.Universs;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Universs.Dtos;
using ApiCartobani.Domain.Universs.Validators;
using ApiCartobani.Domain.Universs.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.Fonctionnalites;


public class Univers : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual ICollection<Fonctionnalite> Fonctionnalite { get; private set; }


    public static Univers Create(UniversForCreationDto universForCreationDto)
    {
        new UniversForCreationDtoValidator().ValidateAndThrow(universForCreationDto);

        var newUnivers = new Univers();

        newUnivers.Nom = universForCreationDto.Nom;

        newUnivers.QueueDomainEvent(new UniversCreated(){ Univers = newUnivers });
        
        return newUnivers;
    }

    public Univers Update(UniversForUpdateDto universForUpdateDto)
    {
        new UniversForUpdateDtoValidator().ValidateAndThrow(universForUpdateDto);

        Nom = universForUpdateDto.Nom;

        QueueDomainEvent(new UniversUpdated(){ Id = Id });
        return this;
    }
    
    protected Univers() { } // For EF + Mocking
}