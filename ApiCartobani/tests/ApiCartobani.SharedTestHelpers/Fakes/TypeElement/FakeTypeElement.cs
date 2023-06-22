namespace ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

using AutoBogus;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.Dtos;

public sealed class FakeTypeElement
{
    public static TypeElement Generate(TypeElementForCreationDto typeElementForCreationDto)
    {
        return TypeElement.Create(typeElementForCreationDto);
    }

    public static TypeElement Generate()
    {
        return Generate(new FakeTypeElementForCreationDto().Generate());
    }
}