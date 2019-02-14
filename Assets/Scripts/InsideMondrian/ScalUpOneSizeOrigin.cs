using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the class for the lines which define the limits of the cube world

public class ScalUpOneSizeOrigin : MonoBehaviour
{
    private float maxSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxSize = transform.localScale.x;
     
        if (maxSize < 36.5f)
            transform.localScale += new Vector3(0.1F*2, 0, 0);
    }
}
