using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValidSpace : MonoBehaviour
{
    SpriteRenderer sr;

    bool turningOn;
    bool turningOff;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

        Vector3 min = this.sr.bounds.min;
        Vector3 max = this.sr.bounds.max;

        GameManager.instance.SetDinoBounds(min.x, max.x);
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
            GameManager.instance.mouseValid = sr.bounds.Contains(GameManager.instance.currentObject.GetComponent<Rigidbody2D>().position) ? true : false;
        }
        else
        {
            GameManager.instance.mouseValid = false;
        }
    }

    public void ShowSpace()
    {
        StopAllCoroutines();
        StartCoroutine(LerpColor(0.15f));
    }

    public void HideSpace()
    {
        StopAllCoroutines();
        StartCoroutine(LerpColor(0f));
    }

    private IEnumerator LerpColor(float alpha)
    {
        if(alpha == 0)
        {
            while (sr.color.a > alpha)
            {
                if (sr.color.a > alpha + .01f) break;
                sr.color = new Color(1, 0, 0, Mathf.Lerp(sr.color.a, alpha, Time.deltaTime * 1f));
                yield return null;
            }
        }
        else
        {
            while (sr.color.a < alpha)
            {
                if (sr.color.a < alpha - .01f) break;
                sr.color = new Color(1, 0, 0, Mathf.Lerp(sr.color.a, alpha, Time.deltaTime * 1f));
                yield return null;
            }
        }
        

        sr.color = new Color(0, 1, 0, alpha);
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
