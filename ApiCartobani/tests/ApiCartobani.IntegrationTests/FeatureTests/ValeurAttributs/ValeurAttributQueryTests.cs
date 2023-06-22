namespace ApiCartobani.IntegrationTests.FeatureTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.Domain.ValeurAttributs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;

public class ValeurAttributQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_valeurattribut_with_accurate_props()
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

        // Act
        var query = new GetValeurAttribut.Query(fakeValeurAttributOne.Id);
        var valeurAttribut = await SendAsync(query);

        // Assert
        valeurAttribut.Valeur.Should().Be(fakeValeurAttributOne.Valeur);
        valeurAttribut.Attribut.Should().Be(fakeValeurAttributOne.Attribut);
        valeurAttribut.Environnement.Should().Be(fakeValeurAttributOne.Environnement);
    }

    [Test]
    public async Task get_valeurattribut_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetValeurAttribut.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}