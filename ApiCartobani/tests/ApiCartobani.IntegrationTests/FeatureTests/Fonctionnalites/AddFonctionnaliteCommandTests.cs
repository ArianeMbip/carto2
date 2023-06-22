namespace ApiCartobani.IntegrationTests.FeatureTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Fonctionnalites.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddFonctionnaliteCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_fonctionnalite_to_db()
    {
        // Arrange
        var fakeFonctionnaliteOne = new FakeFonctionnaliteForCreationDto().Generate();

        // Act
        var command = new AddFonctionnalite.Command(fakeFonctionnaliteOne);
        var fonctionnaliteReturned = await SendAsync(command);
        var fonctionnaliteCreated = await ExecuteDbContextAsync(db => db.Fonctionnalites
            .FirstOrDefaultAsync(f => f.Id == fonctionnaliteReturned.Id));

        // Assert
        fonctionnaliteReturned.Nom.Should().Be(fakeFonctionnaliteOne.Nom);
        fonctionnaliteReturned.Type.Should().Be(fakeFonctionnaliteOne.Type);

        fonctionnaliteCreated.Nom.Should().Be(fakeFonctionnaliteOne.Nom);
        fonctionnaliteCreated.Type.Should().Be(fakeFonctionnaliteOne.Type);
    }
}