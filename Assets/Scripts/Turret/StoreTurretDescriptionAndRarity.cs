using System;
using UnityEngine;

public class StoreTurretDescriptionAndRarity : MonoBehaviour
{
    //rename to turret save data or something?
    public string[] storedTurretDescription = new string[3];
    public string storedTurretSelectedRarity = "";
    public int storeTurretPrice;
    public int storeTurretRerollPrice;


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

    public string GetCurrentRarity()
    {
        return this.storedTurretSelectedRarity;
    }
}
