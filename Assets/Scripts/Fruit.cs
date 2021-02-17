using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject fruitSlicedPrefab;
    public float startForce = 15f;
    private Rigidbody2D rb;

    public FruitSpawner spawner;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startForce,ForceMode2D.Impulse);
    }

    /// <summary>
    /// Разрезание фруктов
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 direction = (collider.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        if (collider.tag == "Blade")
        {
            GameObject slicedFruit = Instantiate(fruitSlicedPrefab,transform.position,rotation);
            Destroy(slicedFruit,3f);
            spawner.ScoreIncrement();
            Destroy(gameObject);
        }

        if (collider.tag == "Barrier")
        {
            spawner.LoseLife();
        }
        
    }
}
