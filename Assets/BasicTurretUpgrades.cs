using UnityEngine;

public class BasicTurretUpgrades : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string[] Upgrades;

    [SerializeField] private float modifierFireRateValue = 0.25f;
    [SerializeField] private float modifierBulletLifeTimeValue = 0.25f;
    [SerializeField] private float modifierBulletSpeedValue = 0.2f;
    
    private TurretShoot _turretShoot;
    void Start()
    {
        Upgrades = new string[3];
        _turretShoot = GetComponent<TurretShoot>();

    }



    public string[] TierOneText_BasicTurretUpgrades()
    {
        Upgrades[0] = "Increase Firerate";
        Upgrades[1] = "Increase Bullet Life Time";
        Upgrades[2] = "Increase Bullet Speed";

        return Upgrades;
    }
    //tier 2
    

    public void GetTurretInfomation(string UpgradeSelected)
    {
        if (UpgradeSelected == "Increase Firerate")
        {
            // - gold cost
            
            _turretShoot.modifierFireRate += modifierFireRateValue;
            print("upgrade fire rate");
        }
        
        if (UpgradeSelected == "Increase Bullet Life Time")
        {
            _turretShoot.modifierBulletLifeTime += modifierBulletLifeTimeValue;
            print("upgrade bullet life time");

        }
        
        if (UpgradeSelected == "Increase Bullet Speed")
        {
            _turretShoot.modifierBulletSpeed += modifierBulletSpeedValue;
        }

        
    }


}
