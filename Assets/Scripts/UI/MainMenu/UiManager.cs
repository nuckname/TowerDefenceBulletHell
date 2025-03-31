using System;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menus;

    private void Start()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].SetActive(false);
        }
        
        menus[0].SetActive(true);
    }
}
