using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanerRubickCube : MonoBehaviour
{

    [SerializeField] int dimension;
    [SerializeField] GameObject tile;
    [SerializeField] GameObject tileHolder;


    GameObject[,,] matrix3D;
    
    Vector3 gridOrigin;

    Transform transformHolder;







    // Start is called before the first frame update
    void Start()
    {
        gridOrigin.x = transform.position.x;
        gridOrigin.y = transform.position.y;
        gridOrigin.z = transform.position.z;

        print(gridOrigin);

        transformHolder = tileHolder.GetComponent<Transform>();

        StartCoroutine(SpawnCube());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCube()
    {
        matrix3D = new GameObject[dimension, dimension, dimension];
        for(int y = 0; y < dimension; y++)
        {
            for (int z = 0; z < dimension; z++)
            {
                for (int x = 0; x< dimension; x++)
                {
                    if (ShouldSpawn(x,y,z))
                    { 
                       
                            matrix3D[x, y, z] = Instantiate(tileHolder, SpawnPosition(x, y, z), Quaternion.identity, transform);
                        
                    }
                    
                    yield return new WaitForSeconds(0.005f);
                }
            }
        }
    }

    bool ShouldSpawn(int x, int y, int z)
    {
        if ((x == 0 || x == dimension - 1) || (y == 0 || y == dimension - 1) || (z == 0 || z == dimension - 1))
        {
            return true;
        }
        else return false;
    }

    Vector3 SpawnPosition(int x, int y, int z)
    {
        float holderOffset = transformHolder.localScale.x;

        float xOffset = (gridOrigin.x - (dimension - 1) * (holderOffset / 2)) + x ;
        float yOffset = (gridOrigin.y - (dimension - 1) * (holderOffset / 2)) + y ;
        float zOffset = (gridOrigin.z - (dimension - 1) * (holderOffset / 2)) + z ;

        return new Vector3(xOffset, yOffset, zOffset);
    }


}
