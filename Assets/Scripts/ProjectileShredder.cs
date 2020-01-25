using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered!"); 
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject); 
        }
    }
}
