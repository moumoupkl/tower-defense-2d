using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_particle : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
    void Start()
    {

        Destroy(gameObject,lifetime);
    }

}
