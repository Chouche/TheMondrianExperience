using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWorldManager : MonoBehaviour
{

    private static CubeWorldManager _instance;

    public static CubeWorldManager Instance { get { return _instance; } }

    public bool activateDifferentSpeed; // activate the function which generate randomly speed for the scale up of the lines

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
