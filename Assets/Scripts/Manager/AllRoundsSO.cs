using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Rounds/RoundData", order = 1)]
public class AllRoundsSO : ScriptableObject
{
    public RoundDataSO[] rounds = new RoundDataSO[20];
}


