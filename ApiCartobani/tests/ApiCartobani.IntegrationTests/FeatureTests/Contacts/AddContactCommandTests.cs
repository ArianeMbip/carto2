namespace ApiCartobani.IntegrationTests.FeatureTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Contacts.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddContactCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_contact_to_db()
    {
        // Arrange
        var fakeContactOne = new FakeContactForCreationDto().Generate();

        // Act
        var command = new AddContact.Command(fakeContactOne);
        var contactReturned = await SendAsync(command);
        var contactCreated = await ExecuteDbContextAsync(db => db.Contacts
            .FirstOrDefaultAsync(c => c.Id == contactReturned.Id));

        // Assert
        contactReturned.Nom.Should().Be(fakeContactOne.Nom);
        contactReturned.Email.Should().Be(fakeContactOne.Email);
        contactReturned.Entite.Should().Be(fakeContactOne.Entite);
        contactReturned.Fonction.Should().Be(fakeContactOne.Fonction);
        contactReturned.Telephone.Should().Be(fakeContactOne.Telephone);

        contactCreated.Nom.Should().Be(fakeContactOne.Nom);
        contactCreated.Email.Should().Be(fakeContactOne.Email);
        contactCreated.Entite.Should().Be(fakeContactOne.Entite);
        contactCreated.Fonction.Should().Be(fakeContactOne.Fonction);
        contactCreated.Telephone.Should().Be(fakeContactOne.Telephone);
    }
}