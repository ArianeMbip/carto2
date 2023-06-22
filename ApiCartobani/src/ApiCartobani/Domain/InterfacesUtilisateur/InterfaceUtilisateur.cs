namespace ApiCartobani.Domain.InterfacesUtilisateur;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Domain.InterfacesUtilisateur.Validators;
using ApiCartobani.Domain.InterfacesUtilisateur.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class InterfaceUtilisateur : BaseEntity
{
    [Required]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Required]
    public virtual string Image { get; private set; }


    public static InterfaceUtilisateur Create(InterfaceUtilisateurForCreationDto interfaceUtilisateurForCreationDto)
    {
        new InterfaceUtilisateurForCreationDtoValidator().ValidateAndThrow(interfaceUtilisateurForCreationDto);

        var newInterfaceUtilisateur = new InterfaceUtilisateur();

        newInterfaceUtilisateur.Nom = interfaceUtilisateurForCreationDto.Nom;
        newInterfaceUtilisateur.Image = interfaceUtilisateurForCreationDto.Image;

        newInterfaceUtilisateur.QueueDomainEvent(new InterfaceUtilisateurCreated(){ InterfaceUtilisateur = newInterfaceUtilisateur });
        
        return newInterfaceUtilisateur;
    }

    public InterfaceUtilisateur Update(InterfaceUtilisateurForUpdateDto interfaceUtilisateurForUpdateDto)
    {
        new InterfaceUtilisateurForUpdateDtoValidator().ValidateAndThrow(interfaceUtilisateurForUpdateDto);

        Nom = interfaceUtilisateurForUpdateDto.Nom;
        Image = interfaceUtilisateurForUpdateDto.Image;

        QueueDomainEvent(new InterfaceUtilisateurUpdated(){ Id = Id });
        return this;
    }
    
    protected InterfaceUtilisateur() { } // For EF + Mocking
}