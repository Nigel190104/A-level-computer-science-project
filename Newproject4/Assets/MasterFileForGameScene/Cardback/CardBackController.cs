using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackController : MonoBehaviour
{
    //cardback will be assigned through the drag and drop and wont matter which card it is as long as it is attached
    public GameObject CardBack;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    public PlayCards retrieveHasCollided;
    // Start is called before the first frame update
    void Start()
    {//we are assigning the retrieveboll to equal to the spawncards component by finding and object that the spawncards component is attached adn retrieving ita
        retrieveHasCollided = gameObject.GetComponent<PlayCards>();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.parent.gameObject == PlayerArea)
        {
            CardBack.SetActive(false);
        }
        //this will keep checking that if the obejct that this script is attached to is not a child of either players areas as well as the 
        //card has collided then it will remove the card back or rather no display by setting it to be inactive
        if (transform.parent.gameObject != PlayerArea && transform.parent.gameObject != EnemyArea)
        {
            if(retrieveHasCollided.isDraggable == false)
            {
                CardBack.SetActive(false);
            }
        }
        //this checks to make sure that if the gameobject this script is attached to is not the players then it will have a cardback so that the players cant see its stats
        if (transform.parent.gameObject == EnemyArea)
        {
            CardBack.SetActive(true);
        }
    }
}
