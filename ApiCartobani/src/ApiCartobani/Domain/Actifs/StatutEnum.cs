namespace ApiCartobani.Domain.Actifs;

using Ardalis.SmartEnum;

public abstract class StatutEnum : SmartEnum<StatutEnum>
{
    public static readonly StatutEnum Brouillon = new BrouillonType();
    public static readonly StatutEnum Valide = new ValideType();
    public static readonly StatutEnum Decommisionne = new DecommisionneType();

    protected StatutEnum(string name, int value) : base(name, value)
    {
    }

    private class BrouillonType : StatutEnum
    {
        public BrouillonType() : base("Brouillon", 0)
        {
        }
    }

    private class ValideType : StatutEnum
    {
        public ValideType() : base("Valide", 1)
        {
        }
    }

    private class DecommisionneType : StatutEnum
    {
        public DecommisionneType() : base("Decommisionne", 2)
        {
        }
    }
}