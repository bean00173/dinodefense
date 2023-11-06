using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDisabler : MonoBehaviour
{
    public GameObject levelSelect;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.loadingMenu)
        {
            levelSelect.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
