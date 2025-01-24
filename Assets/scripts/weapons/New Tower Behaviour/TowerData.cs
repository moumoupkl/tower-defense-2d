using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower Data")]
public class TowerData : ScriptableObject
{
    public enum TargetPriority { First, Last, Closest }

    [Header("General Stats")]
    public float range = 5f;
    public float aimAngle = 70f;
    public float attackSpeed = 1f; // Attacks per second
    public float rotationSpeed = 180f; // Degrees per second
    public TargetPriority targetingPriority = TargetPriority.First;

    [Header("Cost and Upgrade")]
    public int buildCost = 100;
    public float builTime = 5f;
    public int sellValue = 50;
    public int maxUpgradeLevel = 3;

    [Header("Special Options")]
    public bool canTargetAir = false;
    public bool canTargetGround = true;
    public bool isMultiShot = false;
    public int multiShotCount = 3; // If isMultiShot is true

    [Header("AoE Settings")]
    public bool isAoe = false; // Determines if the tower attacks an area
    public int aoeDamage = 30; // Damage dealt by the AoE attack

    [Header("Projectile")]
    public GameObject projectilePrefab;

}
