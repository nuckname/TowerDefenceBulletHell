using UnityEngine;

public class BasicTurretUpgrades : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string[] Upgrades;
    void Start()
    {
        Upgrades = new string[3];
        print("BasicTurretUpgrade");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string[] TierOneText_BasicTurretUpgrades()
    {
        Upgrades[0] = "Increase Damage";
        Upgrades[1] = "Increase Bullet Life Time";
        Upgrades[2] = "Increase Bullet Speed";

        return Upgrades;
    }


}
