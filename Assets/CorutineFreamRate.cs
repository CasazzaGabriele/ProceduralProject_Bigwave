using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutineFreamRate : MonoBehaviour
{
    private Transform transform;
    private Vector3 startScale;
    private Vector3 endScale;


    public float moltiplicator;

    private float duration = 4;
    bool startToEnd = true;

    Coroutine coroutine;


    // Start is called before the first frame update
    void Start()
    {
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
    }


    IEnumerator StartToEnd_Coroutine()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        StartCoroutine("EndToStart_Coroutine");
    }

    IEnumerator EndToStart_Coroutine()
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            transform.localScale = Vector3.Lerp(endScale, startScale, t);
            yield return null;
        }
        StartCoroutine("StartToEnd_Coroutine");
    }
}
