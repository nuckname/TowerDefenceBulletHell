using System;
using UnityEngine;

public class StoreTurretDescriptionAndRarity : MonoBehaviour
{
    //rename to turret save data or something?
    public string[] storedTurretDescription = new string[3];
    public TurretRarity storedTurretSelectedRarity;
    public int storeTurretPrice;
    public int storeTurretRerollPrice;
    public float storeTurretRotation;


    public bool CheckTurretDescription()
    {
        // "" means its empty.
        if (storedTurretDescription[0] == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public TurretRarity GetCurrentRarity()
    {
        return this.storedTurretSelectedRarity;
    }
}
