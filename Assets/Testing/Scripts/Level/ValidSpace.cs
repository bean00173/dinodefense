using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidSpace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.GetComponent<SpriteRenderer>().bounds.Contains(GameManager.instance.currentObject.GetComponent<Rigidbody2D>().position))
        //{
        //    GameManager.instance.mouseValid = true;
        //}
        //else
        //{

        //}

        if(GameManager.instance.currentObject != null)
        {
            GameManager.instance.mouseValid = this.GetComponent<SpriteRenderer>().bounds.Contains(GameManager.instance.currentObject.GetComponent<Rigidbody2D>().position) ? true : false;
        }
        else
        {
            GameManager.instance.mouseValid = false;
        }

    }

    //private void OnMouseOver()
    //{
    //    GameManager.instance.mouseValid = true;
    //}

    //private void OnMouseExit()
    //{
    //    GameManager.instance.mouseValid = false;
    //}
}
