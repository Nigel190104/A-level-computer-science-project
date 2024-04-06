using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;


//not deriving from monobehavouur due to the fact that we are using networking 
public class Playermanager : NetworkBehaviour
{
    //public GameObject Specialeffectcards;
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
    //this holds the normalcards prefab which is used as the template for all the different types of cards with the values 
    //being assigned in the playcards script since that is attached to the normal cards prefab
    //instead of getting one of the gameobjects listed below and using the gameobject.find() function what we
    //can do instead is do drag and drop in the inspector panel
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
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    public GameObject PlayerSlot1;
    public GameObject PlayerSlot2;
    public GameObject PlayerSlot3;
    public GameObject PlayerSlot4;

    public GameObject EnemySlot1;
    public GameObject EnemySlot2;
    public GameObject EnemySlot3;
    public GameObject EnemySlot4;

    public GameManager GameManager;

    //the lists below will contain theire respective slots e.g. for that specific player depending on their network identity
    //they can only interact with their playersocket and not the enemies meaning
    //that they cant assign cards anywhere on the field only where the playersockets for that player is and the
    //correct slot for that turn is specifically being used as it will incremnt up from 0-1
    //in regards to indexes and allow for the player to be able interact with the slots and the cards
    public List<GameObject> PlayerNormalCardAsignmentArea = new List<GameObject>();
    public List<GameObject> EnemyNormalCardAsignmentArea = new List<GameObject>();

    //the list will contain the specific slots for Legenary card Slots
    public List<GameObject> PlayerLegendaryCardAsignmentArea = new List<GameObject>();
    public List<GameObject> EnemyLegendaryCardAsignmentArea = new List<GameObject>();

    //public List<GameObject> PlayerEffectCardAsignmentArea = new List<GameObject>();
    //public List<GameObject> EnemyEffectCardAsignmentArea = new List<GameObject>();

    //these are lists used ot be able store the different card objects that will be instantiated in the game
    List<GameObject> NormalCardsList2 = new List<GameObject>();
    List<GameObject> LegendaryCardsList2 = new List<GameObject>();

    public List<NormalCardsScript> NormalcardsList = new List<NormalCardsScript>();
    public List<LegendaryCardsScript> LegendarycardsList = new List<LegendaryCardsScript>();
    //this is a boolean used to determine if it is the palyers tutn or  not 
    [HideInInspector]
    public bool IsPlayersTurnToAssignCards = false;
    //a variable used to keep track of the amount of card played by the player

    [HideInInspector]
    public int AmountOfCardsPlayed = 0;

    //public int CardStatRandomGenerator;
    //we dont use the start fucntion due to the fact theate we are 
    //the server starts the onstart client happens as soon as the player connects ot the server
    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");

        PlayerSlot1 = GameObject.Find("Playerslot1");
        PlayerSlot2 = GameObject.Find("PlayerSlot2");
        PlayerSlot3 = GameObject.Find("PlayerSlot3");
        PlayerSlot4 = GameObject.Find("PlayerSlot4");

        EnemySlot1 = GameObject.Find("EnemySlot1");
        EnemySlot2 = GameObject.Find("Enemyslot2");
        EnemySlot3 = GameObject.Find("Enemyslot3");
        EnemySlot4 = GameObject.Find("Enemyslot4");
        //adding the slot areas to their respective list which will allow for us to access them through the list
        //and control whihc one can be accessed based on list order and the index incrementing per card played so 
        //that the player can only collide with the slot if it equals the index of the amount of cards currently played and it is their turn to play

        PlayerNormalCardAsignmentArea.Add(PlayerSlot4);

        //this was added with similar functionality to the way normal cards would have worked so that players were limited
        //to only assiggning legndayr cards to their respective areas in the game and only abel to assign one card per round normal
        //and legenary cards inclusive count as cards;
        EnemyLegendaryCardAsignmentArea.Add(EnemySlot1);
        PlayerLegendaryCardAsignmentArea.Add(PlayerSlot1);

        EnemyNormalCardAsignmentArea.Add(EnemySlot4);

        //adds these object references to the normalcardslist
        //resources is the name of the file in which these dinosaur pictures are located in
        NormalcardsList.Add(new NormalCardsScript(180000, 4f, 20, 7000, "T-Rex", Resources.Load<Sprite>("trex")));
        NormalcardsList.Add(new NormalCardsScript(1000, 1.8f, 40, 50, "Velociraptor", Resources.Load<Sprite>("Velociraptor")));
        NormalcardsList.Add(new NormalCardsScript(33, 7.0f, 35, 450, "Dilophosaurus", Resources.Load<Sprite>("dipholosaurus")));
        NormalcardsList.Add(new NormalCardsScript(0, 6.0f, 30, 907, "Megalosaurus", Resources.Load<Sprite>("Megalosaurus")));
        NormalcardsList.Add(new NormalCardsScript(50000, 1.0f, 40, 3, "Compsagnathus", Resources.Load<Sprite>("comsognathus")));
        NormalcardsList.Add(new NormalCardsScript(100, 26f, 10, 75000, "Dreadnoughtus", Resources.Load<Sprite>("dreaghnautus")));
        NormalcardsList.Add(new NormalCardsScript(200, 37.2f, 15, 70000, "Titanosaur", Resources.Load<Sprite>("titanosaur")));
        NormalcardsList.Add(new NormalCardsScript(150, 40.0f, 8, 100000, "Argentinosaurus", Resources.Load<Sprite>("Argentinosaurus")));
        NormalcardsList.Add(new NormalCardsScript(125, 12.8f, 20, 75000, "Saltasaurus", Resources.Load<Sprite>("saltasaurus")));
        NormalcardsList.Add(new NormalCardsScript(120000, 30.5f, 20, 75000, "Paralititan", Resources.Load<Sprite>("paralititan")));

        //this will add cards to the legendary cards list by creatign new references and addign them to the list these will be used in the playcard2 script to
        //be able to determine teh stats of the respective dinosaurs should they be assigned to a card in the random index generator in playcards
        LegendarycardsList.Add(new LegendaryCardsScript(180000, 4f, 20, 7000, "Mega T-Rex", Resources.Load<Sprite>("trex")));
        LegendarycardsList.Add(new LegendaryCardsScript(1000, 1.8f, 40, 50, "Mega Velociraptor", Resources.Load<Sprite>("Velociraptor")));
        LegendarycardsList.Add(new LegendaryCardsScript(33, 6.1f, 20, 450, "Mega Dilophosaurus", Resources.Load<Sprite>("dipholosaurus")));
        LegendarycardsList.Add(new LegendaryCardsScript(0, 6.0f, 30, 907, "Mega Megalosaurus", Resources.Load<Sprite>("Megalosaurus")));
        LegendarycardsList.Add(new LegendaryCardsScript(50000, 1.0f, 40, 3, "Mega Compsagnathus", Resources.Load<Sprite>("comsognathus")));
        LegendarycardsList.Add(new LegendaryCardsScript(100, 26f, 10, 75000, "Mega Dreadnoughtus", Resources.Load<Sprite>("dreaghnautus")));
        LegendarycardsList.Add(new LegendaryCardsScript(200, 37.2f, 15, 70000, "Mega Titanosaur", Resources.Load<Sprite>("titanosaur")));
        LegendarycardsList.Add(new LegendaryCardsScript(150, 40.0f, 8, 100000, "Mega Argentinosaurus", Resources.Load<Sprite>("Argentinosaurus")));
        LegendarycardsList.Add(new LegendaryCardsScript(125, 12.8f, 20, 75000, "Mega Saltasaurus", Resources.Load<Sprite>("saltasaurus")));
        LegendarycardsList.Add(new LegendaryCardsScript(120000, 30.5f, 20, 75000, "Mega Paralititan", Resources.Load<Sprite>("paralititan")));
        //this checks that if the connection type is that of only a client then it will set the boolean controlling if it is the players turn equal to true,
        //therefore the player acting as the host(client and server) will always act second in a round
        if (isClientOnly)
        {
            IsPlayersTurnToAssignCards = true;
        }

    }
    private void Update()
    {
        
    }
    //below once the server starts it will perform the actions specified within this method
    [Server]
    public override void OnStartServer()
    {

        //in this we are usign the cardattached to list to eb able access the normalcardlsit list in spawncards and generate a random object from the list
        //cardAttachedTo[0] = NormalcardsList[thisCardNumber];
        //this adds the template normal cards to normalcardslist2
        NormalCardsList2.Add(Normalcards0);
        NormalCardsList2.Add(Normalcards1);
        NormalCardsList2.Add(Normalcards2);
        NormalCardsList2.Add(Normalcards3);
        NormalCardsList2.Add(Normalcards4);
        NormalCardsList2.Add(Normalcards5);
        NormalCardsList2.Add(Normalcards6);
        NormalCardsList2.Add(Normalcards7);
        NormalCardsList2.Add(Normalcards8);
        NormalCardsList2.Add(Normalcards9);

        
        //this list wont be used yet just there so that it is ready for when it is needed
        LegendaryCardsList2.Add(Legendarycards0);
        LegendaryCardsList2.Add(Legendarycards1);
        LegendaryCardsList2.Add(Legendarycards2);
        LegendaryCardsList2.Add(Legendarycards3);
        LegendaryCardsList2.Add(Legendarycards4);
        LegendaryCardsList2.Add(Legendarycards5);
        LegendaryCardsList2.Add(Legendarycards6);
        LegendaryCardsList2.Add(Legendarycards7);
        LegendaryCardsList2.Add(Legendarycards8);
        LegendaryCardsList2.Add(Legendarycards9);
    }
    //command is needed to tell the server from the client that an action needs to be performed
    [Command]
    public void CmdSpawnCards()
    {
        if (GameManager.Rounds == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                //creates an instance of the card object at the origin and wwe say at eh quaternion.idenitty that it has no rotation
                GameObject card = Instantiate(NormalCardsList2[Random.Range(0, NormalCardsList2.Count)], new Vector2(0, 0), Quaternion.identity);
                //connectiontoclient detemrines that the authority of the card instantiated will be of client level in terms of the accesslevel
                //this means that it will assign authority to the player it is connected to
                NetworkServer.Spawn(card, connectionToClient);
                RpcShowCard(card, "Dealt");

            }
        }
        if (GameManager.Rounds != 0)
        {
            //creates an instance of the card object at the origin and wwe say at eh quaternion.idenitty that it has no rotation
            GameObject card = Instantiate(NormalCardsList2[Random.Range(0, NormalCardsList2.Count)], new Vector2(0, 0), Quaternion.identity);
            //connectiontoclient detemrines that the authority of the card instantiated will be of client level in terms of the accesslevel
            //this means that it will assign authority to the player it is connected to
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "Dealt");
        }
        RpcGameManagerChangeButonState("Compiling results of the round");
    }


    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
        if (type == "Dealt")
        {
            //this is basically saying that if the cards instantiated belong to that player or their connection id
            //matches the card instantiated then it will be set to their playerarea if not then it will be set on their screen to the enemyarea
            //this is basically saying that if the cards instantiated belong to that player or their connection id
            //matches the card instantiated then it will be set to their playerarea if not then it will be set on their screen to the enemyarea
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
            }
            else
            {
                card.transform.SetParent(EnemyArea.transform, false);
            }
        }
        else if (type == "played")
        {
            //players will only be playing a maximum of one legendary or special effect per round
            if (AmountOfCardsPlayed == 1 && PlayerSlot1.transform.childCount == 0)
            {
                AmountOfCardsPlayed = 0;
            }
            //this is saying that when the card is beign isntatniated ot its parent obejct if the
            //player has authority over the card then assign it to their player area
            if (hasAuthority && PlayerSlot1.transform.childCount == 0)
            {
                card.transform.SetParent(PlayerNormalCardAsignmentArea[AmountOfCardsPlayed].transform, false);
                CmdGameManagerCardPlayed();
            }
            //if the player however does not have authority over the card then it will display in taht players screen biew in the enemy area slot
            if (!hasAuthority && EnemySlot1.transform.childCount == 0)
            {
                card.transform.SetParent(EnemyNormalCardAsignmentArea[AmountOfCardsPlayed].transform, false);
            }
            if (PlayerSlot1.transform.childCount != 0) return;

            //incrementing the amount of cards played by that player
            AmountOfCardsPlayed++;
            //the code below is used ot be able to aid with the control of turns for players
            Playermanager TurnManager = NetworkClient.connection.identity.GetComponent<Playermanager>();
            //below essentially once players have already assigned cards to the area it determines that it is no longer
            //that pkayers turn and if it isnt their turn it now sets so that it is noe their turn ot assign a card
            TurnManager.IsPlayersTurnToAssignCards = !TurnManager.IsPlayersTurnToAssignCards;
        }
    }

    //command is needed to tell the server from the client that an action needs to be performed
    [Command]
    public void CmdSpawnLegendaryCards()
    {
        //creates an instance of the card object at the origin and wwe say at eh quaternion.idenitty that it has no rotation
        GameObject card = Instantiate(LegendaryCardsList2[Random.Range(0, LegendaryCardsList2.Count)], new Vector2(0, 0), Quaternion.identity);
        //connectiontoclient detemrines that the authority of the card instantiated will be of client level in terms of the accesslevel
        //this means that it will assign authority to the player it is connected to
        NetworkServer.Spawn(card, connectionToClient);
        RpcShowLegendaryCard(card, "Dealt", EnemyArea, PlayerArea);
    }


    [ClientRpc]
    void RpcShowLegendaryCard(GameObject card, string type, GameObject AsignmentArea, GameObject AsignmentArea2)
    {
        if (type == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(AsignmentArea2.transform, false);
            }
            else
            {
                card.transform.SetParent(AsignmentArea.transform, false);
            }
        }
        else if (type == "played")
        {
            //players will only be playing a maximum of one legendary or special effect per round
            if (AmountOfCardsPlayed == 1 && PlayerSlot4.transform.childCount == 0)
            {
                AmountOfCardsPlayed = 0;
            }
            //this is saying that when the card is beign isntatniated ot its parent obejct if the
            //player has authority over the card then assign it to their player area
            if (hasAuthority && PlayerSlot4.transform.childCount == 0)
            {
                card.transform.SetParent(PlayerLegendaryCardAsignmentArea[AmountOfCardsPlayed].transform, false);
                CmdGameManagerCardPlayed();
            }
            //if the player however does not have authority over the card then it will display in taht players screen biew in the enemy area slot
            if (!hasAuthority && EnemySlot4.transform.childCount == 0)
            {
                card.transform.SetParent(EnemyLegendaryCardAsignmentArea[AmountOfCardsPlayed].transform, false);
            }
            //this line is used to make sure that the turnorder is not incremented if the player has already played a card that round as well as making sure that
            //the amount of cards played is not incremented which may cause errors in gameplay
            if (PlayerSlot4.transform.childCount != 0) return;
            //incrementing the amount of cards played by that player
            AmountOfCardsPlayed++;
            //the code below is used ot be able to aid with the control of turns for players
            Playermanager TurnManager = NetworkClient.connection.identity.GetComponent<Playermanager>();
            //below essentially once players have already assigned cards to the area it determines that it is no longer
            //that pkayers turn and if it isnt their turn it now sets so that it is noe their turn ot assign a card
            TurnManager.IsPlayersTurnToAssignCards = !TurnManager.IsPlayersTurnToAssignCards;
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlaycard(card);
    }

    [Command]
    void CmdPlaycard(GameObject card)
    {
        RpcShowCard(card, "played");
    }

    public void PlayLegendaryCard(GameObject card)
    {
        CmdPlayLegendarycard(card);
    }

    [Command]
    void CmdPlayLegendarycard(GameObject card)
    {
        RpcShowLegendaryCard(card, "played", EnemyArea, PlayerArea);
    }

    [Command]
    public void CmdGameManagerChangeButonState(string ButtonState)
    {
        RpcGameManagerChangeButonState(ButtonState);
    }

    [ClientRpc]
    public void RpcGameManagerChangeButonState(string ButtonState)
    {
        GameManager.ChangeStateButton(ButtonState);
        if (ButtonState == "Compiling results of the round")
        {
            GameManager.ChangeTimesButtonHasBeenPressed();
        }
    }

    [Command]
    void CmdGameManagerCardPlayed()
    {
        RpcGameManagerCardPlayed();
    }

    [ClientRpc]
    void RpcGameManagerCardPlayed()
    {
        GameManager.CardsPlayed();
    }

    [Command]
    public void CmdRemove_Cards_From_Game()
    {
        RpcRemove_Cards_From_Game();
    }

    //removes gameobejct from scene
    [ClientRpc]
    public void RpcRemove_Cards_From_Game()
    {
        for (int i = 0; i < PlayerNormalCardAsignmentArea.Count; i++)
        {
            Destroy(PlayerNormalCardAsignmentArea[i].transform.GetChild(0).gameObject);
            Destroy(EnemyNormalCardAsignmentArea[i].transform.GetChild(0).gameObject);
        }
    }

    //this is used to generate a random number then passes this is in as a parmater which is used ot
    //decide whihc stat will be chosen for the round taht will be compared
    //the random int is generated on server side so that the card stat being chosen for that round will be same on both clients
    [Command]
    public void CmdSettingCardStatForRound()
    {
        int CardStatRandomGenerator = Random.Range(0, 4);
        RpcSettingCardStatForRound(CardStatRandomGenerator);
    }
    //this is called through the serer vommand to be able to change the current stat beign compared for the round and will replicate this on both clients
    [ClientRpc]
    void RpcSettingCardStatForRound(int RandomCardGenerated)
    {
        if(RandomCardGenerated == 0)
        {
            GameManager.DecidingCardBeinComparedForThisRound("MassStat");
        }
        if (RandomCardGenerated == 1)
        {
            GameManager.DecidingCardBeinComparedForThisRound("SpeedStat");
        }
        if (RandomCardGenerated == 2)
        {
            GameManager.DecidingCardBeinComparedForThisRound("StrengthStat");
        }
        if (RandomCardGenerated == 3)
        {
            GameManager.DecidingCardBeinComparedForThisRound("SizeStat");
        }
    }

    [Command]
    public void CmdComparingCardsInSlotAreas()
    {
        RpcComparingCardsStats();
    }

    [ClientRpc]
    void RpcComparingCardsStats()
    {
        GameManager.ComparingCardStatsPhase(GameManager.StatBeingCompared);
    }
    //instead waht i wikl now do is get th sytem to be able to spawn a legendary card every 3 rounds
    
}

// wjat we can do with the slot areas in my game is it will check if the slot areas contain a child in them if it does then it will not allow a collision to occure
//changes that i will make to the spawn cards function
/*
if (PlayerArea.transform.childCount == 0)
{
    for (int i = 0; i < 3; i++)
    {
        //creates an instance of the card object at the origin and wwe say at eh quaternion.idenitty that it has no rotation
        GameObject card = Instantiate(NormalCardsList2[Random.Range(0, NormalCardsList2.Count)], new Vector2(0, 0), Quaternion.identity);
        //connectiontoclient detemrines that the authority of the card instantiated will be of client level in terms of the accesslevel
        //this means that it will assign authority to the player it is connected to
        NetworkServer.Spawn(card, connectionToClient);
        RpcShowCard(card, "Dealt");

    }
}
if (PlayerArea.transform.childCount != 0)
{
    //creates an instance of the card object at the origin and wwe say at eh quaternion.idenitty that it has no rotation
    GameObject card = Instantiate(NormalCardsList2[Random.Range(0, NormalCardsList2.Count)], new Vector2(0, 0), Quaternion.identity);
    //connectiontoclient detemrines that the authority of the card instantiated will be of client level in terms of the accesslevel
    //this means that it will assign authority to the player it is connected to
    NetworkServer.Spawn(card, connectionToClient);
    RpcShowCard(card, "Dealt");
}
*/
/*
 *  [Command]
    public void CmdSettingCardStatForRound()
    {
        GameObject CardStatSelected = Instantiate(CardStatSelectorList[Random.Range(0, CardStatSelectorList.Count)], new Vector2(0, 0), Quaternion.identity);
        NetworkServer.Spawn(CardStatSelected, connectionToClient);
        RpcSettingCardStatForRound(CardStatSelected);
        Destroy(CardStatSelected);
    }

    [ClientRpc]
    void RpcSettingCardStatForRound(GameObject SelecteCardStat)
    {
        SelecteCardStat.transform.SetParent(Canvas.transform, false);

        if (SelecteCardStat == StatAccesorMassStat)
        {
            GameManager.DecidingCardBeinComparedForThisRound("MassStat");
        }
        if (SelecteCardStat == StatAccesorSpeedStat)
        {
            GameManager.DecidingCardBeinComparedForThisRound("SpeedStat");
        }
        if (SelecteCardStat == StatAccesorStrengthStat)
        {
            GameManager.DecidingCardBeinComparedForThisRound("StrengthStat");
        }
        if (SelecteCardStat == StatAccesorSizeStat)
        {
            GameManager.DecidingCardBeinComparedForThisRound("SizeStat");
        }
    }
*/
