using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class GameManager : NetworkBehaviour
{
    //this used to chang the text displays in the game
    public UIManager UIManager;
    public Playermanager PlayerManager;
    public string StatBeingCompared = "";
   //gameturnorder decides in that roudn what the turn is wiht a max value of two which 
   //signifies that both player have had their turn
    public int GameTurnOrder = 0;
    //the four variables below are used for determining the current round
    //as well as storing how many rounds each player has won
    public int Rounds = 0;
    public int EnemyRoundsWon = 0;
    public int PlayerRoundsWon = 0;
    public string ButtonState = "Can Draw Cards";
    [HideInInspector]
    public int HowManyTimesHavePlayerPressedButton = 0;
    public SpawnCards Button;
    public bool StoringPlayerHasWonRoundVariable;
    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        //this passes in the value buttonstate which corrsponds ot the state that the button is in and changes the 
        //the text value in the draw cards button Ui based on this value
        UIManager.UpdateRoundsText(Rounds, EnemyRoundsWon, PlayerRoundsWon);
        UIManager.UpdateButtonText(ButtonState);
        Button = GameObject.Find("Draw Cards Button").GetComponent<SpawnCards>();
        //UIManager.UpdateStatBeingComparedText(StatBeingCompared);
    }

    private void Update()
    {
        if(Rounds== 10)
        {
            Application.Quit();
        }
    }

    //this method is used for changing the state of the button 
    public void ChangeStateButton(string StateChange)
    {
        //in this state the player can press the card and it will generate card
        if(StateChange == "Can Draw Cards")
        {
            HowManyTimesHavePlayerPressedButton = 0;
            ButtonState = "Can Draw Cards";
            if (Rounds == 0)
            {
                RoundsPlayed();  
            }
        }
        //in this state the player can press the card but nothing will happen
        else if (StateChange == "Compiling results of the round")
        {
            if (HowManyTimesHavePlayerPressedButton == 1)
            {
                ButtonState = "Compiling results of the round";
            }
        }
        //in this state once the butto is pressed card are deleted from their respective dropzones or slots
        //in the game
        else if (StateChange == "Remove Cards From Game")
        {
            ButtonState = "Remove Cards From Game";
            GameTurnOrder = 0;
            RoundsPlayed();
        }
        UIManager.UpdateRoundsText(Rounds, EnemyRoundsWon, PlayerRoundsWon);
        UIManager.UpdateButtonText(ButtonState);
    }

    //this is called to increment hoe many times the button has been pressed by the player on that conection id
    public void ChangeTimesButtonHasBeenPressed()
    {
        HowManyTimesHavePlayerPressedButton++;
    }

    //this is called to incremtn the gameturnorder by one each time a card has been played
    //by the player as well as check for when players have played their cards to then later in design will
    // perform a range of checks to decide who wins that round then deletes it
    public void CardsPlayed()
    {
        GameTurnOrder++;
        if (GameTurnOrder == 2)
        {
            ChangeStateButton("Remove Cards From Game");
        }
    }
    //a method with the sole purpose of incrementing the rounds of the game
    public void RoundsPlayed()
    {
        Rounds++;
    }
    //this method is called to be able to change the stat being changed at the start of every round
    public void DecidingCardBeinComparedForThisRound(string StatBeingPassedInForRound)
    {

        if (StatBeingPassedInForRound == "MassStat")
        {
            StatBeingCompared = "MassStat";
        }
        
        if (StatBeingPassedInForRound == "StrengthStat")
        {
            StatBeingCompared = "StrengthStat";
        }
        
        if (StatBeingPassedInForRound == "SizeStat")
        {
            StatBeingCompared = "SizeStat";
        }
        
        if (StatBeingPassedInForRound == "SpeedStat")
        {
            StatBeingCompared = "SpeedStat";
        }
        
        UIManager.UpdateStatBeingComparedText(StatBeingCompared);
    }
    
    //this scritp has been added which will allow for incrementing the amount of rounds won by the player should they win in the card match up phase in the game
    public void ComparingCardStatsPhase(string StatForThisRound)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<Playermanager>();
        //the if statements below check which stat is being compared and if that if statement is true the parmater
        //that is passed in does equal the string value or card stat on the right then 
        //it will check the cards located in each of the areas and compare their stats and depending on which
        //one is bigger will increment the players who has the larger value by a value of 1
        if (StatForThisRound == "SpeedStat")
        {
                if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SpeedStat
               >= PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SpeedStat)
                {
                    PlayerRoundsWon += 1;
                    EnemyRoundsWon += 0;
                }
                else
                {

                    EnemyRoundsWon += 1;
                    PlayerRoundsWon += 0;
                }
            
        }

        if (StatForThisRound == "SizeStat")
        {
           
                if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SizeStat
               >= PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SizeStat)
                {
                    PlayerRoundsWon += 1;
                    EnemyRoundsWon += 0;
                }
                else
                {
                    EnemyRoundsWon += 1;
                    PlayerRoundsWon += 0;
                }
            
        }
        if (StatForThisRound == "StrengthStat")
        {
            
                if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().StrengthStat
               >= PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().StrengthStat)
                {
                    PlayerRoundsWon += 1;
                    EnemyRoundsWon += 0;
                   
                }
                else
                {
                    EnemyRoundsWon += 1;
                    PlayerRoundsWon += 0;
                }
            
        }
        if (StatForThisRound == "MassStat")
        {
            
                if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().MassStat
               >= PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().MassStat)
                {
                    PlayerRoundsWon += 1;
                    EnemyRoundsWon += 0;
                    
                }
                else
                {
                    EnemyRoundsWon += 1;
                    PlayerRoundsWon += 0;
                }
            

        }
    }
    
}
//once i get to this part will be after i have fully developed the roudns system of the gaem and then
//will change wjat the ai displays due ot the fact that 
//the values dont change yet in the game
//PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SpeedStat;
/*
if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SpeedStat
    > PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SpeedStat)
{
    GameManager.PlayerRoundsWon += 1;
}
else
{
    GameManager.EnemyRoundsWon += 0;
}
*/
//PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SizeStat;
/*
if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SizeStat
    > PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().SizeStat)
{
    GameManager.PlayerRoundsWon += 1;
}
else
{
    GameManager.EnemyRoundsWon += 0;
}
*/
//PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().StrengthStat;
/*
if (PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().StrengthStat
    > PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().StrengthStat)
{
    GameManager.PlayerRoundsWon += 1;
}
else
{
    GameManager.EnemyRoundsWon += 0;
}
*/
//PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().MassStat;
/*
if(PlayerManager.PlayerNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().MassStat
    > PlayerManager.EnemyNormalCardAsignmentArea[0].transform.GetChild(0).gameObject.GetComponent<PlayCards>().MassStat)
{
    GameManager.PlayerRoundsWon += 1;
}
else
{
    GameManager.EnemyRoundsWon += 0;
}
*/
