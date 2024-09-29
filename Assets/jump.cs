using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Vector2 jumpForce;
    public Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        jumpForce = new Vector2(5,2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            tr.position = new Vector3(3, 2, 0);
        }
                
    }
}
