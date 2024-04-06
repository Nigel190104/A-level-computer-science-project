using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class CardStatsController : NetworkBehaviour
{
    public Playermanager PlayerManager;
    public GameManager GameManager;
    public UIManager UIManager;

      
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //a function used for the compparing of the cards in the game
    public void DecidingCardBeinComparedForThisRound(string StatBeingPassedInForRound)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<Playermanager>();

        if (StatBeingPassedInForRound == "MassStat")
        {
            GameManager.StatBeingCompared = "MassStat";
        }
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

  
        if (StatBeingPassedInForRound == "StrengthStat")
        {
            GameManager.StatBeingCompared = "StrengthStat";
        }
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

        if (StatBeingPassedInForRound == "SizeStat")
        {
            GameManager.StatBeingCompared = "SizeStat";
        }
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


        if (StatBeingPassedInForRound == "SpeedStat")
        {
            GameManager.StatBeingCompared = "SpeedStat";
        }
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
    }
}
//what we coould do is append the stat beign compared text ui that round then it is changed at the start of the round based on what that players whos turn it si to selects stats chooses out of the 4 options