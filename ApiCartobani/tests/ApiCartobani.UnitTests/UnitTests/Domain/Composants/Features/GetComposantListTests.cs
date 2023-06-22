namespace ApiCartobani.UnitTests.UnitTests.Domain.Composants.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Domain.Composants.Mappings;
using ApiCartobani.Domain.Composants.Features;
using ApiCartobani.Domain.Composants.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetComposantListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IComposantRepository> _composantRepository;

    public GetComposantListTests()
    {
        _composantRepository = new Mock<IComposantRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_composant()
    {
        //Arrange
        var fakeComposantOne = FakeComposant.Generate();
        var fakeComposantTwo = FakeComposant.Generate();
        var fakeComposantThree = FakeComposant.Generate();
        var composant = new List<Composant>();
        composant.Add(fakeComposantOne);
        composant.Add(fakeComposantTwo);
        composant.Add(fakeComposantThree);
        var mockDbData = composant.AsQueryable().BuildMock();
        
        var queryParameters = new ComposantParametersDto() { PageSize = 1, PageNumber = 2 };

        _composantRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetComposantList.Query(queryParameters);
        var handler = new GetComposantList.Handler(_composantRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_composant_list_using_Nom()
    {
        //Arrange
        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.Nom, _ => "alpha")
            .Generate());
        var fakeComposantTwo = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new ComposantParametersDto() { Filters = $"Nom == {fakeComposantTwo.Nom}" };

        var composantList = new List<Composant>() { fakeComposantOne, fakeComposantTwo };
        var mockDbData = composantList.AsQueryable().BuildMock();

        _composantRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetComposantList.Query(queryParameters);
        var handler = new GetComposantList.Handler(_composantRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeComposantTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_composant_by_Nom()
    {
        //Arrange
        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.Nom, _ => "alpha")
            .Generate());
        var fakeComposantTwo = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new ComposantParametersDto() { SortOrder = "-Nom" };

        var ComposantList = new List<Composant>() { fakeComposantOne, fakeComposantTwo };
        var mockDbData = ComposantList.AsQueryable().BuildMock();

        _composantRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetComposantList.Query(queryParameters);
        var handler = new GetComposantList.Handler(_composantRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeComposantTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeComposantOne, options =>
                options.ExcludingMissingMembers());
    }
}