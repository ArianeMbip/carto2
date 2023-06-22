namespace ApiCartobani.UnitTests.UnitTests.Domain.Attributs.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Attributs.Mappings;
using ApiCartobani.Domain.Attributs.Features;
using ApiCartobani.Domain.Attributs.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetAttributListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IAttributRepository> _attributRepository;

    public GetAttributListTests()
    {
        _attributRepository = new Mock<IAttributRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_attribut()
    {
        //Arrange
        var fakeAttributOne = FakeAttribut.Generate();
        var fakeAttributTwo = FakeAttribut.Generate();
        var fakeAttributThree = FakeAttribut.Generate();
        var attribut = new List<Attribut>();
        attribut.Add(fakeAttributOne);
        attribut.Add(fakeAttributTwo);
        attribut.Add(fakeAttributThree);
        var mockDbData = attribut.AsQueryable().BuildMock();
        
        var queryParameters = new AttributParametersDto() { PageSize = 1, PageNumber = 2 };

        _attributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetAttributList.Query(queryParameters);
        var handler = new GetAttributList.Handler(_attributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_attribut_list_using_Nom()
    {
        //Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto()
            .RuleFor(a => a.Nom, _ => "alpha")
            .Generate());
        var fakeAttributTwo = FakeAttribut.Generate(new FakeAttributForCreationDto()
            .RuleFor(a => a.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new AttributParametersDto() { Filters = $"Nom == {fakeAttributTwo.Nom}" };

        var attributList = new List<Attribut>() { fakeAttributOne, fakeAttributTwo };
        var mockDbData = attributList.AsQueryable().BuildMock();

        _attributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetAttributList.Query(queryParameters);
        var handler = new GetAttributList.Handler(_attributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeAttributTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_attribut_list_using_Requis()
    {
        //Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto()
            .RuleFor(a => a.Requis, _ => false)
            .Generate());
        var fakeAttributTwo = FakeAttribut.Generate(new FakeAttributForCreationDto()
            .RuleFor(a => a.Requis, _ => true)
            .Generate());
        var queryParameters = new AttributParametersDto() { Filters = $"Requis == {fakeAttributTwo.Requis}" };

        var attributList = new List<Attribut>() { fakeAttributOne, fakeAttributTwo };
        var mockDbData = attributList.AsQueryable().BuildMock();

        _attributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetAttributList.Query(queryParameters);
        var handler = new GetAttributList.Handler(_attributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeAttributTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_attribut_by_Nom()
    {
        //Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto()
            .RuleFor(a => a.Nom, _ => "alpha")
            .Generate());
        var fakeAttributTwo = FakeAttribut.Generate(new FakeAttributForCreationDto()
            .RuleFor(a => a.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new AttributParametersDto() { SortOrder = "-Nom" };

        var AttributList = new List<Attribut>() { fakeAttributOne, fakeAttributTwo };
        var mockDbData = AttributList.AsQueryable().BuildMock();

        _attributRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetAttributList.Query(queryParameters);
        var handler = new GetAttributList.Handler(_attributRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeAttributTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeAttributOne, options =>
                options.ExcludingMissingMembers());
    }


}