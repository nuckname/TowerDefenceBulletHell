using UnityEngine;

public class StoreTurretDescription : MonoBehaviour
{
    public string[] storedTurretDescription = new string[3]; 

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
