using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    public float lifetime = 5f; 

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
