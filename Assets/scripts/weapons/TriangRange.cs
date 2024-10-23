using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangRange : MonoBehaviour
{
    public List<Collider2D> colliders = new List<Collider2D>();
    private List<TroupMovement> troupMovements = new List<TroupMovement>();
    public FreezerShootBehaviour freeze;

    private void Start()
    {

        if (freeze == null)
        {
            Debug.LogError("FreezerShootBehaviour component not found on the GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("slow");
        // Vérifie si l'objet entrant est un ennemi
        if (other.gameObject.tag == "Enemy")
        {
            colliders.Add(other);
            TroupMovement troupMovement = other.gameObject.GetComponent<TroupMovement>();
            if (troupMovement != null)
            {
                troupMovements.Add(troupMovement);
                // Vérifie si freeze n'est pas null avant d'appeler SlowOn
                if (freeze != null)
                {
                    // Appelle la fonction SlowOn pour ralentir l'ennemi
                    troupMovement.SlowOn(freeze.slowStrength);
                }
                else
                {
                    Debug.LogError("Freeze component is null. Cannot call SlowOn.");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Retirer les ennemis qui sortent de la zone
        if (other.gameObject.tag == "Enemy")
        {
            colliders.Remove(other);
            TroupMovement troupMovement = other.gameObject.GetComponent<TroupMovement>();
            if (troupMovement != null)
            {
                troupMovements.Remove(troupMovement);
            }
        }
    }
}