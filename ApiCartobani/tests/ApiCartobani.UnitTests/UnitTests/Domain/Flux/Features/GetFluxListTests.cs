namespace ApiCartobani.UnitTests.UnitTests.Domain.Flux.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Domain.Flux.Mappings;
using ApiCartobani.Domain.Flux.Features;
using ApiCartobani.Domain.Flux.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetFluxListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IFluxRepository> _fluxRepository;

    public GetFluxListTests()
    {
        _fluxRepository = new Mock<IFluxRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_flux()
    {
        //Arrange
        var fakeFluxOne = FakeFlux.Generate();
        var fakeFluxTwo = FakeFlux.Generate();
        var fakeFluxThree = FakeFlux.Generate();
        var flux = new List<Flux>();
        flux.Add(fakeFluxOne);
        flux.Add(fakeFluxTwo);
        flux.Add(fakeFluxThree);
        var mockDbData = flux.AsQueryable().BuildMock();
        
        var queryParameters = new FluxParametersDto() { PageSize = 1, PageNumber = 2 };

        _fluxRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetFluxList.Query(queryParameters);
        var handler = new GetFluxList.Handler(_fluxRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_flux_list_using_Nom()
    {
        //Arrange
        var fakeFluxOne = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Nom, _ => "alpha")
            .Generate());
        var fakeFluxTwo = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new FluxParametersDto() { Filters = $"Nom == {fakeFluxTwo.Nom}" };

        var fluxList = new List<Flux>() { fakeFluxOne, fakeFluxTwo };
        var mockDbData = fluxList.AsQueryable().BuildMock();

        _fluxRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetFluxList.Query(queryParameters);
        var handler = new GetFluxList.Handler(_fluxRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFluxTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_flux_list_using_Entree()
    {
        //Arrange
        var fakeFluxOne = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => Guid.NewGuid())
            .Generate());
        var fakeFluxTwo = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => Guid.NewGuid())
            .Generate());
        var queryParameters = new FluxParametersDto() { Filters = $"Entree == {fakeFluxTwo.Entree}" };

        var fluxList = new List<Flux>() { fakeFluxOne, fakeFluxTwo };
        var mockDbData = fluxList.AsQueryable().BuildMock();

        _fluxRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetFluxList.Query(queryParameters);
        var handler = new GetFluxList.Handler(_fluxRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFluxTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_flux_list_using_Sortie()
    {
        //Arrange
        var fakeFluxOne = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Sortie, _ => Guid.NewGuid())
            .Generate());
        var fakeFluxTwo = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Sortie, _ => Guid.NewGuid())
            .Generate());
        var queryParameters = new FluxParametersDto() { Filters = $"Sortie == {fakeFluxTwo.Sortie}" };

        var fluxList = new List<Flux>() { fakeFluxOne, fakeFluxTwo };
        var mockDbData = fluxList.AsQueryable().BuildMock();

        _fluxRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetFluxList.Query(queryParameters);
        var handler = new GetFluxList.Handler(_fluxRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFluxTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_flux_by_Nom()
    {
        //Arrange
        var fakeFluxOne = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Nom, _ => "alpha")
            .Generate());
        var fakeFluxTwo = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new FluxParametersDto() { SortOrder = "-Nom" };

        var FluxList = new List<Flux>() { fakeFluxOne, fakeFluxTwo };
        var mockDbData = FluxList.AsQueryable().BuildMock();

        _fluxRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetFluxList.Query(queryParameters);
        var handler = new GetFluxList.Handler(_fluxRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeFluxTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFluxOne, options =>
                options.ExcludingMissingMembers());
    }
}