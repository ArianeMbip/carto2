namespace ApiCartobani.Domain.Fonctionnalites;

using Ardalis.SmartEnum;

public abstract class TypeEnum : SmartEnum<TypeEnum>
{
    public static readonly TypeEnum ExigeanceFonctionnelle = new ExigeanceFonctionnelleType();
    public static readonly TypeEnum ExigeanceNonFonctionnelle = new ExigeanceNonFonctionnelleType();

    protected TypeEnum(string name, int value) : base(name, value)
    {
    }

    private class ExigeanceFonctionnelleType : TypeEnum
    {
        public ExigeanceFonctionnelleType() : base("ExigeanceFonctionnelle", 0)
        {
        }
    }

    private class ExigeanceNonFonctionnelleType : TypeEnum
    {
        public ExigeanceNonFonctionnelleType() : base("ExigeanceNonFonctionnelle", 1)
        {
        }
    }
}