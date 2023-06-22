namespace ApiCartobani.IntegrationTests.FeatureTests.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Actifs.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class AddActifCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_actif_to_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        var fakeActifOne = new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate();

        // Act
        var command = new AddActif.Command(fakeActifOne);
        var actifReturned = await SendAsync(command);
        var actifCreated = await ExecuteDbContextAsync(db => db.Actifs
            .FirstOrDefaultAsync(a => a.Id == actifReturned.Id));

        // Assert
        actifReturned.Nom.Should().Be(fakeActifOne.Nom);
        actifReturned.Criticite.Should().Be(fakeActifOne.Criticite);
        actifReturned.Description.Should().Be(fakeActifOne.Description);
        actifReturned.Version.Should().Be(fakeActifOne.Version);
        actifReturned.Icone.Should().Be(fakeActifOne.Icone);
        actifReturned.Statut.Should().Be(fakeActifOne.Statut);
        actifReturned.TypeActif.Should().Be(fakeActifOne.TypeActif);
        actifReturned.PreVersion.Should().Be(fakeActifOne.PreVersion);
        actifReturned.Hierarchie.Should().Be(fakeActifOne.Hierarchie);

        actifCreated.Nom.Should().Be(fakeActifOne.Nom);
        actifCreated.Criticite.Should().Be(fakeActifOne.Criticite);
        actifCreated.Description.Should().Be(fakeActifOne.Description);
        actifCreated.Version.Should().Be(fakeActifOne.Version);
        actifCreated.Icone.Should().Be(fakeActifOne.Icone);
        actifCreated.Statut.Should().Be(fakeActifOne.Statut);
        actifCreated.TypeActif.Should().Be(fakeActifOne.TypeActif);
        actifCreated.PreVersion.Should().Be(fakeActifOne.PreVersion);
        actifCreated.Hierarchie.Should().Be(fakeActifOne.Hierarchie);
    }
}