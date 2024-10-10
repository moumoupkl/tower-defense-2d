using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTower : MonoBehaviour
{   //list of sprites
    public List<Sprite> sprites;
    // Start is called before the first frame update
    void Start()
    {
        //get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //get a random sprite from the list
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)]; 
    }
}
