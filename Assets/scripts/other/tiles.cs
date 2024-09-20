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

    
    void Start()
    {
        hover = false;
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }

    private void OnSelectp1()// triggered on right shift
    {
        Debug.Log("right shift");
        if (hover && !activeConstruction)
        {
            if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
            {
                activeConstruction = true;
                StartCoroutine(SpawnObject(turret1));
            }
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
                    StartCoroutine(SpawnObject(turret1));
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) // Left Shift for turret2
            {
                if (!gameManager.pause && gameManager.currentCoins >= weaponPrice)
                {
                    activeConstruction = true;
                    StartCoroutine(SpawnObject(turret2));
                }
            }
        }
        else
        {
            animator.SetBool("ishover", false);
        }
    }

    IEnumerator SpawnObject(GameObject turret)
    {
        gameManager.AddCoins(-weaponPrice);

        if (particles != null)
        {
            Debug.Log(transform.position);
            GameObject spawnedParticles = Instantiate(particles, transform.position, Quaternion.identity);
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

        Instantiate(turret, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
