using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBoxBehaviour : MonoBehaviour
{
    public float _maxHealth;
    public float currentHealth { get; private set; }

    public LayerMask postDeathExclude;
    public GameObject ps;
    public GameObject healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.instance.state == levelState.prep)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            healthText.SetActive(false);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            healthText.SetActive(true);
        }
        this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.red, 1 - (currentHealth / _maxHealth));

        if(currentHealth <= 0)
        {
            this.GetComponent<Collider2D>().excludeLayers = postDeathExclude;
            Instantiate(ps, this.transform.position, Quaternion.identity);
            Destroy(healthText);
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

}
