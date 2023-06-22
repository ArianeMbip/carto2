namespace ApiCartobani.Domain.TypeElements;

using Ardalis.SmartEnum;

public abstract class TypeEltEnum : SmartEnum<TypeEltEnum>
{
    public static readonly TypeEltEnum TypeActif = new TypeActifType();
    public static readonly TypeEltEnum TypeFlux = new TypeFluxType();
    public static readonly TypeEltEnum TypeComposantLogiciel = new TypeComposantLogicielType();
    public static readonly TypeEltEnum TypeComposantMateriel = new TypeComposantMaterielType();

    protected TypeEltEnum(string name, int value) : base(name, value)
    {
    }

    private class TypeActifType : TypeEltEnum
    {
        public TypeActifType() : base("TypeActif", 0)
        {
        }
    }

    private class TypeFluxType : TypeEltEnum
    {
        public TypeFluxType() : base("TypeFlux", 1)
        {
        }
    }

    private class TypeComposantLogicielType : TypeEltEnum
    {
        public TypeComposantLogicielType() : base("TypeComposantLogiciel", 2)
        {
        }
    }

    private class TypeComposantMaterielType : TypeEltEnum
    {
        public TypeComposantMaterielType() : base("TypeComposantMateriel", 3)
        {
        }
    }
}