using System.Collections;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    public int maxhealth;
    public int health;
    public int damage;
    public float speed;
    public bool ground;
    public GameManager gameManager;
    public int price;
    public int reward;
    public int capacity;
    public GameObject explosion;

    // Reference to the enemy's renderer
    private Renderer enemyRenderer;

    // Flash duration in seconds
    public float flashDuration = 0.1f;
    public bool blueTeam;
    // Struct to store enemy stats for level ups
    public struct EnemyStats
    {
        public int maxHealth;
        public int damage;
        public float speed;
        public bool ground;
        public int price;
        public float reward;
        public int capacity;
    }
    public EnemyStats level1 = new EnemyStats
    {
        maxHealth = 10,
        damage = 5,
        speed = 1.5f,
        ground = true,
        price = 2,
        reward = 1,
        capacity = 1
    };
    public EnemyStats level2 = new EnemyStats
    {
        maxHealth = 10,
        damage = 20,
        speed = 1.5f,
        ground = true,
        price = 2,
        reward = 1,
        capacity = 1
    };
    public EnemyStats level3 = new EnemyStats
    {
        maxHealth = 300,
        damage = 30,
        speed = 1.5f,
        ground = true,
        price = 2,
        reward = 1,
        capacity = 1
    };

    void Start()
    {
        health = maxhealth;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();

        // Get the renderer of the enemy itself (not the white child object)
        enemyRenderer = GetComponent<Renderer>();
    }


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        // Flash white when taking damage (but not dying)
        if (health > 0)
        {
            StartCoroutine(FlashWhite());
            FindObjectOfType<AudioManager>().Play("Enemy_hit");
        }
        else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            gameManager.AddCoins(reward, !blueTeam);

            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Enemy_death");
        }
    }

    // Coroutine to flash white
    private IEnumerator FlashWhite()
    {
        // Deactivate the renderer to show the white child object
        if (enemyRenderer != null)
        {
            enemyRenderer.enabled = false;
        }

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Reactivate the renderer to go back to the normal appearance
        if (enemyRenderer != null)
        {
            enemyRenderer.enabled = true;
        }
    }
}
