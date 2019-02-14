using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateObject : MonoBehaviour
{
    public GameObject linePrefabX, linePrefabY;
    private int posX, posY;
    private Quaternion rotation;

    private GameObject linePrefab;

    public enum CubeFace { Front, Left, Right, Back, Up, Down };
    public CubeFace PositionFace;

    public Vector3 vecDir;

    private int a,b,c,d,min,max;

    private Dictionary<int, int> mapX = new Dictionary<int, int>();
    private Dictionary<int, int> mapY = new Dictionary<int, int>();

    public int test;

    Transform holderPosition;

    void Start()
    {
        min = 1;
        max = 5;
        
        holderPosition = transform;
        for (int y = 0; y < 10; y++)
        {
            
            randomEdge();
            GameObject line = Instantiate(linePrefab, new Vector3(posX, posY, 20), rotation);
            line.transform.SetParent(gameObject.transform);
            line.GetComponent<ScaleUpOneSize>().id = y;

        }
        rotationLineHolder();



    }

    private int randomIntWithDictionary(Dictionary<int,int> map)
    {
        int i = Random.Range(-16, 16);
        if (!map.ContainsKey(i))
        {
            for (int y = -3; y <= 3; y++)
            {
                if (map.ContainsKey(i + y))
                {
                    return randomIntWithDictionary(map);
                }
            }
            map.Add(i, i);
            return i;
        }
        else
        {
            return randomIntWithDictionary(map);
        }

        
    
    }

    private void rotationLineHolder()
    {
        CubeFace posFace = PositionFace;

        switch (posFace)
        {
            case CubeFace.Front:
                break;

            case CubeFace.Right:
                transform.Rotate(Vector3.up * 90);
                break;

            case CubeFace.Back:
                transform.Translate(Vector3.forward * -40);
                break;

            case CubeFace.Left:
                transform.Rotate(Vector3.up * 90);
                transform.Translate(Vector3.forward * -40);
                break;

            case CubeFace.Up:
                transform.Rotate(Vector3.right * 90);
                break;

            case CubeFace.Down:
                transform.Rotate(Vector3.right * 90);
                transform.Translate(Vector3.forward * -40);
                break;

        }
    }

    private void randomEdge()
    {
        int casePos = Random.Range(min, max);
        
        switch (casePos)
        {
            case 1:
                if (a <= 2)
                {
                    posX = -20;
                    posY = randomIntWithDictionary(mapY);
                    rotation = Quaternion.identity;
                    linePrefab = linePrefabX;
                    a++;
                    test++;
                    break;
                }
                else
                {
                    min++;
                    randomEdge();
                    break;
                }
                

            case 2:
                if (b <= 2)
                {
                    posX = 20;
                    posY = randomIntWithDictionary(mapY);
                    rotation = Quaternion.identity * Quaternion.Euler(0, 0, 180);
                    linePrefab = linePrefabX;
                    b++;
                    test++;
                    break;
                }
                else
                {
                    randomEdge();
                    break;
                }
                

            case 3:
                if (c <= 2)
                {
                    posX = randomIntWithDictionary(mapX);
                    posY = 20;
                    rotation = Quaternion.identity * Quaternion.Euler(0, 0, 270);
                    linePrefab = linePrefabY;
                    c++;
                    test++;
                    break;
                }
                else
                {
                    randomEdge();
                    break;
                }
                

            case 4:
                if( d <= 2)
                {
                    posX = randomIntWithDictionary(mapX);
                    posY = -20;
                    rotation = Quaternion.identity * Quaternion.Euler(0, 0, 90);
                    linePrefab = linePrefabY;
                    test++;
                    d++;
                    break;
                }
                else
                {
                    max--;
                    randomEdge();
                    break;
                }


            default:
                posX = 20;
                posY = Random.Range(-17, 18);
                rotation = Quaternion.identity * Quaternion.Euler(0, 0, 90);
                break;
        }

    }

}
