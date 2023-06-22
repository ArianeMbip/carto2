namespace ApiCartobani.UnitTests.UnitTests.Domain.Historiques.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Domain.Historiques.Mappings;
using ApiCartobani.Domain.Historiques.Features;
using ApiCartobani.Domain.Historiques.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetHistoriqueListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IHistoriqueRepository> _historiqueRepository;

    public GetHistoriqueListTests()
    {
        _historiqueRepository = new Mock<IHistoriqueRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_historique()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate();
        var fakeHistoriqueTwo = FakeHistorique.Generate();
        var fakeHistoriqueThree = FakeHistorique.Generate();
        var historique = new List<Historique>();
        historique.Add(fakeHistoriqueOne);
        historique.Add(fakeHistoriqueTwo);
        historique.Add(fakeHistoriqueThree);
        var mockDbData = historique.AsQueryable().BuildMock();
        
        var queryParameters = new HistoriqueParametersDto() { PageSize = 1, PageNumber = 2 };

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_historique_list_using_NouvelleValeur()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NouvelleValeur, _ => "alpha")
            .Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NouvelleValeur, _ => "bravo")
            .Generate());
        var queryParameters = new HistoriqueParametersDto() { Filters = $"NouvelleValeur == {fakeHistoriqueTwo.NouvelleValeur}" };

        var historiqueList = new List<Historique>() { fakeHistoriqueOne, fakeHistoriqueTwo };
        var mockDbData = historiqueList.AsQueryable().BuildMock();

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_historique_list_using_NomUtilisateur()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NomUtilisateur, _ => "alpha")
            .Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NomUtilisateur, _ => "bravo")
            .Generate());
        var queryParameters = new HistoriqueParametersDto() { Filters = $"NomUtilisateur == {fakeHistoriqueTwo.NomUtilisateur}" };

        var historiqueList = new List<Historique>() { fakeHistoriqueOne, fakeHistoriqueTwo };
        var mockDbData = historiqueList.AsQueryable().BuildMock();

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_historique_list_using_CUID()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.CUID, _ => "alpha")
            .Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.CUID, _ => "bravo")
            .Generate());
        var queryParameters = new HistoriqueParametersDto() { Filters = $"CUID == {fakeHistoriqueTwo.CUID}" };

        var historiqueList = new List<Historique>() { fakeHistoriqueOne, fakeHistoriqueTwo };
        var mockDbData = historiqueList.AsQueryable().BuildMock();

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_historique_by_NouvelleValeur()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NouvelleValeur, _ => "alpha")
            .Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NouvelleValeur, _ => "bravo")
            .Generate());
        var queryParameters = new HistoriqueParametersDto() { SortOrder = "-NouvelleValeur" };

        var HistoriqueList = new List<Historique>() { fakeHistoriqueOne, fakeHistoriqueTwo };
        var mockDbData = HistoriqueList.AsQueryable().BuildMock();

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_historique_by_NomUtilisateur()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NomUtilisateur, _ => "alpha")
            .Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.NomUtilisateur, _ => "bravo")
            .Generate());
        var queryParameters = new HistoriqueParametersDto() { SortOrder = "-NomUtilisateur" };

        var HistoriqueList = new List<Historique>() { fakeHistoriqueOne, fakeHistoriqueTwo };
        var mockDbData = HistoriqueList.AsQueryable().BuildMock();

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_historique_by_CUID()
    {
        //Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.CUID, _ => "alpha")
            .Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto()
            .RuleFor(h => h.CUID, _ => "bravo")
            .Generate());
        var queryParameters = new HistoriqueParametersDto() { SortOrder = "-CUID" };

        var HistoriqueList = new List<Historique>() { fakeHistoriqueOne, fakeHistoriqueTwo };
        var mockDbData = HistoriqueList.AsQueryable().BuildMock();

        _historiqueRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var handler = new GetHistoriqueList.Handler(_historiqueRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHistoriqueOne, options =>
                options.ExcludingMissingMembers());
    }
}