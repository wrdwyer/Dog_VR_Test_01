using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionScript : MonoBehaviour
{
    public List<GameObject> stops = new List<GameObject>();

    [Tooltip("The time a stop is green in seconds.")]
    public float wait = 5f;

    private bool StartCycle = true;

    private int index = 0;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < stops.Count; i++)
        {
            stops[index].GetComponent<StopScript>().stop = true;
            stops[index].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(StartCycle)
        {
            StartCoroutine(cycle(index));

            index++;

            if (index >= stops.Count)
                index = 0;
        }
    }

    IEnumerator cycle(int index)
    {
        StartCycle = false;

        StopScript stopScript = stops[index].GetComponent<StopScript>();

        stopScript.stop = false;
        stops[index].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0, 0.5f);

        yield return new WaitForSeconds(wait);

        stopScript.stop = true;
        stops[index].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 0.5f);

        StartCycle = true;

    }
}
