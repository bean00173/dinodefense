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
