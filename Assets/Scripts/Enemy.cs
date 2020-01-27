using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private float projectileSpeed = 20f; 
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 3f; 

    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter(); 
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime; 
        if (shotCounter <= 0)
        {
            Debug.Log("ENEMY FIRE"); 
            Fire(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckHit(other); 
    }

    private void CheckHit(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                Debug.Log("CHECK HIT");
                damageDealer.Hit();
                health -= damageDealer.Damage;
                CheckHealth();
            }
        }
        
    }

    private void CheckHealth()
    {
        if (health < 1)
        {
            Destroy(gameObject); 
        }
    }

    private void Fire()
    {
        ResetShotCounter();
        GameObject projectile = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed); 
    }

}
