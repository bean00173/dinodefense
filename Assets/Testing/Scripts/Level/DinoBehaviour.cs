using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

[System.Serializable]
public class Dinosaur
{
    public float health;
}

public class DinoBehaviour : MonoBehaviour
{
    public Dinosaur dinoStats;
    public float currentHealth { get; private set; }
    [SerializeField] private float moveSpeed;
    public GameObject healthText;
    public GameObject ps;

    bool clickDisabled;

    bool flipped;
    bool simulating;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = dinoStats.health;
        if(Random.value > .5) Flip();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(ps, this.transform.position, Quaternion.identity);
            Destroy(healthText);
            Destroy(this.gameObject);
        }

        if(LevelManager.instance.state == levelState.sim && !clickDisabled)
        {
            clickDisabled = true;
            simulating = true;
            Destroy(this.GetComponent<ClickAndDrag>());
        }

        if (!this.GetComponent<DinoControl>().holding && !simulating) // if not being held
        {
            if(this.transform.localScale.x > 0) // if facing right
            {
                Debug.Log($"Moving to {GameManager.instance.rightBound}");

                if(this.transform.position.x >= GameManager.instance.rightBound - this.GetComponent<SpriteRenderer>().bounds.extents.x - 0.1f && !flipped)
                {
                    flipped = true;
                    Invoke(nameof(Flip), 1.5f);
                }
                else if (!flipped)
                {
                    this.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                }
            }
            else // if facing left
            {
                Debug.Log($"Moving to {GameManager.instance.leftBound + this.GetComponent<SpriteRenderer>().bounds.extents.x + 0.1f}");
                if (this.transform.position.x <= GameManager.instance.leftBound + this.GetComponent<SpriteRenderer>().bounds.extents.x + 0.1f && !flipped)
                {
                    flipped = true;
                    Invoke(nameof(Flip), 1.5f);
                }
                else if (!flipped)
                {
                    this.transform.position += new Vector3(-Time.deltaTime * moveSpeed, 0, 0);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(LevelManager.instance.state == levelState.sim)
        {
            currentHealth -= damage;
        }  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            if(LevelManager.instance.state == levelState.prep)
            {
                flipped = true;
                Invoke(nameof(Flip), 1.5f);
            }
            else
            {

                if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0f/* || collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0f*/)
                {
                    TakeDamage(collision.gameObject.GetComponent<BuildingBehaviour>().building.fallDamage);
                }
            }
        }
    }

    private void Flip()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
        flipped = false;
    }
}
