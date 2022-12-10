public enum PotionType
{
    HPRestoration,
    MPRestoration,
    Strength,
    Weakness,
    Swiftness,
    Slowness,
    Invisibility,
}

public class PotionObject : ItemObject
{
    public PotionType potionType;
}
