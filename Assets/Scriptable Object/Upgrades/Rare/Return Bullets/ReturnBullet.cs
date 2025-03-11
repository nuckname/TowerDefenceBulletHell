using UnityEngine;

public class ReverseBullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float returnDelay = 1f; // Time delay before the bullet returns
    public float returnDuration = 2f; // Duration for which the bullet returns

    private Vector2 initialDirection;
    private bool isReturning = false;
    private float returnTimer = 0f;

    void Start()
    {
        // Store the initial direction of the bullet
        initialDirection = transform.right;
    }

    void Update()
    {
        if (!isReturning)
        {
            // Move the bullet forward
            transform.Translate(initialDirection * speed * Time.deltaTime);

            // Check if it's time to return
            returnTimer += Time.deltaTime;
            if (returnTimer >= returnDelay)
            {
                isReturning = true;
                returnTimer = 0f;
            }
        }
        else
        {
            // Move the bullet back in the opposite direction
            transform.Translate(-initialDirection * speed * Time.deltaTime);

            // Check if the return duration is over
            returnTimer += Time.deltaTime;
            if (returnTimer >= returnDuration)
            {
                // Destroy the bullet after returning
                Destroy(gameObject);
            }
        }
    }

}
