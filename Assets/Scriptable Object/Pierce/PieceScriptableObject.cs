using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PiercingProjectilesUpgrade", menuName = "Upgrades/Effects/PiercingProjectiles")]
public class PieceScriptableObject : UpgradeEffect
{
    public int maxPierces = 1;

    public override void Apply(GameObject targetTurret)
    {
        //display current pierce in descrtiption"? 
        maxPierces++;
        /*
        PierceTurretUpgrade turret = targetTurret.GetComponentInChildren<PierceTurretUpgrade>();
        if (turret != null)
        {
            turret.EnablePiercing(maxPierces);
        }
        else
        {
            Debug.LogWarning("Target turret does not have a Turret component!");
        }
        */
    }

}
