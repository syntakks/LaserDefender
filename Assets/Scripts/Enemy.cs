﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckHit(other); 
    }

    private void CheckHit(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            damageDealer.Hit();
            health -= damageDealer.Damage;
            CheckHealth(); 
        }
    }

    private void CheckHealth()
    {
        if (health < 1)
        {
            Destroy(gameObject); 
        }
    }

}
