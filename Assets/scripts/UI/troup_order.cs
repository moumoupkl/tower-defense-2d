using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troup_order : MonoBehaviour
{
    public GameObject card;
    private GameObject newCard;
    public Transform spawn_point;
    //list off all soawned cards
    public List<GameObject> cards = new List<GameObject>();
    private TroopsAndTowers troopsAndTowers;
    public bool blueteam;
    // Start is called before the first frame update
    void Start()
    {

        troopsAndTowers = Camera.main.GetComponent<TroopsAndTowers>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawn_card(int troopIndex)
    {
        Debug.Log("Spawn card");
        newCard = Instantiate(card, spawn_point.position, spawn_point.rotation);
        //set the spriterenderer of newcards child gameobject to the correct troop sprite
        newCard.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = troopsAndTowers.troopPrefabs[troopIndex].GetComponent<SpriteRenderer>().sprite;
        //set blueteam of the childs objectstat to the correct value
        newCard.transform.GetChild(0).GetComponent<ObjectStats>().blueTeam = blueteam;
        //call update shader from teamcolor from the childgameobject
        newCard.transform.GetChild(0).GetComponent<TeamColor>().UpdateShader();

    }
}
