using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviour : MonoBehaviour
{
    public Building building;
    public float currentHealth { get; private set; }

    public LayerMask postDeathExclude;
    public GameObject ps;
    public GameObject healthText;

    bool clickDisabled;

    public bool beingMoved;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = building.endurance;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.instance.state == levelState.prep)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            healthText.SetActive(false);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            healthText.SetActive(true);
        }
        this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.gray, 1 - (currentHealth / building.endurance));

        if(currentHealth <= 0)
        {
            this.GetComponent<Collider2D>().excludeLayers = postDeathExclude;
            Instantiate(ps, this.transform.position, Quaternion.identity);
            Destroy(healthText);
            Destroy(this.gameObject);
        }

        if (LevelManager.instance.state == levelState.sim && !clickDisabled)
        {
            clickDisabled = true;
            Destroy(this.GetComponent<ClickAndDrag>());
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

}
