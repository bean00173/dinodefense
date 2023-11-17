using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateIndicator : MonoBehaviour
{
    public GameObject associatedContents;
    public Color activeColor;
    public Color inactiveColor;
    // Start is called before the first frame update
    void Start()
    {
        bool active = this.GetComponent<TimePeriodIdentity>().permalocked ? true : GameManager.instance.enabledTimePeriods.Contains(this.GetComponent<TimePeriodIdentity>().timePeriod) ? false : true;
        this.transform.GetChild(0).gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        if (associatedContents.activeSelf)
        {
            this.GetComponent<Image>().color = activeColor;
        }
        else
        {
            this.GetComponent<Image>().color = inactiveColor;
        }
    }
}
