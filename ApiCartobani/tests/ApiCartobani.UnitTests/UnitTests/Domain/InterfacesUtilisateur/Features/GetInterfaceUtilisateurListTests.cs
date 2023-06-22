namespace ApiCartobani.UnitTests.UnitTests.Domain.InterfacesUtilisateur.Features;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Domain.InterfacesUtilisateur.Mappings;
using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using ApiCartobani.Domain.InterfacesUtilisateur.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetInterfaceUtilisateurListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IInterfaceUtilisateurRepository> _interfaceUtilisateurRepository;

    public GetInterfaceUtilisateurListTests()
    {
        _interfaceUtilisateurRepository = new Mock<IInterfaceUtilisateurRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_interfaceUtilisateur()
    {
        //Arrange
        var fakeInterfaceUtilisateurOne = FakeInterfaceUtilisateur.Generate();
        var fakeInterfaceUtilisateurTwo = FakeInterfaceUtilisateur.Generate();
        var fakeInterfaceUtilisateurThree = FakeInterfaceUtilisateur.Generate();
        var interfaceUtilisateur = new List<InterfaceUtilisateur>();
        interfaceUtilisateur.Add(fakeInterfaceUtilisateurOne);
        interfaceUtilisateur.Add(fakeInterfaceUtilisateurTwo);
        interfaceUtilisateur.Add(fakeInterfaceUtilisateurThree);
        var mockDbData = interfaceUtilisateur.AsQueryable().BuildMock();
        
        var queryParameters = new InterfaceUtilisateurParametersDto() { PageSize = 1, PageNumber = 2 };

        _interfaceUtilisateurRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetInterfaceUtilisateurList.Query(queryParameters);
        var handler = new GetInterfaceUtilisateurList.Handler(_interfaceUtilisateurRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}