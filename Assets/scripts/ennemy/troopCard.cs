using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class troopCard : MonoBehaviour
{
    public GameObject card;
    public Transform maxHeight;
    public Transform minHeight;
    public float maxDistanceBetweenCards;
    public Transform spawnPosition;
    //list of all spawned cards
    public List<GameObject> cards = new List<GameObject>();
    public void Start()
    {
        //get maxheight, minheight and spawnposition from chilldren
        maxHeight = transform.Find("maxHeight");
        minHeight = transform.Find("minHeight");
        spawnPosition = transform.Find("spawnPosition");
    }

    /// <summary>
    /// Spawns a new card at the specified spawn position and sets its sprite and team based on the given troop.
    /// </summary>
    /// <param name="troop">The troop GameObject to base the new card on.</param>
    public void spawn_card(GameObject troop)
    {
        //spawn the card at the spawn position
        GameObject newCard = Instantiate(card, spawnPosition.position, Quaternion.identity);
        //set the spriterenderer of the child of newcard to the sprite of the troop
        newCard.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = troop.GetComponent<SpriteRenderer>().sprite;
        //set blueteam of the same child object to the blueteam of the troop
        newCard.transform.GetChild(0).GetComponent<ObjectStats>().blueTeam = troop.GetComponent<enemyStats>().blueTeam;
        //put the new card in the list of cards
        cards.Add(newCard);
        //move the card to the correct position using movecard of the cardmove script
        newCard.GetComponent<cardMove>().move_card(maxHeight);

    }
}