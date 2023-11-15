using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHintPanel : MonoBehaviour
{
    public static ButtonHintPanel instance;
    bool active;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(active);
        }

        if(LevelManager.instance.state == levelState.sim)
        {
            active = false;
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
