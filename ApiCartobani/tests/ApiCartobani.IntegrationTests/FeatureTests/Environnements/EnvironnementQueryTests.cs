namespace ApiCartobani.IntegrationTests.FeatureTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.Domain.Environnements.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class EnvironnementQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_environnement_with_accurate_props()
    {
        // Arrange
        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        // Act
        var query = new GetEnvironnement.Query(fakeEnvironnementOne.Id);
        var environnement = await SendAsync(query);

        // Assert
        environnement.Nom.Should().Be(fakeEnvironnementOne.Nom);
    }

    [Test]
    public async Task get_environnement_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetEnvironnement.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}