using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingBehaviour : MonoBehaviour
{
    public Building building; // variable using Building class to store current building statistics
    public float currentHealth { get; private set; }

    public LayerMask postDeathExclude;
    public GameObject destroyPs, placePs, damagedPs;
    public GameObject healthText; // store healthbar so it can be referenced when it moves to world space canvas

    bool clickDisabled;

    public bool beingMoved;

    [Header("Audio Playback Event Triggers (Sound Handler)")]
    public UnityEvent onPlace = new UnityEvent();
    public UnityEvent onPickUp = new UnityEvent();
    public UnityEvent onDrop = new UnityEvent();
    public UnityEvent onDestroy = new UnityEvent();

    bool damaged;

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
            //healthText.SetActive(false); // disable health text
        }
        else 
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            //healthText.SetActive(true);
        }
        this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.gray, 1 - (currentHealth / building.endurance)); // updates the sprite color based on the percentage of current health

        
        if (currentHealth <= 0) // if no more health
        {
            this.GetComponent<Collider2D>().excludeLayers = postDeathExclude;
            Instantiate(destroyPs, this.transform.position, Quaternion.identity); // instantiate death particle system
            onDestroy?.Invoke();
            //Destroy(healthText); // destroy associated healthbar on healthbar canvas
            Destroy(this.gameObject); // destroy this building
        }
        else if ((currentHealth / building.endurance) <= .5f)// if building health is at or below half and has not already changed to damaged state, switch to damaged sprite.
        {
            ImageStateChange();
        }

        if (LevelManager.instance.state == levelState.sim && !clickDisabled) // if current state is simulation and bool not true (so that this only runs once per sim phase)
        {
            clickDisabled = true;
            Destroy(this.GetComponent<ClickAndDrag>()); // disable building movement
        }

    }

    public void TakeDamage(float damage)
    {
        Instantiate(damagedPs, this.transform.position, Quaternion.identity);
        damaged = true;
        currentHealth -= damage; // deduct health
    }

    public void ImageStateChange()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.building.damagedState; // change sprite to damaged sprite
    }

    public void ObjectPlaced()
    {
        Instantiate(placePs, this.transform.position, Quaternion.identity); // spawn placement destroyPs;
        onPlace?.Invoke();
    }

}
