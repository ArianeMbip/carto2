namespace ApiCartobani.UnitTests.UnitTests.Domain.Actifs.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.Actifs.Mappings;
using ApiCartobani.Domain.Actifs.Features;
using ApiCartobani.Domain.Actifs.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetActifListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IActifRepository> _actifRepository;

    public GetActifListTests()
    {
        _actifRepository = new Mock<IActifRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_actif()
    {
        //Arrange
        var fakeActifOne = FakeActif.Generate();
        var fakeActifTwo = FakeActif.Generate();
        var fakeActifThree = FakeActif.Generate();
        var actif = new List<Actif>();
        actif.Add(fakeActifOne);
        actif.Add(fakeActifTwo);
        actif.Add(fakeActifThree);
        var mockDbData = actif.AsQueryable().BuildMock();
        
        var queryParameters = new ActifParametersDto() { PageSize = 1, PageNumber = 2 };

        _actifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetActifList.Query(queryParameters);
        var handler = new GetActifList.Handler(_actifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_actif_list_using_Nom()
    {
        //Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.Nom, _ => "alpha")
            .Generate());
        var fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new ActifParametersDto() { Filters = $"Nom == {fakeActifTwo.Nom}" };

        var actifList = new List<Actif>() { fakeActifOne, fakeActifTwo };
        var mockDbData = actifList.AsQueryable().BuildMock();

        _actifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetActifList.Query(queryParameters);
        var handler = new GetActifList.Handler(_actifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeActifTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_actif_list_using_PreVersion()
    {
        //Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.PreVersion, _ => Guid.NewGuid())
            .Generate());
        var fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.PreVersion, _ => Guid.NewGuid())
            .Generate());
        var queryParameters = new ActifParametersDto() { Filters = $"PreVersion == {fakeActifTwo.PreVersion}" };

        var actifList = new List<Actif>() { fakeActifOne, fakeActifTwo };
        var mockDbData = actifList.AsQueryable().BuildMock();

        _actifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetActifList.Query(queryParameters);
        var handler = new GetActifList.Handler(_actifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeActifTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_actif_by_Nom()
    {
        //Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.Nom, _ => "alpha")
            .Generate());
        var fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new ActifParametersDto() { SortOrder = "-Nom" };

        var ActifList = new List<Actif>() { fakeActifOne, fakeActifTwo };
        var mockDbData = ActifList.AsQueryable().BuildMock();

        _actifRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetActifList.Query(queryParameters);
        var handler = new GetActifList.Handler(_actifRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeActifTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeActifOne, options =>
                options.ExcludingMissingMembers());
    }
}