using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    public float speed = 5f;       // Forward movement speed
    public float spiralSpeed = 5f; // Speed of the spiral motion
    public float spiralRadius = 1f; // Radius of the spiral movement

    private float time; 

    private void Update()
    {
        time += Time.deltaTime;

        // Forward movement
        Vector3 forwardMove = transform.right * (speed * Time.deltaTime);

        // Spiral motion using sine and cosine
        float xOffset = Mathf.Cos(time * spiralSpeed) * spiralRadius;
        float yOffset = Mathf.Sin(time * spiralSpeed) * spiralRadius;
        Vector3 spiralMove = new Vector3(xOffset, yOffset, 0);

        // Apply movement
        transform.position += forwardMove + spiralMove;
    }
}