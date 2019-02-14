using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class of the cubes/lines which turn in the good direction the cube and scale up this one to become a line and 
public class ScaleUpOneSize : MonoBehaviour
{

    public bool scaleUp = true;
    public float scaleX;

    private bool activateDifferentSpeed;

    Vector3 dir;

    private Vector3 origin;

    private float maxSize;

    float speed, offsetX, offsetY, offsetZ;

    public int id;

    public enum OriginPos { Left, Right, Up, Down };

    public OriginPos posOrigin;

    // Start is called before the first frame update
    void Start()
    {
        dir = Vector3.right;
        speed = Random.Range(1.0f, 2.0f);
        
    }

    private void Awake()
    {
        originPosition();
    }


    // Update is called once per frame
    void Update()
    {
        maxSize = transform.localScale.x;
        activateDifferentSpeed = CubeWorldManager.Instance.activateDifferentSpeed;


        dirOffset();
        origin = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);
        scaleX = transform.localScale.x;

        if (scaleUp == true && !activateDifferentSpeed && maxSize < 40)
            transform.localScale += new Vector3(0.1F, 0, 0);
        if (scaleUp == true && activateDifferentSpeed && maxSize < 40)
            transform.localScale += new Vector3(0.1F*speed, 0, 0);

       
    }

    //check with line collided with the other one to stop only the one who collided by the front.
    private void OnTriggerEnter(Collider other)
    {
        
        RaycastHit hit;
        if (Physics.Raycast(origin, transform.TransformDirection(dir), out hit, Mathf.Infinity) && hit.transform.tag == "Line" && hit.distance < 0.5)
        {
            Debug.DrawRay(origin, transform.TransformDirection(dir) * Mathf.Infinity, Color.red);
            scaleUp = false;
        }


    }

    //check the position of each cube/lines at the begining to turn on itself with an other function dirOffset()
    private void originPosition()
    {
        if (transform.position.x == 20)
            posOrigin = OriginPos.Right;
        else if (transform.position.x == -20)
            posOrigin = OriginPos.Left;
        else if (transform.position.y == 20)
            posOrigin = OriginPos.Up;
        else if (transform.position.y == -20)
            posOrigin = OriginPos.Down;
    }


    /* because the line prefab only scale up on one side, I need to turn every cube on itself to scale up inthe right direction*/
    private void dirOffset()
    {

        switch (posOrigin)
        {
            case OriginPos.Left:
                if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
                {
                    offsetX = transform.localScale.x;
                }
                else if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
                {
                    offsetZ = -transform.localScale.x;
                }
                break;

            case OriginPos.Right:
                if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
                {
                    offsetX = -transform.localScale.x;
                }
                else if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
                {
                    offsetZ = transform.localScale.x;
                }
                break;

            case OriginPos.Down:
                if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
                {
                    offsetY = transform.localScale.x;
                }
                else if(GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
                {
                    offsetZ = transform.localScale.x;
                }
                break;

            case OriginPos.Up:
                if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
                {
                    offsetY = -transform.localScale.x;
                }
                else if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
                {
                    offsetZ = -transform.localScale.x;
                }
                break;

        }

    }
}
