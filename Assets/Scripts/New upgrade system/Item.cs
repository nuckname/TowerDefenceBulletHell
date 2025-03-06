using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public abstract string GiveName();

    public virtual GameObject UpdateBulletPrefab(GameObject bullet, int stacks)
    {
        return null;
    }

    public virtual void UpdateTurretPrefab(TurretStats turret, int stacks)
    {
        
    }
}

public class IncreaseAttackSpeed : Item
{
    public override string GiveName()
    {
        return "IncreaseAttackSpeed";
    }

    public override GameObject UpdateBulletPrefab(GameObject bulletPrefab, int stacks)
    {
        Debug.Log("Increase Bullet Speed");
        Debug.Log("Get this once");
        
        GameObject modifiedBullet = GameObject.Instantiate(bulletPrefab);
        
        BulletStats bulletStats = modifiedBullet.GetComponent<BulletStats>();
        if (bulletStats != null)
        {
            bulletStats.bulletSpeed += 1.2f * stacks;
        }

        return modifiedBullet;

    }
}


public class FireExtraProjectile : Item
{
    public override string GiveName()
    {
        return "FireExtraProjectile";
    }

    public override void UpdateTurretPrefab(TurretStats turret, int stacks)
    {
        Debug.Log("Extra Projectile");
        Debug.Log("Get this once");

        turret.extraProjectiles++;
        

    }
}
