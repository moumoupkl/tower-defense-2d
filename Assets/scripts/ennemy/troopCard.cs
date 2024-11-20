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
    public float distanceBetweenCards;
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
        //move all cards to the correct position
        set_distance();
        move_cards();   
    }

    /// <summary>
    /// Removes the card at the given index from the list and moves all other cards to the correct position.
    /// </summary>
    /// <param name="index">The index of the card to remove.</param>
    public void remove_card(int index)
    {
        Debug.Log("removing card at index: " + index);
        //destroy the card at the given index
        Destroy(cards[index]);
        //remove the card from the list
        cards.RemoveAt(index);
        //move all other cards to the correct position
        set_distance();
        move_cards();
    }

    /// <summary>
    /// Moves all cards in the list to the correct position based on their index in the list spcaing them with the distance distanceBetweenCards.
    /// </summary>
    public void move_cards()
    {
        //for each card in the list
        for (int i = 0; i < cards.Count; i++)
        {
            //calculate the final position for the card
            Vector3 finalPosition = new Vector3(maxHeight.position.x, maxHeight.position.y - i * distanceBetweenCards, maxHeight.position.z);
            //move the card to the correct position
            cards[i].GetComponent<cardMove>().move_card(finalPosition);
        }
    }

    /// <summary>
    /// set the biggest distance between cards so that they are not lower than the minHeight
    /// </summary>
    public void set_distance()
    {
        //get the number of cards
        int cardCount = cards.Count;
        //calculate the biggest distance between cards
        distanceBetweenCards = (maxHeight.position.y - minHeight.position.y) / cardCount;
        //if the distance between cards is bigger than the max distance between cards set the distance between cards to the max distance between cards
        if (distanceBetweenCards > maxDistanceBetweenCards)
        {
            distanceBetweenCards = maxDistanceBetweenCards;
        }
    }
    
}