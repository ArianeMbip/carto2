namespace ApiCartobani.IntegrationTests.FeatureTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.ValeurAttributs.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;

public class AddValeurAttributCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_valeurattribut_to_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        var fakeValeurAttributOne = new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate();

        // Act
        var command = new AddValeurAttribut.Command(fakeValeurAttributOne);
        var valeurAttributReturned = await SendAsync(command);
        var valeurAttributCreated = await ExecuteDbContextAsync(db => db.ValeurAttributs
            .FirstOrDefaultAsync(v => v.Id == valeurAttributReturned.Id));

        // Assert
        valeurAttributReturned.Valeur.Should().Be(fakeValeurAttributOne.Valeur);
        valeurAttributReturned.Attribut.Should().Be(fakeValeurAttributOne.Attribut);
        valeurAttributReturned.Environnement.Should().Be(fakeValeurAttributOne.Environnement);

        valeurAttributCreated.Valeur.Should().Be(fakeValeurAttributOne.Valeur);
        valeurAttributCreated.Attribut.Should().Be(fakeValeurAttributOne.Attribut);
        valeurAttributCreated.Environnement.Should().Be(fakeValeurAttributOne.Environnement);
    }
}