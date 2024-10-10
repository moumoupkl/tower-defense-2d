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

    // Flash duration in seconds
    public float flashDuration = 0.1f;
    public bool blueTeam;
    public float level;

    // Reference to the TeamColor component
    private TeamColor teamColor;

    // Materials for white
    public Material whiteMaterial;

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

        // Get the TeamColor component
        teamColor = GetComponent<TeamColor>();

        // Apply the normal material to the enemy
        if (teamColor != null)
        {
            teamColor.SetNormalMaterial();
        }
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
        // Switch to white material
        if (teamColor != null && whiteMaterial != null)
        {
            teamColor.GetComponent<Renderer>().material = whiteMaterial;
        }

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Switch back to normal material
        if (teamColor != null)
        {
            teamColor.SetNormalMaterial();
        }
    }
}