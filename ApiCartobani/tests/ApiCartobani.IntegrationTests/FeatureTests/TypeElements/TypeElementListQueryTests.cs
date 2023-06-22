namespace ApiCartobani.IntegrationTests.FeatureTests.TypeElements;

using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.TypeElements.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class TypeElementListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_typeelement_list()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var fakeTypeElementTwo = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var queryParameters = new TypeElementParametersDto();

        await InsertAsync(fakeTypeElementOne, fakeTypeElementTwo);

        // Act
        var query = new GetTypeElementList.Query(queryParameters);
        var typeElements = await SendAsync(query);

        // Assert
        typeElements.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}