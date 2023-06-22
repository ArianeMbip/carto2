namespace ApiCartobani.IntegrationTests.FeatureTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.Domain.Contacts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteContactCommandTests : TestBase
{
    [Test]
    public async Task can_delete_contact_from_db()
    {
        // Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        await InsertAsync(fakeContactOne);
        var contact = await ExecuteDbContextAsync(db => db.Contacts
            .FirstOrDefaultAsync(c => c.Id == fakeContactOne.Id));

        // Act
        var command = new DeleteContact.Command(contact.Id);
        await SendAsync(command);
        var contactResponse = await ExecuteDbContextAsync(db => db.Contacts.CountAsync(c => c.Id == contact.Id));

        // Assert
        contactResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_contact_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteContact.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_contact_from_db()
    {
        // Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        await InsertAsync(fakeContactOne);
        var contact = await ExecuteDbContextAsync(db => db.Contacts
            .FirstOrDefaultAsync(c => c.Id == fakeContactOne.Id));

        // Act
        var command = new DeleteContact.Command(contact.Id);
        await SendAsync(command);
        var deletedContact = await ExecuteDbContextAsync(db => db.Contacts
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == contact.Id));

        // Assert
        deletedContact?.IsDeleted.Should().BeTrue();
    }
}