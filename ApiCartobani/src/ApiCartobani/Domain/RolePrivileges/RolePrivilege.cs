namespace ApiCartobani.Domain.RolePrivileges;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Domain.RolePrivileges.Validators;
using ApiCartobani.Domain.RolePrivileges.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class RolePrivilege : BaseEntity
{
    [Column("Name")]
    public virtual string Nom { get; private set; }

    public virtual bool Lire { get; private set; }

    public virtual bool Ecrire { get; private set; }

    public virtual bool Modifier { get; private set; }

    public virtual bool Supprimer { get; private set; }

    public virtual bool Valider { get; private set; }

    public virtual bool Archiver { get; private set; }

    public virtual bool Generer { get; private set; }


    public static RolePrivilege Create(RolePrivilegeForCreationDto rolePrivilegeForCreationDto)
    {
        new RolePrivilegeForCreationDtoValidator().ValidateAndThrow(rolePrivilegeForCreationDto);

        var newRolePrivilege = new RolePrivilege();

        newRolePrivilege.Nom = rolePrivilegeForCreationDto.Nom;
        newRolePrivilege.Lire = rolePrivilegeForCreationDto.Lire;
        newRolePrivilege.Ecrire = rolePrivilegeForCreationDto.Ecrire;
        newRolePrivilege.Modifier = rolePrivilegeForCreationDto.Modifier;
        newRolePrivilege.Supprimer = rolePrivilegeForCreationDto.Supprimer;
        newRolePrivilege.Valider = rolePrivilegeForCreationDto.Valider;
        newRolePrivilege.Archiver = rolePrivilegeForCreationDto.Archiver;
        newRolePrivilege.Generer = rolePrivilegeForCreationDto.Generer;

        newRolePrivilege.QueueDomainEvent(new RolePrivilegeCreated(){ RolePrivilege = newRolePrivilege });
        
        return newRolePrivilege;
    }

    public RolePrivilege Update(RolePrivilegeForUpdateDto rolePrivilegeForUpdateDto)
    {
        new RolePrivilegeForUpdateDtoValidator().ValidateAndThrow(rolePrivilegeForUpdateDto);

        Nom = rolePrivilegeForUpdateDto.Nom;
        Lire = rolePrivilegeForUpdateDto.Lire;
        Ecrire = rolePrivilegeForUpdateDto.Ecrire;
        Modifier = rolePrivilegeForUpdateDto.Modifier;
        Supprimer = rolePrivilegeForUpdateDto.Supprimer;
        Valider = rolePrivilegeForUpdateDto.Valider;
        Archiver = rolePrivilegeForUpdateDto.Archiver;
        Generer = rolePrivilegeForUpdateDto.Generer;

        QueueDomainEvent(new RolePrivilegeUpdated(){ Id = Id });
        return this;
    }
    
    protected RolePrivilege() { } // For EF + Mocking
}