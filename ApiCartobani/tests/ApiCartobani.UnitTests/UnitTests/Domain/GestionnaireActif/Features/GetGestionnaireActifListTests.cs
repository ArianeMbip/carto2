namespace ApiCartobani.UnitTests.UnitTests.Domain.GestionnaireActif.Features;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Dtos;
using ApiCartobani.Domain.GestionnaireActif.Mappings;
using ApiCartobani.Domain.GestionnaireActif.Features;
using ApiCartobani.Domain.GestionnaireActif.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetGestionnaireActifListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IGestionnaireActifRepository> _gestionnaireActifRepository;

    public GetGestionnaireActifListTests()
    {
        _gestionnaireActifRepository = new Mock<IGestionnaireActifRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_gestionnaireActif()
    {
        //Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate();
        var fakeGestionnaireActifTwo = FakeGestionnaireActif.Generate();
        var fakeGestionnaireActifThree = FakeGestionnaireActif.Generate();
        var gestionnaireActif = new List<GestionnaireActif>();
        gestionnaireActif.Add(fakeGestionnaireActifOne);
        gestionnaireActif.Add(fakeGestionnaireActifTwo);
        gestionnaireActif.Add(fakeGestionnaireActifThree);
        var mockDbData = gestionnaireActif.AsQueryable().BuildMock();
        
        var queryParameters = new GestionnaireActifParametersDto() { PageSize = 1, PageNumber = 2 };

        _gestionnaireActifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetGestionnaireActifList.Query(queryParameters);
        var handler = new GetGestionnaireActifList.Handler(_gestionnaireActifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_gestionnaireactif_list_using_Nom()
    {
        //Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.Nom, _ => "alpha")
            .Generate());
        var fakeGestionnaireActifTwo = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new GestionnaireActifParametersDto() { Filters = $"Nom == {fakeGestionnaireActifTwo.Nom}" };

        var gestionnaireActifList = new List<GestionnaireActif>() { fakeGestionnaireActifOne, fakeGestionnaireActifTwo };
        var mockDbData = gestionnaireActifList.AsQueryable().BuildMock();

        _gestionnaireActifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetGestionnaireActifList.Query(queryParameters);
        var handler = new GetGestionnaireActifList.Handler(_gestionnaireActifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeGestionnaireActifTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_gestionnaireactif_list_using_CUID()
    {
        //Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.CUID, _ => "alpha")
            .Generate());
        var fakeGestionnaireActifTwo = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.CUID, _ => "bravo")
            .Generate());
        var queryParameters = new GestionnaireActifParametersDto() { Filters = $"CUID == {fakeGestionnaireActifTwo.CUID}" };

        var gestionnaireActifList = new List<GestionnaireActif>() { fakeGestionnaireActifOne, fakeGestionnaireActifTwo };
        var mockDbData = gestionnaireActifList.AsQueryable().BuildMock();

        _gestionnaireActifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetGestionnaireActifList.Query(queryParameters);
        var handler = new GetGestionnaireActifList.Handler(_gestionnaireActifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeGestionnaireActifTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_gestionnaireactif_by_Nom()
    {
        //Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.Nom, _ => "alpha")
            .Generate());
        var fakeGestionnaireActifTwo = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new GestionnaireActifParametersDto() { SortOrder = "-Nom" };

        var GestionnaireActifList = new List<GestionnaireActif>() { fakeGestionnaireActifOne, fakeGestionnaireActifTwo };
        var mockDbData = GestionnaireActifList.AsQueryable().BuildMock();

        _gestionnaireActifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetGestionnaireActifList.Query(queryParameters);
        var handler = new GetGestionnaireActifList.Handler(_gestionnaireActifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeGestionnaireActifTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeGestionnaireActifOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_gestionnaireactif_by_CUID()
    {
        //Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.CUID, _ => "alpha")
            .Generate());
        var fakeGestionnaireActifTwo = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto()
            .RuleFor(G => G.CUID, _ => "bravo")
            .Generate());
        var queryParameters = new GestionnaireActifParametersDto() { SortOrder = "-CUID" };

        var GestionnaireActifList = new List<GestionnaireActif>() { fakeGestionnaireActifOne, fakeGestionnaireActifTwo };
        var mockDbData = GestionnaireActifList.AsQueryable().BuildMock();

        _gestionnaireActifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetGestionnaireActifList.Query(queryParameters);
        var handler = new GetGestionnaireActifList.Handler(_gestionnaireActifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeGestionnaireActifTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeGestionnaireActifOne, options =>
                options.ExcludingMissingMembers());
    }
}