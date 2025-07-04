using UnityEngine;

public class UpgradeTracker
{
    public int normalUpgrades = 0;
    public int rareUpgrades = 0;
    public int legendaryUpgrades = 0;

    public int GetWeightedUpgradeValue()
    {
        return (normalUpgrades * 5) + (rareUpgrades * 10) + (legendaryUpgrades * 20);
    }
}
