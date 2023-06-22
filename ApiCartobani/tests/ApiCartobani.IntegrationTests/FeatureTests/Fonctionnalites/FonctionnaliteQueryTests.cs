namespace ApiCartobani.IntegrationTests.FeatureTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.Domain.Fonctionnalites.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class FonctionnaliteQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_fonctionnalite_with_accurate_props()
    {
        // Arrange
        var fakeFonctionnaliteOne = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        await InsertAsync(fakeFonctionnaliteOne);

        // Act
        var query = new GetFonctionnalite.Query(fakeFonctionnaliteOne.Id);
        var fonctionnalite = await SendAsync(query);

        // Assert
        fonctionnalite.Nom.Should().Be(fakeFonctionnaliteOne.Nom);
        fonctionnalite.Type.Should().Be(fakeFonctionnaliteOne.Type);
    }

    [Test]
    public async Task get_fonctionnalite_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetFonctionnalite.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}