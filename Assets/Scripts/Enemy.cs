using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] float health = 100;
    [Header("Projectile")]
    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private float projectileSpeed = 20f; 
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 3f; 
    [Header("VFX")]
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float durationOfExplosion = 1f;
    [Header("SFX")]
    [SerializeField] private AudioClip deathSFX;
    [Range(0, 1)] [SerializeField] float deathVolume = 0.75f;
    [SerializeField] private AudioClip shootSound;
    [Range(0, 1)] [SerializeField] float shootVolume = 0.25f;

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
                damageDealer.Hit();
                health -= damageDealer.Damage;
                CheckDeath();
            }
        }
        
    }

    private void CheckDeath()
    {
        if (health < 1)
        {
            GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(explosion, durationOfExplosion); 
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, 0.5f); 
        }
    }

    private void Fire()
    {
        ResetShotCounter();
        GameObject projectile = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
    }

}
