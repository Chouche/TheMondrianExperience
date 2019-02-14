using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //Simple scale up of a color cube
        if (transform.localScale.x < 1.0f)
            transform.localScale += new Vector3(0.05F, 0.05f, 0.05f);
    }
}
