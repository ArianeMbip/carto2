namespace ApiCartobani.Domain.PiecesJointes;

using SharedKernel.Exceptions;
using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Domain.PiecesJointes.Validators;
using ApiCartobani.Domain.PiecesJointes.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class PieceJointe : BaseEntity
{
    [Required]
    [Sieve(CanFilter = true, CanSort = true)]
    [Column("Nom")]
    public virtual string Nom { get; private set; }

    [Required]
    public virtual string Chemin { get; private set; }


    public static PieceJointe Create(PieceJointeForCreationDto pieceJointeForCreationDto)
    {
        new PieceJointeForCreationDtoValidator().ValidateAndThrow(pieceJointeForCreationDto);

        var newPieceJointe = new PieceJointe();

        newPieceJointe.Nom = pieceJointeForCreationDto.Nom;
        newPieceJointe.Chemin = pieceJointeForCreationDto.Chemin;

        newPieceJointe.QueueDomainEvent(new PieceJointeCreated(){ PieceJointe = newPieceJointe });
        
        return newPieceJointe;
    }

    public PieceJointe Update(PieceJointeForUpdateDto pieceJointeForUpdateDto)
    {
        new PieceJointeForUpdateDtoValidator().ValidateAndThrow(pieceJointeForUpdateDto);

        Nom = pieceJointeForUpdateDto.Nom;
        Chemin = pieceJointeForUpdateDto.Chemin;

        QueueDomainEvent(new PieceJointeUpdated(){ Id = Id });
        return this;
    }
    
    protected PieceJointe() { } // For EF + Mocking
}