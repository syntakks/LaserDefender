﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile" ||
            collision.gameObject.tag == "Enemy Projectile")
        {
            Destroy(collision.gameObject); 
        }
    }
}
