using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleChildren : MonoBehaviour
{
    int index = 0;
    bool allowed;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }

        transform.GetChild(index).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CycleToNext()
    {
        while (!allowed)
        {
            if (index == transform.childCount - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            allowed = transform.GetChild(index).GetComponent<TimePeriodIdentity>().permalocked ? false : GameManager.instance.enabledTimePeriods.Contains(transform.GetChild(index).GetComponent<TimePeriodIdentity>().timePeriod) ? true : false;
        }
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(index).gameObject.SetActive(true);
        allowed = false;
    }

    public void CycleToPrevious()
    {
        while (!allowed)
        {
            if (index == 0)
            {
                index = transform.childCount - 1;
            }
            else
            {
                index--;
            }

            allowed = transform.GetChild(index).GetComponent<TimePeriodIdentity>().permalocked ? false : GameManager.instance.enabledTimePeriods.Contains(transform.GetChild(index).GetComponent<TimePeriodIdentity>().timePeriod) ? true : false;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(index).gameObject.SetActive(true);
        allowed = false;
    }
}
