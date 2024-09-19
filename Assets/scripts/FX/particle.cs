using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifetime;
    private ParticleSystem ps;
    private bool started;

    void Start()
    {
        started = false;
        ps = GetComponent<ParticleSystem>();
        ps.Stop(); // Stop initially; wait for the lifetime to be set
    }

    void Update()
    {
        // Make sure that we start the particle system only after the lifetime has been properly set
        if (!started)
        {
            started = true;
            ps.Play();
        }
    }

    // Optionally, you can provide a method to set the lifetime from another script
    public void SetLifetime(float newLifetime)
    {
        lifetime = newLifetime;

        // Access the ParticleSystem component
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            // Access the main module of the particle system
            var mainModule = ps.main;

            // Set the particle system's duration to match the lifetime
            mainModule.duration = lifetime;

            // You can also ensure that the particle system is destroyed after its lifetime, if needed
            Destroy(gameObject, lifetime + 2);
            
        }
        
    }

}
