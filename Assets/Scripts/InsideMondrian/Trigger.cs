using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The class who stop every lines which are going outside the limits of the cube world
public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        other.GetComponent<ScaleUpOneSize>().scaleUp = false;
      
    }
}
