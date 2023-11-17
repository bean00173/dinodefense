using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePeriodIdentity : MonoBehaviour
{
    // this script stores a time period to this object 
    // it is used in instances where a container cycles children based on variables
    // in this case the usage is the market and the lockoff panels

    public timeperiod timePeriod;
    public bool permalocked;
    public bool lockoffPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockoffPanel) // if this script is on a lockoff panel (inspector variable decided) then keep locked off until era is unlocked
        {
            this.gameObject.SetActive(!GameManager.instance.enabledTimePeriods.Contains(this.GetComponent<TimePeriodIdentity>().timePeriod));
        }
    }
}
