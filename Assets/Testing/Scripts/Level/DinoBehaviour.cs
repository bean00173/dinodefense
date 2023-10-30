using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dinosaur
{
    public float health;
}

public class DinoBehaviour : MonoBehaviour
{
    public Dinosaur dinoStats;
    public float currentHealth { get; private set; }
    private GameObject healthText;
    public GameObject ps;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = dinoStats.health;
        healthText = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(ps, this.transform.position, Quaternion.identity);
            Destroy(healthText.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0f/* || collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0f*/)
            {
                TakeDamage(collision.gameObject.GetComponent<BuildingBehaviour>().fallDamage);
            }
        }
    }
}
