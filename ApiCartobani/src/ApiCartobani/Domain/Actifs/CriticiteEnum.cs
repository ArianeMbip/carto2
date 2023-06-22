namespace ApiCartobani.Domain.Actifs;

using Ardalis.SmartEnum;

public abstract class CriticiteEnum : SmartEnum<CriticiteEnum>
{
    public static readonly CriticiteEnum Basse = new BasseType();
    public static readonly CriticiteEnum Moyenne = new MoyenneType();
    public static readonly CriticiteEnum Haute = new HauteType();
    public static readonly CriticiteEnum Critique = new CritiqueType();

    protected CriticiteEnum(string name, int value) : base(name, value)
    {
    }

    private class BasseType : CriticiteEnum
    {
        public BasseType() : base("Basse", 0)
        {
        }
    }

    private class MoyenneType : CriticiteEnum
    {
        public MoyenneType() : base("Moyenne", 1)
        {
        }
    }

    private class HauteType : CriticiteEnum
    {
        public HauteType() : base("Haute", 2)
        {
        }
    }

    private class CritiqueType : CriticiteEnum
    {
        public CritiqueType() : base("Critique", 3)
        {
        }
    }
}