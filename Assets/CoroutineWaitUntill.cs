using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineWaitUntill : MonoBehaviour
{

    private Transform transform;
    private Vector3 startScale;
    private Vector3 endScale;


    public float moltiplicator;

    private float duration = 4;

    bool playstop = true;

    Coroutine coroutine;

    //-------------------------------------------------

    MeshRenderer renderer;
    Color color;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        color = renderer.material.color;

        transform = GetComponent<Transform>();
        startScale = transform.localScale;

        endScale = startScale * moltiplicator;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            StartCoroutine("StartToEnd_Coroutine");
        }

        if (Input.GetKeyDown("k"))
        {
            playstop = !playstop;
        }
    }

    IEnumerator StartToEnd_Coroutine()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            color.a = Mathf.Lerp(1f, 0.1f, t);
            renderer.material.color = color;
            yield return new WaitUntil(() => playstop);
        }
        StartCoroutine("EndToStart_Coroutine");
    }

    IEnumerator EndToStart_Coroutine()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            transform.localScale = Vector3.Lerp(endScale, startScale, t);
            color.a = Mathf.Lerp(0.1f, 1f, t);
            renderer.material.color = color;
            yield return new WaitUntil(() => playstop);
        }
        StartCoroutine("StartToEnd_Coroutine");
    }


}

