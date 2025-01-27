using System;
using UnityEngine;

public class StoreTurretDescription : MonoBehaviour
{
    public string[] storedTurretDescription = new string[3];

    private void Awake()
    {
        /*
        Debug.LogWarning("storedTurretDescription is empty.");
        storedTurretDescription[0] = "";
        storedTurretDescription[1] = "";
        storedTurretDescription[2] = "";
        */
    }

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
}
