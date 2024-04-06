using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SpawnCards : NetworkBehaviour
{
    public Playermanager PlayerManager;
    public GameManager GameManager;
    //this used to chang the text displays in the game
    public UIManager UIManager;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }



    public void PlayerClickButton()
    {
        //we are storing the network identity of the person who has pressed the draw buton/that clients connection
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        //this finds the playermanager that exists in that client(the person who has connected/ presed the draw cards button)
        PlayerManager = networkIdentity.GetComponent<Playermanager>();
        //this will acces the method in the playermanager script to be abel to spawn
        //the cards in the game once the player presses the draw cards button
        if (GameManager.ButtonState == "Can Draw Cards")
        {
            canDrawCardsClick();
        }
        //this will access the method in hte playermanager script which will remove cards from the card
        //from the dropzone
        if (GameManager.ButtonState == "Remove Cards From Game")
        {
            RemoveCardsFromGameClick();
        }
        
    }

    void canDrawCardsClick()
    {
        PlayerManager.CmdSpawnCards();
        PlayerManager.AmountOfCardsPlayed = 0;
        //this has been added so that only when the host presses the button does it change the cardstat for round for both players
        if (!isClientOnly)
        {
            PlayerManager.CmdSettingCardStatForRound();
        }

    }

    void RemoveCardsFromGameClick()
    {  
        PlayerManager.CmdComparingCardsInSlotAreas();
        PlayerManager.CmdRemove_Cards_From_Game();
        PlayerManager.CmdGameManagerChangeButonState("Can Draw Cards");
    }
}
