namespace ApiCartobani.Domain.TypeElements;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Domain.TypeElements.Validators;
using ApiCartobani.Domain.TypeElements.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ApiCartobani.Domain.Attributs;


public class TypeElement : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    private TypeEltEnum _type;
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Type
    {
        get => _type.Name;
        private set
        {
            if (!TypeEltEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Type), value);

            _type = parsed;
        }
    }

    public virtual string Icone { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual ICollection<Attribut> Attributs { get; private set; }


    public static TypeElement Create(TypeElementForCreationDto typeElementForCreationDto)
    {
        new TypeElementForCreationDtoValidator().ValidateAndThrow(typeElementForCreationDto);

        var newTypeElement = new TypeElement();

        newTypeElement.Nom = typeElementForCreationDto.Nom;
        newTypeElement.Type = typeElementForCreationDto.Type;
        newTypeElement.Icone = typeElementForCreationDto.Icone;

        newTypeElement.QueueDomainEvent(new TypeElementCreated(){ TypeElement = newTypeElement });
        
        return newTypeElement;
    }

    public TypeElement Update(TypeElementForUpdateDto typeElementForUpdateDto)
    {
        new TypeElementForUpdateDtoValidator().ValidateAndThrow(typeElementForUpdateDto);

        Nom = typeElementForUpdateDto.Nom;
        Type = typeElementForUpdateDto.Type;
        Icone = typeElementForUpdateDto.Icone;

        QueueDomainEvent(new TypeElementUpdated(){ Id = Id });
        return this;
    }
    
    protected TypeElement() { } // For EF + Mocking
}