using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {

    public int health = 1;

    void OnTriggerEnter2D()
    {
        Debug.Log("Trigger");

        health--;
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
