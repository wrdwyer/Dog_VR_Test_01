using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    public GameObject []peopleList;
    int index;
    bool panicing;
	// Use this for initialization
	void Start () {
        index = 0;
        StartCoroutine("spawn");
	}
    private void OnEnable()
    {

        PannicTrigger.triggerPanicAndRun += Panic;
    }

    private void OnDisable()
    {
        PannicTrigger.triggerPanicAndRun -= Panic;
    }
    void Panic()
    {
        panicing = true;
    }

    IEnumerator spawn()
    {
        //Debug.Log("Spawning "+index);
        peopleList[index++].SetActive(true);
        if (index < peopleList.Length && !panicing)
        {
            //Debug.Log("In if");
            yield return new WaitForSeconds(8);
            StartCoroutine("spawn");
        }
    }
}
