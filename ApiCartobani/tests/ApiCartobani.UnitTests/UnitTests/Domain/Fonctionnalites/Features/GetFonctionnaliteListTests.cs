namespace ApiCartobani.UnitTests.UnitTests.Domain.Fonctionnalites.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Domain.Fonctionnalites.Mappings;
using ApiCartobani.Domain.Fonctionnalites.Features;
using ApiCartobani.Domain.Fonctionnalites.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetFonctionnaliteListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IFonctionnaliteRepository> _fonctionnaliteRepository;

    public GetFonctionnaliteListTests()
    {
        _fonctionnaliteRepository = new Mock<IFonctionnaliteRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_fonctionnalite()
    {
        //Arrange
        var fakeFonctionnaliteOne = FakeFonctionnalite.Generate();
        var fakeFonctionnaliteTwo = FakeFonctionnalite.Generate();
        var fakeFonctionnaliteThree = FakeFonctionnalite.Generate();
        var fonctionnalite = new List<Fonctionnalite>();
        fonctionnalite.Add(fakeFonctionnaliteOne);
        fonctionnalite.Add(fakeFonctionnaliteTwo);
        fonctionnalite.Add(fakeFonctionnaliteThree);
        var mockDbData = fonctionnalite.AsQueryable().BuildMock();
        
        var queryParameters = new FonctionnaliteParametersDto() { PageSize = 1, PageNumber = 2 };

        _fonctionnaliteRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetFonctionnaliteList.Query(queryParameters);
        var handler = new GetFonctionnaliteList.Handler(_fonctionnaliteRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}