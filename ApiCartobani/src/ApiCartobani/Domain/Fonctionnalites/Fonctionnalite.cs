namespace ApiCartobani.Domain.Fonctionnalites;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Domain.Fonctionnalites.Validators;
using ApiCartobani.Domain.Fonctionnalites.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Fonctionnalite : BaseEntity
{
    [Required]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    private TypeEnum _type;
    [Required]    [Sieve(CanFilter = true, CanSort = true)]    public virtual string Type
    {
        get => _type.Name;
        private set
        {
            if (!TypeEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Type), value);

            _type = parsed;
        }
    }




    public static Fonctionnalite Create(FonctionnaliteForCreationDto fonctionnaliteForCreationDto)
    {
        new FonctionnaliteForCreationDtoValidator().ValidateAndThrow(fonctionnaliteForCreationDto);

        var newFonctionnalite = new Fonctionnalite();

        newFonctionnalite.Nom = fonctionnaliteForCreationDto.Nom;
        newFonctionnalite.Type = fonctionnaliteForCreationDto.Type;

        newFonctionnalite.QueueDomainEvent(new FonctionnaliteCreated(){ Fonctionnalite = newFonctionnalite });
        
        return newFonctionnalite;
    }

    public Fonctionnalite Update(FonctionnaliteForUpdateDto fonctionnaliteForUpdateDto)
    {
        new FonctionnaliteForUpdateDtoValidator().ValidateAndThrow(fonctionnaliteForUpdateDto);

        Nom = fonctionnaliteForUpdateDto.Nom;
        Type = fonctionnaliteForUpdateDto.Type;

        QueueDomainEvent(new FonctionnaliteUpdated(){ Id = Id });
        return this;
    }
    
    protected Fonctionnalite() { } // For EF + Mocking
}