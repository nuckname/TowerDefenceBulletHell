using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image _HealthBar1;
    [SerializeField] private Image _HealthBar2;
    [SerializeField] private Image _HealthBar3;
    [SerializeField] private Image _HealthBar4;
    [SerializeField] private Image _HealthBar5;

    [SerializeField] private List<Image> PlayerHpBars = new List<Image>();

    private int _playerHealthIndex = 0;
    void Start()
    {
        PlayerHpBars.Add(_HealthBar1);
        PlayerHpBars.Add(_HealthBar2);
        PlayerHpBars.Add(_HealthBar3);
        PlayerHpBars.Add(_HealthBar4);
        PlayerHpBars.Add(_HealthBar5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMinusHealth()
    {
        if (_playerHealthIndex >= 0 && _playerHealthIndex < PlayerHpBars.Count - 1)
        {
            PlayerHpBars[_playerHealthIndex].gameObject.SetActive(false);
            _playerHealthIndex++;
        }
    }
}
