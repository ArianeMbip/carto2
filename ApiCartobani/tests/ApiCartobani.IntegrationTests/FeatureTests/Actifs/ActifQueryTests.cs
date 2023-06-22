namespace ApiCartobani.IntegrationTests.FeatureTests.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.Domain.Actifs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class ActifQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_actif_with_accurate_props()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate());
        await InsertAsync(fakeActifOne);

        // Act
        var query = new GetActif.Query(fakeActifOne.Id);
        var actif = await SendAsync(query);

        // Assert
        actif.Nom.Should().Be(fakeActifOne.Nom);
        actif.Criticite.Should().Be(fakeActifOne.Criticite);
        actif.Description.Should().Be(fakeActifOne.Description);
        actif.Version.Should().Be(fakeActifOne.Version);
        actif.Icone.Should().Be(fakeActifOne.Icone);
        actif.Statut.Should().Be(fakeActifOne.Statut);
        actif.TypeActif.Should().Be(fakeActifOne.TypeActif);
        actif.PreVersion.Should().Be(fakeActifOne.PreVersion);
        actif.Hierarchie.Should().Be(fakeActifOne.Hierarchie);
    }

    [Test]
    public async Task get_actif_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetActif.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}