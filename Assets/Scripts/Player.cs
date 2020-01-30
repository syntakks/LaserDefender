using System.Collections; 
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int health = 200;
    [SerializeField] private float playerSpeed = 20f;
    [SerializeField] private float boundaryOffset = 0.5f;
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private float fireCooldown = 0.1f;
    [Header("SFX")]
    [SerializeField] private AudioClip deathSFX;
    [Range(0, 1)] [SerializeField] float deathVolume = 0.75f;
    [SerializeField] private AudioClip shootSound;
    [Range(0, 1)] [SerializeField] float shootVolume = 0.25f;
    Coroutine firingCoroutine; 
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax; 

    // Start is called before the first frame update
    void Start()
    {
        SetupBoundaries(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckFire(); 
    }

    private void SetupBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + boundaryOffset; 
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - boundaryOffset;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + boundaryOffset;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - boundaryOffset;
    }

    private void CheckMove()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPosition, newYPosition); 
    }

    private void CheckFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(ShootCo()); 
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine); 
        }
    }

    private IEnumerator ShootCo()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed;
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
            Destroy(projectile, 2f);
            yield return new WaitForSeconds(fireCooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckHit(other); 
    }


    // Projectiles trigger this. 
    private void CheckHit(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" ||
            other.gameObject.tag == "Enemy Projectile")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                damageDealer.Hit();
            }
            health -= 100;
            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (health < 1)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, 0.5f);
        }
    }
}
