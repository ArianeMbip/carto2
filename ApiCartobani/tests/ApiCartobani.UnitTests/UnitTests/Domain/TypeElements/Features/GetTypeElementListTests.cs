namespace ApiCartobani.UnitTests.UnitTests.Domain.TypeElements.Features;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Domain.TypeElements.Mappings;
using ApiCartobani.Domain.TypeElements.Features;
using ApiCartobani.Domain.TypeElements.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetTypeElementListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<ITypeElementRepository> _typeElementRepository;

    public GetTypeElementListTests()
    {
        _typeElementRepository = new Mock<ITypeElementRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_typeElement()
    {
        //Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate();
        var fakeTypeElementTwo = FakeTypeElement.Generate();
        var fakeTypeElementThree = FakeTypeElement.Generate();
        var typeElement = new List<TypeElement>();
        typeElement.Add(fakeTypeElementOne);
        typeElement.Add(fakeTypeElementTwo);
        typeElement.Add(fakeTypeElementThree);
        var mockDbData = typeElement.AsQueryable().BuildMock();
        
        var queryParameters = new TypeElementParametersDto() { PageSize = 1, PageNumber = 2 };

        _typeElementRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetTypeElementList.Query(queryParameters);
        var handler = new GetTypeElementList.Handler(_typeElementRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_typeelement_list_using_Nom()
    {
        //Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto()
            .RuleFor(t => t.Nom, _ => "alpha")
            .Generate());
        var fakeTypeElementTwo = FakeTypeElement.Generate(new FakeTypeElementForCreationDto()
            .RuleFor(t => t.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new TypeElementParametersDto() { Filters = $"Nom == {fakeTypeElementTwo.Nom}" };

        var typeElementList = new List<TypeElement>() { fakeTypeElementOne, fakeTypeElementTwo };
        var mockDbData = typeElementList.AsQueryable().BuildMock();

        _typeElementRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetTypeElementList.Query(queryParameters);
        var handler = new GetTypeElementList.Handler(_typeElementRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeTypeElementTwo, options =>
                options.ExcludingMissingMembers());
    }



    [Test]
    public async Task can_get_sorted_list_of_typeelement_by_Nom()
    {
        //Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto()
            .RuleFor(t => t.Nom, _ => "alpha")
            .Generate());
        var fakeTypeElementTwo = FakeTypeElement.Generate(new FakeTypeElementForCreationDto()
            .RuleFor(t => t.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new TypeElementParametersDto() { SortOrder = "-Nom" };

        var TypeElementList = new List<TypeElement>() { fakeTypeElementOne, fakeTypeElementTwo };
        var mockDbData = TypeElementList.AsQueryable().BuildMock();

        _typeElementRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetTypeElementList.Query(queryParameters);
        var handler = new GetTypeElementList.Handler(_typeElementRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeTypeElementTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeTypeElementOne, options =>
                options.ExcludingMissingMembers());
    }


}