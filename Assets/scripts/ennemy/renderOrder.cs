using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component
        Renderer renderer = GetComponent<Renderer>();

        // Check if the Renderer component exists
        if (renderer != null)
        {
            // Calculate the sorting order based on the negative y-position
            int sortingOrder = Mathf.RoundToInt(-transform.position.y);

            // Set the sorting order
            renderer.sortingOrder = sortingOrder;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}