namespace ApiCartobani.UnitTests.UnitTests.Domain.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateRolePrivilegeTests
{
    private readonly Faker _faker;

    public UpdateRolePrivilegeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_rolePrivilege()
    {
        // Arrange
        var fakeRolePrivilege = FakeRolePrivilege.Generate();
        var updatedRolePrivilege = new FakeRolePrivilegeForUpdateDto().Generate();
        
        // Act
        fakeRolePrivilege.Update(updatedRolePrivilege);

        // Assert
        fakeRolePrivilege.Nom.Should().Be(updatedRolePrivilege.Nom);
        fakeRolePrivilege.Lire.Should().Be(updatedRolePrivilege.Lire);
        fakeRolePrivilege.Ecrire.Should().Be(updatedRolePrivilege.Ecrire);
        fakeRolePrivilege.Modifier.Should().Be(updatedRolePrivilege.Modifier);
        fakeRolePrivilege.Supprimer.Should().Be(updatedRolePrivilege.Supprimer);
        fakeRolePrivilege.Valider.Should().Be(updatedRolePrivilege.Valider);
        fakeRolePrivilege.Archiver.Should().Be(updatedRolePrivilege.Archiver);
        fakeRolePrivilege.Generer.Should().Be(updatedRolePrivilege.Generer);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeRolePrivilege = FakeRolePrivilege.Generate();
        var updatedRolePrivilege = new FakeRolePrivilegeForUpdateDto().Generate();
        fakeRolePrivilege.DomainEvents.Clear();
        
        // Act
        fakeRolePrivilege.Update(updatedRolePrivilege);

        // Assert
        fakeRolePrivilege.DomainEvents.Count.Should().Be(1);
        fakeRolePrivilege.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(RolePrivilegeUpdated));
    }
}