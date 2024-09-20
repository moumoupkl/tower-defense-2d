using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangRange : MonoBehaviour
{
        private List<Collider2D> colliders = new List<Collider2D>();
    private void OnTriggerEnter2D (Collider2D other) {
        colliders.Add(other); 
        Debug.Log("qsygudfqgu");
        
    }

    private void OnTriggerExit2D (Collider2D other) {

    
        colliders.Remove(other); // Retirer les ennemis qui sortent de la zone

    }

}
