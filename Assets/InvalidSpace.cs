using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvalidSpace : MonoBehaviour
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
        //    GameManager.instance.mouseInvalid = true;
        //}
        //else
        //{

        //}

        if(GameManager.instance.currentObject != null)
        {
            GameManager.instance.mouseInvalid = this.GetComponent<SpriteRenderer>().bounds.Contains(GameManager.instance.currentObject.GetComponent<Rigidbody2D>().position) ? true : false;
            GameManager.instance.intersecting = this.GetComponent<SpriteRenderer>().bounds.Contains(GameManager.instance.currentObject.GetComponent<Rigidbody2D>().position) ? true : false;
        }

    }

    //private void OnMouseOver()
    //{
    //    GameManager.instance.mouseInvalid = true;
    //}

    //private void OnMouseExit()
    //{
    //    GameManager.instance.mouseInvalid = false;
    //}
}
