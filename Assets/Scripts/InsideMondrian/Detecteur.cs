using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*The detector class will generate an array which work as a map to define if the spaces of one face of the cube world are fulled by a line or not.
 So it will browse the face and check it. If it's empty the detector will fill the empty faces with cubes of the same color with a flood fill algorithm to keep used the same color in group of empty space of the array*/
public class Detecteur : MonoBehaviour
{
    private Vector3 origin;

    public bool collisionX, collisionY;

    Dictionary<Vector3, Vector3> mapVoid = new Dictionary<Vector3, Vector3>();

    private int tempX, tempY;

    private bool begin = false;
    private bool stop = false;

    public GameObject whiteCube, blackCube, redCube, yellowCube, blueCube;
    private GameObject cubePrefab;

    private  bool[,] mapArray; //2D boolean array to map each face of the World Cube

    private Vector3 vecBase;

    // Start is called before the first frame update
    void Start()
    {
        vecBase = transform.position;
        origin = transform.position;
        mapArray = new bool[40, 40];
        transform.position = vecBase;

    }

    // Update is called once per frame
    void Update()
    {
        if (!begin && !stop)
        {
            StartCoroutine(WaitSeconds(7));

        }
        else if (begin && !stop)
        {
            DetectorMoving();
            stop = true;

            for (int i = 0; i < 39; i++)
            {
                
                for (int j = 0; j < 39; j++)
                {
                    cubePrefab = RandomCubePrefab();
                    StartCoroutine(DelayedDFS((i + j) * 0.1f, i, j, mapArray, 40, 40, cubePrefab));
                }
            }
        }
    }

    IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        if (begin == false)
            begin = true;
    }

    /*The detector will browse the face of the cube world and fill 
    the boolean 2D mapArray when the space in the world is free or not.
    */
    private void DetectorMoving()
    {
        for (int i = -19; i < 21; i++)
        {

            for (int j = -19; j < 20; j++)
            {
                if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
                {
                    vecBase.x = j;
                }
                if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
                {
                    vecBase.z = j;
                    if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
                        vecBase.x = -20f;

                }

                if (Physics.CheckSphere(vecBase, 0.3f))
                {
                    mapArray[i + 19, j + 19] = true;

                }

                
                
            }
            if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
            {
                vecBase.y = i;
            }
            if(GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
            {
                vecBase.z = i;
            }

        }
    }



    IEnumerator DelayedDFS(float delay, int x, int y, bool[,] visited, int n, int m, GameObject cubePrefab)
    {
        yield return new WaitForSeconds(delay);
        Dfs(x, y, visited, n, m, cubePrefab);
    }

    // Simple flood fill function to fill the remain empty spaces in the 2D mapArray
    private void Dfs(int x, int y, bool[,] visited, int n, int m, GameObject cubePrefab)
    {
        if (x >= n || y >= m)
        return;
        if (x < 0 || y < 0)
            return;
        if (visited[x,y] == true)
            return;
        visited[x, y] = true;

        if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Front || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Back)
        {
            vecBase.x = y - 19;
            vecBase.y = x - 20;
        }
        if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Right || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Left)
        {
            vecBase.z = y - 19;
            vecBase.y = x - 20;
        }
        if (GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Up || GetComponentInParent<InstanciateObject>().PositionFace == InstanciateObject.CubeFace.Down)
        {
            vecBase.x = y - 19;
            vecBase.z = x - 20;
        }

        Instantiate(cubePrefab, vecBase, Quaternion.identity);

        Dfs(x - 1, y - 1, visited, n, m, cubePrefab);
        Dfs(x - 1, y, visited, n, m, cubePrefab);
        Dfs(x - 1, y + 1, visited, n, m, cubePrefab);
        Dfs(x , y - 1, visited, n, m, cubePrefab);
        Dfs(x, y + 1, visited, n, m, cubePrefab);
        Dfs(x + 1, y - 1, visited, n, m, cubePrefab);
        Dfs(x + 1, y, visited, n, m, cubePrefab);
        Dfs(x + 1, y + 1, visited, n, m, cubePrefab);
       

    }


    
    private GameObject RandomCubePrefab()
    {
        int rand = Random.Range(1, 16);
        switch(rand)
        {
            case 1:
            case 2:
            case 3:
            case 9:
            case 8:
            case 14:
            case 15:
            case 13:
                return whiteCube;
            case 4:
                return blackCube;
            case 5:
            case 11:
                return yellowCube;
            case 6:
            case 10:
                return blueCube;
            case 7:
            case 12:
                return redCube;

        }
        return null;
    }


    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(vecBase, 0.3f);
    }
}
