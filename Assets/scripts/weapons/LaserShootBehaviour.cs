using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootBehaviour : MonoBehaviour
{
    private CDDamageDower CDdamageTower;
    public Transform target;
    public int laserDamage = 10;
    public LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        CDdamageTower = GetComponent<CDDamageDower>();
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootLaser()
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, transform.position);

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(
            direction.x > 0 ? Screen.width : 0,
            direction.y > 0 ? Screen.height : 0,
            Camera.main.nearClipPlane));

        laserLine.SetPosition(1, screenEdge);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {

                if (enemy != null)
                {
                    enemy.TakeDamage(laserDamage);
                }*/
            }
        }
    }
}