using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            LevelManager.instance.Spend(-collision.gameObject.GetComponent<BuildingBehaviour>().building.cost);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Dinosaur"))
        {
            collision.gameObject.GetComponent<DinoBehaviour>().Death();
        }
    }
}
