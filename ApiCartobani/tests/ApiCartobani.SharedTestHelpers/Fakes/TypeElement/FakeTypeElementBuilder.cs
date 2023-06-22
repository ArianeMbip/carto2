namespace ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.Dtos;

public class FakeTypeElementBuilder
{
    private TypeElementForCreationDto _creationData = new FakeTypeElementForCreationDto().Generate();

    public FakeTypeElementBuilder WithDto(TypeElementForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public TypeElement Build()
    {
        var result = TypeElement.Create(_creationData);
        return result;
    }
}