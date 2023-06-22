namespace ApiCartobani.IntegrationTests.FeatureTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.Domain.TypeElements.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class TypeElementQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_typeelement_with_accurate_props()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        // Act
        var query = new GetTypeElement.Query(fakeTypeElementOne.Id);
        var typeElement = await SendAsync(query);

        // Assert
        typeElement.Nom.Should().Be(fakeTypeElementOne.Nom);
        typeElement.Type.Should().Be(fakeTypeElementOne.Type);
        typeElement.Icone.Should().Be(fakeTypeElementOne.Icone);
    }

    [Test]
    public async Task get_typeelement_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetTypeElement.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}