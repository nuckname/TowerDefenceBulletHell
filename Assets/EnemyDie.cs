using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public TMP_Text goldText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHasDied()
    {
        //Per layer of defence. Like per bloon popped.
        PlayerGold.CURRENT_PLAYER_GOLD += 10;
        //Update UI.
        goldText.text = PlayerGold.CURRENT_PLAYER_GOLD.ToString();

    }
}
