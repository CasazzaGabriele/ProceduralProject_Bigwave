using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearpingFead : MonoBehaviour
{
    public Color endColor;
    private Color startColor;

    private MeshRenderer renderer;
    private Material startMaterial;

    private float duration = 4;

    private float t = 0;

    bool startToEnd = true;

    //-------------------------------------

    Transform transfrom;

    Quaternion startRotation;
    public Quaternion endRotation;

    Quaternion startQuaternion;

    Coroutine coroutine;





    // Start is called before the first frame update
    void Start()
    {
        transfrom = GetComponent<Transform>();
        startRotation = GetComponent<Transform>().rotation;

        //------------------------------------------

        renderer = GetComponent<MeshRenderer>();
        startMaterial = GetComponent<MeshRenderer>().material;

        startColor = renderer.material.color;

        coroutine = StartCoroutine("SartToEnd");
    }

    // Update is called once per frame
    void Update()
    {
        //t = startToEnd ? t += Time.deltaTime / duration : t -= Time.deltaTime / duration;
        //startMaterial.color = Color.Lerp(startColor, endColor, t);
        //print(t);

        //transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

        if(Input.GetKey("space"))
        {
            StopCoroutine(coroutine);
        }

    }

    //---------------------------------------------------------------------------------------------

    IEnumerator SartToEnd()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            startMaterial.color = Color.Lerp(startColor, endColor, t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        coroutine = StartCoroutine("EndToStart");
    }
     
    IEnumerator EndToStart()
    {
        for (float t = 1; t >= 0; t -= Time.deltaTime / duration)
        {
            startMaterial.color = Color.Lerp(startColor, endColor, t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        coroutine = StartCoroutine("SartToEnd");
    }


    //---------------------------------------------------------------------------------------------------

    // lerp funtion 
    void FromStartToEnd()
    {
        for (float t = 0; t <= 1 ; t+= Time.deltaTime / duration)
        {
            startMaterial.color = Color.Lerp(startColor, endColor, t);
        }
    }

    void FromEndToStart()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            startMaterial.color = Color.Lerp(endColor, startColor, t);
        }
    }
}
