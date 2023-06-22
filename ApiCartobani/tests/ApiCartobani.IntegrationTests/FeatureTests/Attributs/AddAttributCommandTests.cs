namespace ApiCartobani.IntegrationTests.FeatureTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Attributs.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddAttributCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_attribut_to_db()
    {
        // Arrange
        var fakeAttributOne = new FakeAttributForCreationDto().Generate();

        // Act
        var command = new AddAttribut.Command(fakeAttributOne);
        var attributReturned = await SendAsync(command);
        var attributCreated = await ExecuteDbContextAsync(db => db.Attributs
            .FirstOrDefaultAsync(a => a.Id == attributReturned.Id));

        // Assert
        attributReturned.Nom.Should().Be(fakeAttributOne.Nom);
        attributReturned.Requis.Should().Be(fakeAttributOne.Requis);
        attributReturned.TypeDonnee.Should().Be(fakeAttributOne.TypeDonnee);

        attributCreated.Nom.Should().Be(fakeAttributOne.Nom);
        attributCreated.Requis.Should().Be(fakeAttributOne.Requis);
        attributCreated.TypeDonnee.Should().Be(fakeAttributOne.TypeDonnee);
    }
}