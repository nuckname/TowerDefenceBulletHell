using UnityEngine;

public class GoldMinerHealth : MonoBehaviour
{
    [SerializeField] private int _goldMinerHealth = 10000; // Starting health
    private Renderer _renderer; // For modifying material color
    private float _baseDamageInterval = 10f / 100; // Base interval: 10 minutes (600 seconds) divided into 100 health points
    private float _timer = 0f; // Timer to track intervals
    private int _goldMinerCounter = 1; // Number of miners; starts at 1 by default
    private float _damageInterval; // Actual interval accounting for the multiplier

    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer to modify material color
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer component is missing!");
        }

        // Initialize the actual damage interval
        UpdateDamageInterval();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime; // Increment the timer

        if (_timer >= _damageInterval) // Check if it's time to apply damage
        {
            TakeDamage(1); // Apply 1 point of damage
            _timer = 0f; // Reset the timer
        }
    }

    private void TakeDamage(int damage)
    {
        _goldMinerHealth -= damage;

        // Clamp health between 0 and 100
        _goldMinerHealth = Mathf.Clamp(_goldMinerHealth, 0, 10000);

        // Update the color based on health
        if (_renderer != null)
        {
            float healthPercentage = _goldMinerHealth / 100f;
            Color newColor = new Color(1f, healthPercentage, 0f, healthPercentage); // Darker yellow and more transparent
            _renderer.material.color = newColor;
        }

        // Destroy the object if health is zero
        if (_goldMinerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateDamageInterval()
    {
        // Update the damage interval based on the number of miners
        _damageInterval = _baseDamageInterval / _goldMinerCounter; // Faster when more miners are present
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Increment the miner counter when a miner enters
        _goldMinerCounter++;
        UpdateDamageInterval(); // Recalculate the interval
    }
}
