using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangRange : MonoBehaviour
{
    public List<Collider2D> colliders = new List<Collider2D>();
    private FreezerShootBehaviour freeze;




    private void Start()
    {
        freeze = GetComponent<FreezerShootBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet entrant est un ennemi
        if (other.gameObject.tag == "Enemy")
        {
            colliders.Add(other);
            // Appelle la fonction SlowOn pour ralentir l'ennemi
            other.gameObject.GetComponent<TroupMovement>().SlowOn(freeze.slowStrength);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Retirer les ennemis qui sortent de la zone
        if (other.gameObject.tag == "Enemy")
        {
            colliders.Remove(other);
        }
    }
}