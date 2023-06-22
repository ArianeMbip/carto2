namespace ApiCartobani.IntegrationTests.FeatureTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.Domain.Icones.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class IconeQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_icone_with_accurate_props()
    {
        // Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        await InsertAsync(fakeIconeOne);

        // Act
        var query = new GetIcone.Query(fakeIconeOne.Id);
        var icone = await SendAsync(query);

        // Assert
        icone.Url.Should().Be(fakeIconeOne.Url);
    }

    [Test]
    public async Task get_icone_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetIcone.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}