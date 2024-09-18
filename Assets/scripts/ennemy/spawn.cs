using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject troup;
    public GameManager gameManager;

    void Start()
    {
        Camera mainCamera = Camera.main;
        gameManager = mainCamera.GetComponent<GameManager>();
    }
    void OnMouseDown()
    {
        if(!gameManager.pause)
        {
            if (troup == null)
            {
                Debug.LogError("Troup prefab not assigned!");
                return;
            }

            Instantiate(troup, Vector2.zero, Quaternion.identity);

            Debug.Log("Spawned troup");
        }
    }
}