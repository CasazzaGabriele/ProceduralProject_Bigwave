using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixSpawner : MonoBehaviour
{
    #region "VARIABILI ESPOSTE"

    [SerializeField] int gridx;
    [SerializeField] int gridy;
    [SerializeField] int gridz;
    [SerializeField] GameObject tile;
    [SerializeField] float delay;
    [SerializeField] float tileOffset;

    [Range(0.1f,13f)]
    [SerializeField] float frequency;

    [Range(0.1f, 13f)]
    [SerializeField] float amplitude;

    [SerializeField] Gradient gradient;

    #endregion

    #region "VARIABILI NASCOSTE"
    GameObject[,,] matrix3d;
    Vector3 gridOrigin;

    float noisx;
    float noisz;

    int seedx;
    int seedz;

    float perlinNois;

    int xCounter;
    int yCounter;
    int zCounter;


    #endregion



    private void Awake()
    {
        seedx = Random.Range(-100000, 100000);
        seedz = Random.Range(-100000, 100000);

        gridOrigin.x = transform.localPosition.x - (gridx - 1) * (tileOffset / 2);
        gridOrigin.y = transform.localPosition.y - (gridy - 1) * (tileOffset / 2);
        gridOrigin.z = transform.localPosition.z - (gridz - 1) * (tileOffset / 2);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }



    IEnumerator SpawnGrid()
    {
        xCounter = 0;
        yCounter = 0;
        zCounter = 0;
        matrix3d = new GameObject[gridx, gridy, gridz];
        for (int y=0; y < gridy; y++)
        {
            yCounter++;
            for (int z = 0; z < gridz; z++)
            {
                zCounter++;
                for (int x = 0; x < gridx; x++)
                {
                    xCounter++;
                    matrix3d[x, y, z] = Instantiate(tile, SpawnPosition(x, y, z), Quaternion.identity, transform);
                    ColorTile(matrix3d[x, y, z]);
                    yield return new WaitForSeconds(0.005f);
                }
            }
        }
    }



    Vector3 SpawnPosition(int x, int y,int z)
    {
        
        float spawnX = gridOrigin.x + x * tileOffset;
        float spawnY = gridOrigin.y + y * tileOffset + GeneratorNoisY(x,z) * amplitude;
        float spawnZ = gridOrigin.z + z * tileOffset;

         
        return new Vector3(spawnX, spawnY, spawnZ);
    }

    float GeneratorNoisY(int x, int z)
    {
        noisx = (seedx + x) / frequency;
        noisz = (seedz + z) / frequency;
        perlinNois = Mathf.PerlinNoise(noisx, noisz);
        return perlinNois - 0.5f;
    }

    void ColorTile(GameObject tile)
    {
       // float height = tile.transform.localPosition.y+(amplitude * 0.5f);
        tile.GetComponent<MeshRenderer>().material.color = gradient.Evaluate(perlinNois);
    }



    IEnumerator DestroyGrid()
    {
        print(xCounter);
        print(yCounter);
        print(zCounter);

        // mi posiziono sull ultimo piano e man mano scendo 
        for (int y = yCounter-1; y>=0; y--){ 

            // controllo se l'ultima colonno spawnata è completa 
            if ( zCounter%gridz == 0) {
                // se lo è mi posiziono sempre sull ultima colonna 
                for (int z = gridz - 1; z >= 0; z--){
                    // e faccio lo stesso controllo sulle righe
                    if (xCounter % gridx == 0)
                    {
                        for (int x = gridx - 1; x >= 0; x--)
                        {

                            Destroy(matrix3d[x, y, z]);
                            yield return new WaitForSeconds(0.05f);
                        }
                    }
                    else
                    {
                        // nel caso l'ultima righa non sia completra mi calcolo il resto e la cancello
                        int xIndex = xCounter - (((int)(xCounter / gridx))*gridx);
                        for (int x = xIndex - 1; x >= 0; x--)
                        {

                            Destroy(matrix3d[x, y, z]);
                            yield return new WaitForSeconds(0.05f);
                        }
                        xCounter -= xIndex;
                    }
                }
            }
            else
            {
                int zIndex = zCounter - (((int)(zCounter / gridz)) * gridz);
                print(zIndex);
                for (int z = zIndex - 1; z >= 0; z--)
                {
                    if (xCounter % gridx == 0)
                    {
                        for (int x = gridx - 1; x >= 0; x--)
                        {

                            Destroy(matrix3d[x, y, z]);
                            yield return new WaitForSeconds(0.05f);
                        }
                    }
                    else
                    {
                        int xIndex = xCounter - (((int)(xCounter / gridx)) * gridx);
                        print(xIndex);
                        for (int x = xIndex - 1; x >= 0; x--)
                        {
                         
                            Destroy(matrix3d[x, y, z]);
                            yield return new WaitForSeconds(0.05f);
                        }
                        xCounter -= xIndex;
                    }
                }
                zCounter -= zIndex;

            }
        }
        StartCoroutine(SpawnGrid());
            
    }


    public void onClick()
    {
        if (matrix3d != null)
        {
            StopAllCoroutines();
            StartCoroutine(DestroyGrid());
           

        }
        else
        {
            StartCoroutine(SpawnGrid());
        }
    }
    
}
