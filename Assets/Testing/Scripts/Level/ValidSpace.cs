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

        if(GameManager.instance.currentObject != null) // if there is an object being held 
        {
            GameManager.instance.mouseValid = sr.bounds.Contains(GameManager.instance.currentObject.GetComponent<Rigidbody2D>().position) ? true : false; // mouse is valid if the currentobject is within this objects sprite bounds
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

    private IEnumerator LerpColor(float alpha) // lerp to activate / deactivate the color by lowering / raising the alpha value
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

    public bool isValid(GameObject go)
    {
        return sr.bounds.Contains(go.transform.position) ? true : false;
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
