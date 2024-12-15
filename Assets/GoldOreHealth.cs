using UnityEngine;
using UnityEngine.Serialization;

public class GoldOreHealth : MonoBehaviour
{
    public int goldMinerHealth = 10000;
    private Renderer _renderer; 
    private float _baseDamageInterval = 10f / 100;
    private float _timer = 0f;
    private int _goldMinerCounter = 1; 
    private float _damageInterval;

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
        if (goldMinerHealth <= 10000)
        {
            _renderer.material.color = Color.yellow;
            if (goldMinerHealth <= 8000)
            {
                _renderer.material.color = new Color(217, 255, 28);

                if (goldMinerHealth <= 6000)
                {
                    _renderer.material.color = new Color(195, 230, 25);

                    if (goldMinerHealth <= 4000)
                    {
                        _renderer.material.color = new Color(170, 201, 20);

                        if (goldMinerHealth <= 2000)
                        {
                            _renderer.material.color = new Color(134, 158, 16);

                            if (goldMinerHealth <= 1000)
                            {
                                _renderer.material.color = new Color(74, 87, 11);


                            }
                        }
                    }
                }
            }
        }
        
        goldMinerHealth -= damage;

        // Clamp health between 0 and 100
        goldMinerHealth = Mathf.Clamp(goldMinerHealth, 0, 10000);

        // Update the color based on health
        if (_renderer != null)
        {
            float healthPercentage = goldMinerHealth / 100f;
            Color newColor = new Color(1f, healthPercentage, 0f, healthPercentage); // Darker yellow and more transparent
            _renderer.material.color = newColor;
        }

        // Destroy the object if health is zero
        if (goldMinerHealth <= 0)
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
