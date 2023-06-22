namespace ApiCartobani.IntegrationTests.FeatureTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.Domain.Contacts.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Contacts.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateContactCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_contact_in_db()
    {
        // Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        var updatedContactDto = new FakeContactForUpdateDto().Generate();
        await InsertAsync(fakeContactOne);

        var contact = await ExecuteDbContextAsync(db => db.Contacts
            .FirstOrDefaultAsync(c => c.Id == fakeContactOne.Id));
        var id = contact.Id;

        // Act
        var command = new UpdateContact.Command(id, updatedContactDto);
        await SendAsync(command);
        var updatedContact = await ExecuteDbContextAsync(db => db.Contacts.FirstOrDefaultAsync(c => c.Id == id));

        // Assert
        updatedContact.Nom.Should().Be(updatedContactDto.Nom);
        updatedContact.Email.Should().Be(updatedContactDto.Email);
        updatedContact.Entite.Should().Be(updatedContactDto.Entite);
        updatedContact.Fonction.Should().Be(updatedContactDto.Fonction);
        updatedContact.Telephone.Should().Be(updatedContactDto.Telephone);
    }
}