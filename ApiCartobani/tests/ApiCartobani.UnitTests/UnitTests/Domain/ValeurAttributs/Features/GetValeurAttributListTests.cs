namespace ApiCartobani.UnitTests.UnitTests.Domain.ValeurAttributs.Features;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.ValeurAttributs.Mappings;
using ApiCartobani.Domain.ValeurAttributs.Features;
using ApiCartobani.Domain.ValeurAttributs.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetValeurAttributListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IValeurAttributRepository> _valeurAttributRepository;

    public GetValeurAttributListTests()
    {
        _valeurAttributRepository = new Mock<IValeurAttributRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_valeurAttribut()
    {
        //Arrange
        var fakeValeurAttributOne = FakeValeurAttribut.Generate();
        var fakeValeurAttributTwo = FakeValeurAttribut.Generate();
        var fakeValeurAttributThree = FakeValeurAttribut.Generate();
        var valeurAttribut = new List<ValeurAttribut>();
        valeurAttribut.Add(fakeValeurAttributOne);
        valeurAttribut.Add(fakeValeurAttributTwo);
        valeurAttribut.Add(fakeValeurAttributThree);
        var mockDbData = valeurAttribut.AsQueryable().BuildMock();
        
        var queryParameters = new ValeurAttributParametersDto() { PageSize = 1, PageNumber = 2 };

        _valeurAttributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetValeurAttributList.Query(queryParameters);
        var handler = new GetValeurAttributList.Handler(_valeurAttributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_valeurattribut_list_using_Valeur()
    {
        //Arrange
        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Valeur, _ => "alpha")
            .Generate());
        var fakeValeurAttributTwo = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Valeur, _ => "bravo")
            .Generate());
        var queryParameters = new ValeurAttributParametersDto() { Filters = $"Valeur == {fakeValeurAttributTwo.Valeur}" };

        var valeurAttributList = new List<ValeurAttribut>() { fakeValeurAttributOne, fakeValeurAttributTwo };
        var mockDbData = valeurAttributList.AsQueryable().BuildMock();

        _valeurAttributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetValeurAttributList.Query(queryParameters);
        var handler = new GetValeurAttributList.Handler(_valeurAttributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeValeurAttributTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_valeurattribut_list_using_Attribut()
    {
        //Arrange
        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => Guid.NewGuid())
            .Generate());
        var fakeValeurAttributTwo = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => Guid.NewGuid())
            .Generate());
        var queryParameters = new ValeurAttributParametersDto() { Filters = $"Attribut == {fakeValeurAttributTwo.Attribut}" };

        var valeurAttributList = new List<ValeurAttribut>() { fakeValeurAttributOne, fakeValeurAttributTwo };
        var mockDbData = valeurAttributList.AsQueryable().BuildMock();

        _valeurAttributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetValeurAttributList.Query(queryParameters);
        var handler = new GetValeurAttributList.Handler(_valeurAttributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeValeurAttributTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_valeurattribut_list_using_Environnement()
    {
        //Arrange
        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Environnement, _ => Guid.NewGuid())
            .Generate());
        var fakeValeurAttributTwo = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Environnement, _ => Guid.NewGuid())
            .Generate());
        var queryParameters = new ValeurAttributParametersDto() { Filters = $"Environnement == {fakeValeurAttributTwo.Environnement}" };

        var valeurAttributList = new List<ValeurAttribut>() { fakeValeurAttributOne, fakeValeurAttributTwo };
        var mockDbData = valeurAttributList.AsQueryable().BuildMock();

        _valeurAttributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetValeurAttributList.Query(queryParameters);
        var handler = new GetValeurAttributList.Handler(_valeurAttributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeValeurAttributTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_valeurattribut_by_Valeur()
    {
        //Arrange
        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Valeur, _ => "alpha")
            .Generate());
        var fakeValeurAttributTwo = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Valeur, _ => "bravo")
            .Generate());
        var queryParameters = new ValeurAttributParametersDto() { SortOrder = "-Valeur" };

        var ValeurAttributList = new List<ValeurAttribut>() { fakeValeurAttributOne, fakeValeurAttributTwo };
        var mockDbData = ValeurAttributList.AsQueryable().BuildMock();

        _valeurAttributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetValeurAttributList.Query(queryParameters);
        var handler = new GetValeurAttributList.Handler(_valeurAttributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeValeurAttributTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeValeurAttributOne, options =>
                options.ExcludingMissingMembers());
    }
}