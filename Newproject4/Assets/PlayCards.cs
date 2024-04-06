using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;


public class PlayCards : NetworkBehaviour
{
    //the gameobject cnavas is used ot be able to make sure that while the cards are beign dragged they're parent is
    //the ,main canvas, so that they will be diplayed on top of the ui in the
    //game instead of below it the player and enemy area we dont want the cards ot behind them as
    //the player cant see them whils they are in that section of the screen
    public GameObject Canvas;
    //this scirpt is attahced ot both card one and card two which allows for the 
    //two funciton to be called by putting the object into the event trigger component
    private bool isDragging = false;
    public Playermanager PlayerManager;
    public GameManager GameManager;
    private bool isOverSlotArea = false;
    //this makes sure that this variable can not be changed in the inspoector viesw  
    [HideInInspector]
    public bool isDraggable = true;
    private GameObject startParent;
    private GameObject slotArea;

    private Vector2 startposition;
    //these cards will be assigned in the inspectore panel
    public GameObject Normalcards0;
    public GameObject Normalcards1;
    public GameObject Normalcards2;
    public GameObject Normalcards3;
    public GameObject Normalcards4;
    public GameObject Normalcards5;
    public GameObject Normalcards6;
    public GameObject Normalcards7;
    public GameObject Normalcards8;
    public GameObject Normalcards9;

    //will be assigned through drag and drop
    public GameObject Legendarycards0;
    public GameObject Legendarycards1;
    public GameObject Legendarycards2;
    public GameObject Legendarycards3;
    public GameObject Legendarycards4;
    public GameObject Legendarycards5;
    public GameObject Legendarycards6;
    public GameObject Legendarycards7;
    public GameObject Legendarycards8;
    public GameObject Legendarycards9;
    //this has been created so that i cna have list that will contain either the
    //normal cards gameobjects or the legendary card gameobjects in the game
    //this will be used so that the cards and the functions that will be perfomed on them will be different 
    //as well as this allows for the game to tell the difference between the two as
    //checks can be perfomed so that dependign on the list they are assigned to 
    //varoius actions will be performed and the assignment of cards values will be correct
    List<GameObject> RetrieveNormalCards = new List<GameObject>();
    List<GameObject> RetrieveLegendaryCards = new List<GameObject>();

    //the stats below will later be made equal to the stats of the random card generate in the start function
    public int StrengthStat;
    public int SpeedStat;
    public float SizeStat;
    public int MassStat;
    public string CardName;

    //this will be used to access the cards in the playermanager script and acces the indexes and will leter be made equal
    //to the playermanager normalcardlist adn will be used to retreive the card
    //indexes
    public static List<NormalCardsScript> RetrieveCardDataNormalCard = new List<NormalCardsScript>();

    //will be used ot acccess the  legendary card data through indexes of the legednary cardist in the playermanager
    public static List<LegendaryCardsScript> RetrieveCardDataLegendaryCard = new List<LegendaryCardsScript>();

    //these will be assigned by drag and drop after attachign this script to its respecive card which is
    //normalcard and draggin the different text fields into it
    public Text StrengthStatText;
    public Text SpeedStatText;
    public Text SizeStatText;
    public Text MassStatText;
    public Text CardNameText;
    //these images will be the dinosaur image displayed for each type of card
    public Sprite CardImage;
    public Image thisCardImage;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveNormalCards.Add(Normalcards0);
        RetrieveNormalCards.Add(Normalcards1);
        RetrieveNormalCards.Add(Normalcards2);
        RetrieveNormalCards.Add(Normalcards3);
        RetrieveNormalCards.Add(Normalcards4);
        RetrieveNormalCards.Add(Normalcards5);
        RetrieveNormalCards.Add(Normalcards6);
        RetrieveNormalCards.Add(Normalcards7);
        RetrieveNormalCards.Add(Normalcards8);
        RetrieveNormalCards.Add(Normalcards9);


        

        Canvas = GameObject.Find("Canvas");
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //this is the only way in which we can access the playermanager script since the object it is attached to is
        //not in the scene until it is instatniated by the network manager
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<Playermanager>();
        RetrieveCardDataNormalCard = PlayerManager.NormalcardsList;

        //this was added to allow for performing of specific function and determining if something should happen based on the fact taht
        //it the card being assigned is a legendary card
        RetrieveLegendaryCards.Add(Legendarycards0);
        RetrieveLegendaryCards.Add(Legendarycards1);
        RetrieveLegendaryCards.Add(Legendarycards2);
        RetrieveLegendaryCards.Add(Legendarycards3);
        RetrieveLegendaryCards.Add(Legendarycards4);
        RetrieveLegendaryCards.Add(Legendarycards5);
        RetrieveLegendaryCards.Add(Legendarycards6);
        RetrieveLegendaryCards.Add(Legendarycards7);
        RetrieveLegendaryCards.Add(Legendarycards8);
        RetrieveLegendaryCards.Add(Legendarycards9);

        //used to access te legendary cards list which contaisn data abotu dinousars such as their stats images and name
        //with a different dinosaur at eahc index of the lsit
        RetrieveCardDataLegendaryCard = PlayerManager.LegendarycardsList;

        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //the way in which card gamobjects will have their values assigned has been condensed to
        //this as it requires less code as is reletively more efficeint
        // and redices the amount of code and lines needed for eddti
        //the first conditon just checks which list the assgined game object is attached
        //to then will perform actions specifed below based on this
        if (RetrieveLegendaryCards.Contains(gameObject))
        {
            //this is used ot access the index of the gamobject this script is
            //attacjed to which will be one out of the 20 cards
            int CardIndex = RetrieveLegendaryCards.IndexOf(gameObject);
            //based on index of the card in its respectice list it will
            //retrieve the respective card data for that
            //specific card from the lsit in the playermanager script
            RetrieveCardDataLegendaryCard[0] = PlayerManager.LegendarycardsList[CardIndex];

            //thsi is settign the stats of the card accordingly to the randomly
            //selected object within the normalcardslist list
            StrengthStat = RetrieveCardDataLegendaryCard[0].StrengthStat;
            SpeedStat = RetrieveCardDataLegendaryCard[0].SpeedStat;
            SizeStat = RetrieveCardDataLegendaryCard[0].SizeStat;
            MassStat = RetrieveCardDataLegendaryCard[0].MassStat;
            CardName = RetrieveCardDataLegendaryCard[0].CardName;

            CardImage = RetrieveCardDataLegendaryCard[0].CardImage;
            thisCardImage.sprite = CardImage;
        }
        if (RetrieveNormalCards.Contains(gameObject))
        {
            int CardIndex = RetrieveNormalCards.IndexOf(gameObject);

            RetrieveCardDataNormalCard[0] = PlayerManager.NormalcardsList[CardIndex];

            //thsi is settign the stats of the card accordingly to the randomly selected object within the normalcardslist list
            StrengthStat = RetrieveCardDataNormalCard[0].StrengthStat;
            SpeedStat = RetrieveCardDataNormalCard[0].SpeedStat;
            SizeStat = RetrieveCardDataNormalCard[0].SizeStat;
            MassStat = RetrieveCardDataNormalCard[0].MassStat;
            CardName = RetrieveCardDataNormalCard[0].CardName;

            CardImage = RetrieveCardDataNormalCard[0].CardImage;
            thisCardImage.sprite = CardImage;
        }

        //this changes the text of the card this script is attached to teh stats of the random card generated
        CardNameText.text = "" + CardName;
        SpeedStatText.text = "Speed: " + SpeedStat;
        SizeStatText.text = "Size: " + SizeStat;
        MassStatText.text = "Mass: " + MassStat;
        StrengthStatText.text = "Strength:" + StrengthStat;
        
        if (isDragging)
        {
            //every frame will check if dragging is true it will trandorm the object being clicked
            //by the muse tro transform its position by wherever the mouse is
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
    //when any of the card collide with the dropzone what will happen is the dropzone essentially
    //becomes its parent so contains it
    //collison in brackets can be changed to anythin you want it to be
    //collion 2d in the brackets means that when theres a 2d collion between two objects there will be a collion when 
    //the obejct the sciprt is attached colides with an object (card or dropzone) what is insidet the second paranthesis will occur
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this line of the code only alows a collision between two objects being the slot area and the card
        //this checks that depenign on the card type if theres is a collison detected then it will assign it to is respective area in the game
        //legednary cards will be assigned to their respective slot areas in the game and the same for normal cards
        if (RetrieveNormalCards.Contains(gameObject))
        {
            if (collision.gameObject == PlayerManager.PlayerNormalCardAsignmentArea[PlayerManager.AmountOfCardsPlayed])
            {
                isOverSlotArea = true;
                slotArea = collision.gameObject;
            }
        }
        if (RetrieveLegendaryCards.Contains(gameObject))
        {
            if (collision.gameObject == PlayerManager.PlayerLegendaryCardAsignmentArea[PlayerManager.AmountOfCardsPlayed])
            {
                isOverSlotArea = true;
                slotArea = collision.gameObject;
            }
        }
    }

    // if we miss the dropzone or we decide not to place it on the dropzone by releasing the clicked card the card wil return back to either the enemey areda or the player area
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverSlotArea = false;
        slotArea = null;
    }


    //for both start drag and end drag these function are called when the even triggers which is when the cards are clicked then dragged which is a script attached to the cards
    public void StartDrag()
    {
        //this means if the persion does not have authoity dont do anything
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        //whne we call the start drag methos we want the starposition to be equla to the transfrom position 
        startposition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;
        isDragging = false;
        //this works with the collision method above as ti checks to see
        //if the the card has collided with the fropsozone once
        //the player has relesased the card and if they have the, 
        // the parent of the card changes to the dropzone, and it the cards
        // moves to the dropzones/slots position this now also checks that if it 
        //is the players turn then they can assign a card to its designated slot
        if (isOverSlotArea && PlayerManager.IsPlayersTurnToAssignCards)
        {
            //sets the dropzoen to be the parent if the the playre has
            //stopped dragging the card and it is over the dropzeon
            transform.SetParent(slotArea.transform, false);
            isDraggable = false;
            //thsi has been done as this script is attached to both
            //legendary cards and normal cards which will have different methods used within 
            //the player manager script to instaniate or transform them ot different gameobjects
            //below it is checking which list the gameobject belongs
            //to and based of that will call differetn methods in the playermanager script 
            if(RetrieveNormalCards.Contains(gameObject))
            {
                PlayerManager.PlayCard(gameObject);
            }
            if(RetrieveLegendaryCards.Contains(gameObject))
            {
                PlayerManager.PlayLegendaryCard(gameObject);
            }
        }
        //it will return its origal parent and still be a child of it
        else
        {
            transform.position = startposition;
            transform.SetParent(startParent.transform, false);
        }
    }

}
//i received some help from mrs farzan card game tutorial with the making of this script