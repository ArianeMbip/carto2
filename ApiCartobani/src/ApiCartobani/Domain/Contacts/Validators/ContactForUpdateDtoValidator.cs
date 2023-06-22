namespace ApiCartobani.Domain.Contacts.Validators;

using ApiCartobani.Domain.Contacts.Dtos;
using FluentValidation;

public sealed class ContactForUpdateDtoValidator: ContactForManipulationDtoValidator<ContactForUpdateDto>
{
    public ContactForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}