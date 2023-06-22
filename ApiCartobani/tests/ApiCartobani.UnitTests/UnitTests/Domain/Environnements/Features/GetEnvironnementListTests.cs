namespace ApiCartobani.UnitTests.UnitTests.Domain.Environnements.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Domain.Environnements.Mappings;
using ApiCartobani.Domain.Environnements.Features;
using ApiCartobani.Domain.Environnements.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetEnvironnementListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IEnvironnementRepository> _environnementRepository;

    public GetEnvironnementListTests()
    {
        _environnementRepository = new Mock<IEnvironnementRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_environnement()
    {
        //Arrange
        var fakeEnvironnementOne = FakeEnvironnement.Generate();
        var fakeEnvironnementTwo = FakeEnvironnement.Generate();
        var fakeEnvironnementThree = FakeEnvironnement.Generate();
        var environnement = new List<Environnement>();
        environnement.Add(fakeEnvironnementOne);
        environnement.Add(fakeEnvironnementTwo);
        environnement.Add(fakeEnvironnementThree);
        var mockDbData = environnement.AsQueryable().BuildMock();
        
        var queryParameters = new EnvironnementParametersDto() { PageSize = 1, PageNumber = 2 };

        _environnementRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetEnvironnementList.Query(queryParameters);
        var handler = new GetEnvironnementList.Handler(_environnementRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}