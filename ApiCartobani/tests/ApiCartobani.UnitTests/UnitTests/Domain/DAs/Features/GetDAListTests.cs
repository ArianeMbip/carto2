namespace ApiCartobani.UnitTests.UnitTests.Domain.DAs.Features;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Domain.DAs.Mappings;
using ApiCartobani.Domain.DAs.Features;
using ApiCartobani.Domain.DAs.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetDAListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IDARepository> _dARepository;

    public GetDAListTests()
    {
        _dARepository = new Mock<IDARepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_dA()
    {
        //Arrange
        var fakeDAOne = FakeDA.Generate();
        var fakeDATwo = FakeDA.Generate();
        var fakeDAThree = FakeDA.Generate();
        var dA = new List<DA>();
        dA.Add(fakeDAOne);
        dA.Add(fakeDATwo);
        dA.Add(fakeDAThree);
        var mockDbData = dA.AsQueryable().BuildMock();
        
        var queryParameters = new DAParametersDto() { PageSize = 1, PageNumber = 2 };

        _dARepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetDAList.Query(queryParameters);
        var handler = new GetDAList.Handler(_dARepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}