using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    public GameObject Test1;
    public TextMeshProUGUI test;

    [SerializeField] private string tagFilter; 
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(Test1, gameObject.transform.position, quaternion.identity);
        //Instantiate(test, gameObject.transform.position, quaternion.identity);
        //test.text = "gi";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (CompareTag(tagFilter))
        {
            print("UI open");
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (CompareTag(tagFilter))
        {
            print("UI closed");
            
        }
    }
}
