using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    public float lifetime = 15f; 

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
