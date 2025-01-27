using UnityEngine;

public class PiercingBulletUpgrade : MonoBehaviour
{
    private int pierceCounter;
    
    public void PierceUpgrade(int pierceUpgradeValue)
    {
        if (pierceCounter == pierceUpgradeValue)
        {
            if (this != null && gameObject != null)
            {
                //also getting destroied from BulletColour.cs
                //I think causing issues? should I use a static method to destory stuff?
                print("Destory from PiercingBulletUpgrade.cs");
                Destroy(gameObject);
            }
            else
            { 
                Debug.LogWarning("Cant destory form PiercingBulletUpgrade");
            }
        }
        else
        {
            pierceCounter++;
        }
    }
}
