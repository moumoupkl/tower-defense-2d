using UnityEngine;

public class TeamColor : MonoBehaviour
{
    public Material blueMaterial;
    public Material redMaterial;
    //list of gameobjects that need to change color
    public GameObject[] objectsToChangeColor;
    private Material normalMaterial;
    private Renderer enemyRenderer;
    private bool blueTeam;

    void Start()
    {
        UpdateShader();
    }

    public void UpdateShader()
    {
        // Get the renderer of the enemy itself (not the white child object)
        enemyRenderer = GetComponent<Renderer>();

        //check if there is an ObjectStats component
        ObjectStats objectStats = GetComponent<ObjectStats>();
        if (objectStats != null)
        {
            blueTeam = objectStats.blueTeam;
        }
        else
        {
            //check if there is an enemyStats component
            enemyStats enemyStats = GetComponent<enemyStats>();
            if (enemyStats != null)
            {
                blueTeam = enemyStats.blueTeam;
            }
        }

        if (blueTeam)
        {
            normalMaterial = blueMaterial;
        }
        else
        {
            normalMaterial = redMaterial;
        }

        // Apply the normal material to the enemy
        if (enemyRenderer != null && normalMaterial != null)
        {
            enemyRenderer.material = normalMaterial;
        }

        // Apply the normal material to the list of gameobjects
        ApplyMaterialToObjects();
    }

    public void SetNormalMaterial()
    {
        if (enemyRenderer != null && normalMaterial != null)
        {
            enemyRenderer.material = normalMaterial;
        }
    }

    public Material GetNormalMaterial()
    {
        return normalMaterial;
    }

    private void ApplyMaterialToObjects()
    {
        if (objectsToChangeColor != null && normalMaterial != null)
        {
            foreach (GameObject obj in objectsToChangeColor)
            {
                Renderer objRenderer = obj.GetComponent<Renderer>();
                if (objRenderer != null)
                {
                    objRenderer.material = normalMaterial;
                }
            }
        }
    }
}