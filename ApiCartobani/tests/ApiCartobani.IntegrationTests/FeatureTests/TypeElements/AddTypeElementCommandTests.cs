namespace ApiCartobani.IntegrationTests.FeatureTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.TypeElements.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddTypeElementCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_typeelement_to_db()
    {
        // Arrange
        var fakeTypeElementOne = new FakeTypeElementForCreationDto().Generate();

        // Act
        var command = new AddTypeElement.Command(fakeTypeElementOne);
        var typeElementReturned = await SendAsync(command);
        var typeElementCreated = await ExecuteDbContextAsync(db => db.TypeElements
            .FirstOrDefaultAsync(t => t.Id == typeElementReturned.Id));

        // Assert
        typeElementReturned.Nom.Should().Be(fakeTypeElementOne.Nom);
        typeElementReturned.Type.Should().Be(fakeTypeElementOne.Type);
        typeElementReturned.Icone.Should().Be(fakeTypeElementOne.Icone);

        typeElementCreated.Nom.Should().Be(fakeTypeElementOne.Nom);
        typeElementCreated.Type.Should().Be(fakeTypeElementOne.Type);
        typeElementCreated.Icone.Should().Be(fakeTypeElementOne.Icone);
    }
}