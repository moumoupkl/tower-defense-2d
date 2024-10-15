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
        // fait appel a la fonction freeze dans le script de troupMovement
        if (other.gameObject.tag == "Enemy")
        {
            colliders.Add(other);
            other.gameObject.GetComponent<TroupMovement>().SlowOn(freeze.slowStrength);


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {


        colliders.Remove(other); // Retirer les ennemis qui sortent de la zone

    }

}
