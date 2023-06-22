namespace ApiCartobani.Domain.Historiques;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Domain.Historiques.Validators;
using ApiCartobani.Domain.Historiques.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Historique : BaseEntity
{
    [Required]
    [Column("Name")]
    public virtual DateTime DateModification { get; private set; }

    [Required]
    public virtual string PartieModifiee { get; private set; }

    public virtual string AncienneValeur { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string NouvelleValeur { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string NomUtilisateur { get; private set; }

    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string CUID { get; private set; }


    public static Historique Create(HistoriqueForCreationDto historiqueForCreationDto)
    {
        new HistoriqueForCreationDtoValidator().ValidateAndThrow(historiqueForCreationDto);

        var newHistorique = new Historique();

        newHistorique.DateModification = historiqueForCreationDto.DateModification;
        newHistorique.PartieModifiee = historiqueForCreationDto.PartieModifiee;
        newHistorique.AncienneValeur = historiqueForCreationDto.AncienneValeur;
        newHistorique.NouvelleValeur = historiqueForCreationDto.NouvelleValeur;
        newHistorique.NomUtilisateur = historiqueForCreationDto.NomUtilisateur;
        newHistorique.CUID = historiqueForCreationDto.CUID;

        newHistorique.QueueDomainEvent(new HistoriqueCreated(){ Historique = newHistorique });
        
        return newHistorique;
    }

    public Historique Update(HistoriqueForUpdateDto historiqueForUpdateDto)
    {
        new HistoriqueForUpdateDtoValidator().ValidateAndThrow(historiqueForUpdateDto);

        DateModification = historiqueForUpdateDto.DateModification;
        PartieModifiee = historiqueForUpdateDto.PartieModifiee;
        AncienneValeur = historiqueForUpdateDto.AncienneValeur;
        NouvelleValeur = historiqueForUpdateDto.NouvelleValeur;
        NomUtilisateur = historiqueForUpdateDto.NomUtilisateur;
        CUID = historiqueForUpdateDto.CUID;

        QueueDomainEvent(new HistoriqueUpdated(){ Id = Id });
        return this;
    }
    
    protected Historique() { } // For EF + Mocking
}