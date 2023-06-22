namespace ApiCartobani.UnitTests.UnitTests.Domain.Icones.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Domain.Icones.Mappings;
using ApiCartobani.Domain.Icones.Features;
using ApiCartobani.Domain.Icones.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetIconeListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IIconeRepository> _iconeRepository;

    public GetIconeListTests()
    {
        _iconeRepository = new Mock<IIconeRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_icone()
    {
        //Arrange
        var fakeIconeOne = FakeIcone.Generate();
        var fakeIconeTwo = FakeIcone.Generate();
        var fakeIconeThree = FakeIcone.Generate();
        var icone = new List<Icone>();
        icone.Add(fakeIconeOne);
        icone.Add(fakeIconeTwo);
        icone.Add(fakeIconeThree);
        var mockDbData = icone.AsQueryable().BuildMock();
        
        var queryParameters = new IconeParametersDto() { PageSize = 1, PageNumber = 2 };

        _iconeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetIconeList.Query(queryParameters);
        var handler = new GetIconeList.Handler(_iconeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_icone_list_using_Url()
    {
        //Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto()
            .RuleFor(i => i.Url, _ => "alpha")
            .Generate());
        var fakeIconeTwo = FakeIcone.Generate(new FakeIconeForCreationDto()
            .RuleFor(i => i.Url, _ => "bravo")
            .Generate());
        var queryParameters = new IconeParametersDto() { Filters = $"Url == {fakeIconeTwo.Url}" };

        var iconeList = new List<Icone>() { fakeIconeOne, fakeIconeTwo };
        var mockDbData = iconeList.AsQueryable().BuildMock();

        _iconeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetIconeList.Query(queryParameters);
        var handler = new GetIconeList.Handler(_iconeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIconeTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_icone_by_Url()
    {
        //Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto()
            .RuleFor(i => i.Url, _ => "alpha")
            .Generate());
        var fakeIconeTwo = FakeIcone.Generate(new FakeIconeForCreationDto()
            .RuleFor(i => i.Url, _ => "bravo")
            .Generate());
        var queryParameters = new IconeParametersDto() { SortOrder = "-Url" };

        var IconeList = new List<Icone>() { fakeIconeOne, fakeIconeTwo };
        var mockDbData = IconeList.AsQueryable().BuildMock();

        _iconeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetIconeList.Query(queryParameters);
        var handler = new GetIconeList.Handler(_iconeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeIconeTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIconeOne, options =>
                options.ExcludingMissingMembers());
    }
}