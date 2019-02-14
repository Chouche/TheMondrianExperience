using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportInThePainture : MonoBehaviour
{
    float m_FieldOfView, speed;
    bool startTeleport;

    // Start is called before the first frame update
    void Start()
    {
        m_FieldOfView = 60.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInChildren<GvrReticlePointer>().ReticleOuterDiameter > 0.09 || startTeleport)
        {
            startTeleport = true;
            StartCoroutine(TeleportIn());
        }
        
    }

    IEnumerator TeleportIn()
    {
        speed += 0.1f;
        m_FieldOfView -= speed;
        Camera.main.fieldOfView = m_FieldOfView;
        transform.parent.position += new Vector3(0, 0, speed);
        if (m_FieldOfView < 0.03)
            StartCoroutine(LoadInsideThePainture());
            
        yield return null;
    }

    IEnumerator LoadInsideThePainture()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("InsideThePainture", LoadSceneMode.Single);
    }
}
