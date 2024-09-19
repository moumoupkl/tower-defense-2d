using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class tiles : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public Animator animator;
    public int weaponPrice;
    public GameManager gameManager;
    public bool hover;
    public bool activeConstruction;
    public float constructionTime;
    public GameObject particles;

    public Transform tile;
    
    void Start()
    {
        hover = false;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    private void OnSelectp1()
    {
        if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
        {
            activeConstruction = true;
            StartCoroutine(SpawnObject(turret1, tile));
        }
    }

    void Update()
    {
        if (hover && !activeConstruction)
        {
            animator.SetBool("ishover", true);

            if (Input.GetKeyDown(KeyCode.Space)) // Space for turret1
            {
                if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
                {
                    activeConstruction = true;
                    StartCoroutine(SpawnObject(turret1, tile));
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) // Left Shift for turret2
            {
                if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
                {
                    activeConstruction = true;
                    StartCoroutine(SpawnObject(turret2, tile));
                }
            }
        }
        else
        {
            animator.SetBool("ishover", false);
        }
    }

    IEnumerator SpawnObject(GameObject turret, Transform spawnPosition)
    {
        gameManager.AddCoins(-weaponPrice);

        if (particles != null)
        {
            Debug.Log(spawnPosition.transform.position);
            GameObject spawnedParticles = Instantiate(particles, spawnPosition.transform.position, Quaternion.identity);
            Particle particleScript = spawnedParticles.GetComponent<Particle>();

            if (particleScript != null)
            {
                particleScript.SetLifetime(constructionTime);
            }
        }

        float elapsedTime = 0f;

        while (elapsedTime < constructionTime)
        {
            if (!gameManager.pause)
            {
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }

        Instantiate(turret, spawnPosition.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
