using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

[System.Serializable]
public class Dinosaur // class to store dinosaur health, probably didnt need a separate class 
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
        currentHealth = dinoStats.health; // sets current health
        if(Random.value > .5) Flip(); // randomly flips the dinosaur on start for a little bit of variation in gameplay
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) // if dinosaur has no health left
        {
            Instantiate(ps, this.transform.position, Quaternion.identity); // instantiate death ps
            Destroy(healthText); // destroy relevant gameobjects
            Destroy(this.gameObject);
        }

        if(LevelManager.instance.state == levelState.sim && !clickDisabled) // if simulation phase has been entered and bool not true (so that this only runs once in sim phase)
        {
            clickDisabled = true;
            simulating = true;
            Destroy(this.GetComponent<DinoControl>());
        }

        if (!simulating && !this.GetComponent<DinoControl>().holding) // if not being held and not simulating
        {
            if(this.transform.localScale.x > 0) // if facing right
            {
                if(this.transform.position.x >= GameManager.instance.rightBound - this.GetComponent<SpriteRenderer>().bounds.extents.x - 0.1f && !flipped) // if dinosaur has reached right limit of movement area
                {
                    flipped = true;
                    Invoke(nameof(Flip), 1.5f); // flip dinosaur around 
                }
                else if (!flipped) // else if within area and not flipped
                {
                    this.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0); // move dinosaur towards right limit
                }
            }
            else // if facing left
            {
                if (this.transform.position.x <= GameManager.instance.leftBound + this.GetComponent<SpriteRenderer>().bounds.extents.x + 0.1f && !flipped) // if dinosaur has reached left limit of movement area
                {
                    flipped = true;
                    Invoke(nameof(Flip), 1.5f); // flip dinosaur around 
                }
                else if (!flipped) // else if within area and not flipped
                {
                    this.transform.position += new Vector3(-Time.deltaTime * moveSpeed, 0, 0); // move dinosaur towards left limit
                }
            }
        }

        DoProximityCheck(); // check if nearby buildings
    }

    public void TakeDamage(float damage) 
    {
        if(LevelManager.instance.state == levelState.sim)
        {
            currentHealth -= damage; // deduct health
        }  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building")) // if building collides with dinosaur
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x >= .1f || collision.gameObject.GetComponent<Rigidbody2D>().velocity.x <= -.1f) // this needs to be modified to ensure that dinosaurs cannot die to minor movements
            {
                TakeDamage(collision.gameObject.GetComponent<BuildingBehaviour>().building.fallDamage); // take damage based on how much damage the building deals on falls
            }
        }
    }

    private void Flip() // inverts the current localScale.x
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
        flipped = false;
    }

    private void DoProximityCheck() // circlecasts in an area to check if there is a building nearby
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, this.GetComponent<SpriteRenderer>().bounds.extents.x, Vector2.up);
        foreach (RaycastHit2D rayHit in hit)
        {
            GameObject go = rayHit.collider.gameObject;
            if (go.CompareTag("Building") && !flipped)
            {
                flipped = true;
                Invoke(nameof(Flip), 1.5f);
            }

        }
    }
}
