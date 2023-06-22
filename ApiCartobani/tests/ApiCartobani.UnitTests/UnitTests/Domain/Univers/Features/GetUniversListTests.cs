namespace ApiCartobani.UnitTests.UnitTests.Domain.Univers.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.Domain.Univers;
using ApiCartobani.Domain.Univers.Dtos;
using ApiCartobani.Domain.Univers.Mappings;
using ApiCartobani.Domain.Univers.Features;
using ApiCartobani.Domain.Univers.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetUniversListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IUniversRepository> _universRepository;

    public GetUniversListTests()
    {
        _universRepository = new Mock<IUniversRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_univers()
    {
        //Arrange
        var fakeUniversOne = FakeUnivers.Generate();
        var fakeUniversTwo = FakeUnivers.Generate();
        var fakeUniversThree = FakeUnivers.Generate();
        var univers = new List<Univers>();
        univers.Add(fakeUniversOne);
        univers.Add(fakeUniversTwo);
        univers.Add(fakeUniversThree);
        var mockDbData = univers.AsQueryable().BuildMock();
        
        var queryParameters = new UniversParametersDto() { PageSize = 1, PageNumber = 2 };

        _universRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetUniversList.Query(queryParameters);
        var handler = new GetUniversList.Handler(_universRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_univers_list_using_Nom()
    {
        //Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto()
            .RuleFor(u => u.Nom, _ => "alpha")
            .Generate());
        var fakeUniversTwo = FakeUnivers.Generate(new FakeUniversForCreationDto()
            .RuleFor(u => u.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new UniversParametersDto() { Filters = $"Nom == {fakeUniversTwo.Nom}" };

        var universList = new List<Univers>() { fakeUniversOne, fakeUniversTwo };
        var mockDbData = universList.AsQueryable().BuildMock();

        _universRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUniversList.Query(queryParameters);
        var handler = new GetUniversList.Handler(_universRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUniversTwo, options =>
                options.ExcludingMissingMembers());
    }



    [Test]
    public async Task can_get_sorted_list_of_univers_by_Nom()
    {
        //Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto()
            .RuleFor(u => u.Nom, _ => "alpha")
            .Generate());
        var fakeUniversTwo = FakeUnivers.Generate(new FakeUniversForCreationDto()
            .RuleFor(u => u.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new UniversParametersDto() { SortOrder = "-Nom" };

        var UniversList = new List<Univers>() { fakeUniversOne, fakeUniversTwo };
        var mockDbData = UniversList.AsQueryable().BuildMock();

        _universRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUniversList.Query(queryParameters);
        var handler = new GetUniversList.Handler(_universRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeUniversTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUniversOne, options =>
                options.ExcludingMissingMembers());
    }


}