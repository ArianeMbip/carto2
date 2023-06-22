namespace ApiCartobani.IntegrationTests.FeatureTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.Domain.TypeElements.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteTypeElementCommandTests : TestBase
{
    [Test]
    public async Task can_delete_typeelement_from_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);
        var typeElement = await ExecuteDbContextAsync(db => db.TypeElements
            .FirstOrDefaultAsync(t => t.Id == fakeTypeElementOne.Id));

        // Act
        var command = new DeleteTypeElement.Command(typeElement.Id);
        await SendAsync(command);
        var typeElementResponse = await ExecuteDbContextAsync(db => db.TypeElements.CountAsync(t => t.Id == typeElement.Id));

        // Assert
        typeElementResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_typeelement_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteTypeElement.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_typeelement_from_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);
        var typeElement = await ExecuteDbContextAsync(db => db.TypeElements
            .FirstOrDefaultAsync(t => t.Id == fakeTypeElementOne.Id));

        // Act
        var command = new DeleteTypeElement.Command(typeElement.Id);
        await SendAsync(command);
        var deletedTypeElement = await ExecuteDbContextAsync(db => db.TypeElements
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == typeElement.Id));

        // Assert
        deletedTypeElement?.IsDeleted.Should().BeTrue();
    }
}