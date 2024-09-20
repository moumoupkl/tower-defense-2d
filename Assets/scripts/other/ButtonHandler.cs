using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public GameObject groundTroup;
    public GameObject flyingTroup;

    private ObjectSpawner objectSpawner;

    void Start()
    {
        objectSpawner = GetComponent<ObjectSpawner>();

        // Add listener to Button 1 to spawn Object 1
        button1.onClick.AddListener(() => objectSpawner.SpawnObject(groundTroup, true));

        // Add listener to Button 2 to spawn Object 2
        button2.onClick.AddListener(() => objectSpawner.SpawnObject(flyingTroup, true));

        button3.onClick.AddListener(() => objectSpawner.SpawnObject(groundTroup, false));

        button4.onClick.AddListener(() => objectSpawner.SpawnObject(flyingTroup, false));

    }
}
