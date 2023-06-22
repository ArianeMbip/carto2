namespace ApiCartobani.IntegrationTests.FeatureTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.Domain.ValeurAttributs.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;

public class DeleteValeurAttributCommandTests : TestBase
{
    [Test]
    public async Task can_delete_valeurattribut_from_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate());
        await InsertAsync(fakeValeurAttributOne);
        var valeurAttribut = await ExecuteDbContextAsync(db => db.ValeurAttributs
            .FirstOrDefaultAsync(v => v.Id == fakeValeurAttributOne.Id));

        // Act
        var command = new DeleteValeurAttribut.Command(valeurAttribut.Id);
        await SendAsync(command);
        var valeurAttributResponse = await ExecuteDbContextAsync(db => db.ValeurAttributs.CountAsync(v => v.Id == valeurAttribut.Id));

        // Assert
        valeurAttributResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_valeurattribut_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteValeurAttribut.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_valeurattribut_from_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate());
        await InsertAsync(fakeValeurAttributOne);
        var valeurAttribut = await ExecuteDbContextAsync(db => db.ValeurAttributs
            .FirstOrDefaultAsync(v => v.Id == fakeValeurAttributOne.Id));

        // Act
        var command = new DeleteValeurAttribut.Command(valeurAttribut.Id);
        await SendAsync(command);
        var deletedValeurAttribut = await ExecuteDbContextAsync(db => db.ValeurAttributs
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == valeurAttribut.Id));

        // Assert
        deletedValeurAttribut?.IsDeleted.Should().BeTrue();
    }
}