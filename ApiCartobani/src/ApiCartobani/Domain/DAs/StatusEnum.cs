namespace ApiCartobani.Domain.DAs;

using Ardalis.SmartEnum;

public abstract class StatusEnum : SmartEnum<StatusEnum>
{
    public static readonly StatusEnum Brouillon = new BrouillonType();
    public static readonly StatusEnum Valide = new ValideType();
    public static readonly StatusEnum Decommissionne = new DecommissionneType();

    protected StatusEnum(string name, int value) : base(name, value)
    {
    }

    private class BrouillonType : StatusEnum
    {
        public BrouillonType() : base("Brouillon", 0)
        {
        }
    }

    private class ValideType : StatusEnum
    {
        public ValideType() : base("Valide", 1)
        {
        }
    }

    private class DecommissionneType : StatusEnum
    {
        public DecommissionneType() : base("Decommissionne", 2)
        {
        }
    }
}