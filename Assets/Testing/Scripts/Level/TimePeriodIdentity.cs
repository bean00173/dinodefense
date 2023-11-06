using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePeriodIdentity : MonoBehaviour
{
    public timeperiod timePeriod;

    public bool lockoffPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockoffPanel)
        {
            this.gameObject.SetActive(!GameManager.instance.enabledTimePeriods.Contains(this.GetComponent<TimePeriodIdentity>().timePeriod));
        }
    }
}
