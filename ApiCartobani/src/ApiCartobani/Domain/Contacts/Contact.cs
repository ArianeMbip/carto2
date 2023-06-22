namespace ApiCartobani.Domain.Contacts;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Domain.Contacts.Validators;
using ApiCartobani.Domain.Contacts.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Contact : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Email { get; private set; }

    public virtual string Entite { get; private set; }

    public virtual string Fonction { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Telephone { get; private set; }


    public static Contact Create(ContactForCreationDto contactForCreationDto)
    {
        new ContactForCreationDtoValidator().ValidateAndThrow(contactForCreationDto);

        var newContact = new Contact();

        newContact.Nom = contactForCreationDto.Nom;
        newContact.Email = contactForCreationDto.Email;
        newContact.Entite = contactForCreationDto.Entite;
        newContact.Fonction = contactForCreationDto.Fonction;
        newContact.Telephone = contactForCreationDto.Telephone;

        newContact.QueueDomainEvent(new ContactCreated(){ Contact = newContact });
        
        return newContact;
    }

    public Contact Update(ContactForUpdateDto contactForUpdateDto)
    {
        new ContactForUpdateDtoValidator().ValidateAndThrow(contactForUpdateDto);

        Nom = contactForUpdateDto.Nom;
        Email = contactForUpdateDto.Email;
        Entite = contactForUpdateDto.Entite;
        Fonction = contactForUpdateDto.Fonction;
        Telephone = contactForUpdateDto.Telephone;

        QueueDomainEvent(new ContactUpdated(){ Id = Id });
        return this;
    }
    
    protected Contact() { } // For EF + Mocking
}