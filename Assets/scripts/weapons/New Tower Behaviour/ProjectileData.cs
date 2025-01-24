using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Tower Defense/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 10f; // Speed of the bullet
    public int bulletDamage = 20; // Damage dealt by the bullet
    public float bulletLifetime = 2f; // Lifetime of the bullet in seconds
    public bool bulletPierces = false; // If the bullet can pierce through targets
    public float accuracy = 0f; // The accuracy of the bullet
}