using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviour : MonoBehaviour
{
    public Building building; // variable using Building class to store current building statistics
    public float currentHealth { get; private set; }

    public LayerMask postDeathExclude;
    public GameObject ps;
    public GameObject healthText; // store healthbar so it can be referenced when it moves to world space canvas

    bool clickDisabled;

    public bool beingMoved;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = building.endurance; // sets health to stat endurance
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.instance.state == levelState.prep) // if current levelstate is in the prep phase
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // set rigidbody constraints
            this.GetComponent<Rigidbody2D>().gravityScale = 1; // set gravity
            healthText.SetActive(false); // disable health text
        }
        else 
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            healthText.SetActive(true);
        }
        this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.gray, 1 - (currentHealth / building.endurance)); // updates the sprite color based on the percentage of current health

        if(currentHealth <= 0) // if no more health
        {
            this.GetComponent<Collider2D>().excludeLayers = postDeathExclude;
            Instantiate(ps, this.transform.position, Quaternion.identity); // instantiate death particle system
            Destroy(healthText); // destroy associated healthbar on healthbar canvas
            Destroy(this.gameObject); // destroy this building
        }

        if (LevelManager.instance.state == levelState.sim && !clickDisabled) // if current state is simulation and bool not true (so that this only runs once per sim phase)
        {
            clickDisabled = true;
            Destroy(this.GetComponent<ClickAndDrag>()); // disable building movement
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // deduct health
    }

}
