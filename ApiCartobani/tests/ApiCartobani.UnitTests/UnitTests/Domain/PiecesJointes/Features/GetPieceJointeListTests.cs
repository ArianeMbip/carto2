namespace ApiCartobani.UnitTests.UnitTests.Domain.PiecesJointes.Features;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Domain.PiecesJointes.Mappings;
using ApiCartobani.Domain.PiecesJointes.Features;
using ApiCartobani.Domain.PiecesJointes.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetPieceJointeListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IPieceJointeRepository> _pieceJointeRepository;

    public GetPieceJointeListTests()
    {
        _pieceJointeRepository = new Mock<IPieceJointeRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_pieceJointe()
    {
        //Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate();
        var fakePieceJointeTwo = FakePieceJointe.Generate();
        var fakePieceJointeThree = FakePieceJointe.Generate();
        var pieceJointe = new List<PieceJointe>();
        pieceJointe.Add(fakePieceJointeOne);
        pieceJointe.Add(fakePieceJointeTwo);
        pieceJointe.Add(fakePieceJointeThree);
        var mockDbData = pieceJointe.AsQueryable().BuildMock();
        
        var queryParameters = new PieceJointeParametersDto() { PageSize = 1, PageNumber = 2 };

        _pieceJointeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetPieceJointeList.Query(queryParameters);
        var handler = new GetPieceJointeList.Handler(_pieceJointeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_piecejointe_list_using_Nom()
    {
        //Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto()
            .RuleFor(p => p.Nom, _ => "alpha")
            .Generate());
        var fakePieceJointeTwo = FakePieceJointe.Generate(new FakePieceJointeForCreationDto()
            .RuleFor(p => p.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new PieceJointeParametersDto() { Filters = $"Nom == {fakePieceJointeTwo.Nom}" };

        var pieceJointeList = new List<PieceJointe>() { fakePieceJointeOne, fakePieceJointeTwo };
        var mockDbData = pieceJointeList.AsQueryable().BuildMock();

        _pieceJointeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetPieceJointeList.Query(queryParameters);
        var handler = new GetPieceJointeList.Handler(_pieceJointeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakePieceJointeTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_piecejointe_by_Nom()
    {
        //Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto()
            .RuleFor(p => p.Nom, _ => "alpha")
            .Generate());
        var fakePieceJointeTwo = FakePieceJointe.Generate(new FakePieceJointeForCreationDto()
            .RuleFor(p => p.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new PieceJointeParametersDto() { SortOrder = "-Nom" };

        var PieceJointeList = new List<PieceJointe>() { fakePieceJointeOne, fakePieceJointeTwo };
        var mockDbData = PieceJointeList.AsQueryable().BuildMock();

        _pieceJointeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetPieceJointeList.Query(queryParameters);
        var handler = new GetPieceJointeList.Handler(_pieceJointeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakePieceJointeTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakePieceJointeOne, options =>
                options.ExcludingMissingMembers());
    }
}