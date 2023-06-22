namespace ApiCartobani.IntegrationTests.FeatureTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.Domain.Contacts.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class ContactQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_contact_with_accurate_props()
    {
        // Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        await InsertAsync(fakeContactOne);

        // Act
        var query = new GetContact.Query(fakeContactOne.Id);
        var contact = await SendAsync(query);

        // Assert
        contact.Nom.Should().Be(fakeContactOne.Nom);
        contact.Email.Should().Be(fakeContactOne.Email);
        contact.Entite.Should().Be(fakeContactOne.Entite);
        contact.Fonction.Should().Be(fakeContactOne.Fonction);
        contact.Telephone.Should().Be(fakeContactOne.Telephone);
    }

    [Test]
    public async Task get_contact_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetContact.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}