using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public int health = 3;
    private SnakeBoss snakeBoss;

    void Start()
    {
        snakeBoss = FindObjectOfType<SnakeBoss>(); 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            snakeBoss.DamageSegment(gameObject);
        }
    }

}
