using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootCooldown; // Cooldown in seconds
    [SerializeField] Slider cooldownSlider;
    [SerializeField] float projectileSpeed;

    private float lastShootTime;

    void Start()
    {
        if (cooldownSlider != null)
        {
            lastShootTime = shootCooldown * -1; //lastShootTime be negative so the game doesnt think you shoot once at the start (which Time.time starts at 0)
            cooldownSlider.maxValue = shootCooldown;
            cooldownSlider.gameObject.SetActive(false); // Hide the slider initially
        }
    }

    void Update()
    {
        // Check for right mouse click and if the cooldown has expired
        if (Input.GetMouseButtonDown(1) && Time.time >= lastShootTime + shootCooldown)
        {
            ShootProjectile();
            lastShootTime = Time.time;
        }

        // Update the cooldown slider
        if (cooldownSlider != null)
        {
            float cooldownRemaining = Mathf.Clamp(shootCooldown - (Time.time - lastShootTime), 0, shootCooldown);
            cooldownSlider.value = cooldownRemaining;
            cooldownSlider.gameObject.SetActive(cooldownRemaining > 0);
        }
    }

    void ShootProjectile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position);
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.transform.up = direction;
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
    }
}
