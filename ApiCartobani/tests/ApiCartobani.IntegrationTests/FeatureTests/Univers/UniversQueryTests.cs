namespace ApiCartobani.IntegrationTests.FeatureTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.Domain.Univers.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class UniversQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_univers_with_accurate_props()
    {
        // Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        await InsertAsync(fakeUniversOne);

        // Act
        var query = new GetUnivers.Query(fakeUniversOne.Id);
        var univers = await SendAsync(query);

        // Assert
        univers.Nom.Should().Be(fakeUniversOne.Nom);
    }

    [Test]
    public async Task get_univers_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetUnivers.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}