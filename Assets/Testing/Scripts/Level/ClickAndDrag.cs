using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ClickAndDrag : MonoBehaviour
{
    Vector2 startPos;
    bool holding;
    // Start is called before the first frame update
    void Start()
    {
       startPos = this.transform.position;
       holding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            Debug.Log("Holding");

            GameManager.instance.currentObject = this.gameObject;
            LevelManager.instance.validSpace.ShowSpace();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            mousePosition.z = 0;
            this.transform.position = mousePosition;

            if (Input.GetKeyDown(KeyCode.R))
            {
                Quaternion fromAngle = transform.rotation;
                Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90f));

                this.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, 1f);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (!GameManager.instance.mouseValid)
                {
                    this.GetComponent<Rigidbody2D>().gravityScale = 0;
                    LevelManager.instance.Spend(-this.GetComponent<BuildingBehaviour>().building.cost);
                    StartCoroutine(ReturnToInitialPos());
                    //Destroy(this.gameObject);
                }

                if (holding)
                {
                    holding = false;
                    this.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Default");
                }

                GameManager.instance.currentObject = null;
                LevelManager.instance.validSpace.HideSpace();
            }
        }
    }

    private void OnMouseDrag()
    {
        holding = true;
        //Debug.Log("Holding");

        //GameManager.instance.currentObject = this.gameObject;
        //LevelManager.instance.validSpace.ShowSpace();
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ////mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        //mousePosition.z = 0;
        //this.transform.position = mousePosition;

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Quaternion fromAngle = transform.rotation;
        //    Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90f));

        //    this.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, 1f);
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    this.transform.localScale = -transform.localScale;
        //}
    }


    private void OnMouseUp()
    {
        //if (!GameManager.instance.mouseValid)
        //{
        //    this.GetComponent<Rigidbody2D>().gravityScale = 0;
        //    StartCoroutine(ReturnToInitialPos());
        //    Destroy(this.gameObject);
        //}

        //if (holding)
        //{
        //    holding = false;
        //    this.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Default");
        //}

        //GameManager.instance.currentObject = null;
        //LevelManager.instance.validSpace.HideSpace();
    }

    private IEnumerator ReturnToSafeVerticalPos()
    {
        Vector3 initial = this.transform.position;
        float time = 0;
        float duration = .25f;

        Vector3 returnPos = new Vector3(this.transform.position.x, 0, 0);

        while (time < duration)
        {
            this.transform.position = Vector3.Lerp(initial, returnPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = returnPos;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private IEnumerator ReturnToInitialPos()
    {
        Vector3 initial = this.transform.position;
        float time = 0;
        float duration = .25f;

        while (time < duration)
        {
            this.transform.position = Vector3.Lerp(initial, startPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPos;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
