using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static int CURRENT_ROUND;

    [SerializeField] private Transform SpawnPoint;
    int RedEnemiesSpawnAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        //debug 
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateRound();
        }
    }

    //Called using state machien
    public void CreateRound()
    {
        //+2
        //+1
        //pattern continues.
        //random cals, they dont actually mean anything. 
        float enemySpawnRateMutipler = 1.5f;
        float multipler = enemySpawnRateMutipler * CURRENT_ROUND;
        
        //debug
        for (int i = 0; i < multipler + 4; i++)
        {
            //Spawn Red enemies
            RedEnemiesSpawnAmount++;
        }
        //Coroutine so they dont stack on top of each other. 
        
        print("Red Enemies: " + RedEnemiesSpawnAmount);
        CURRENT_ROUND++;
        //How do i make this scale? 
    }
}
