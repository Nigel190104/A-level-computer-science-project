using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class UIManager : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Button;
    public GameObject RoundsText;
    public GameObject EnemyRoundsWonText;
    public GameObject PlayerRoundsWonText;
    public GameObject StatBeingComparedText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void UpdateRoundsText(int rounds, int EnemyRoundsWon, int PlayerRoundsWon)
    {
        RoundsText.GetComponent<Text>().text = "Rounds: " + rounds;
        EnemyRoundsWonText.GetComponent<Text>().text = "Enemy Rounds Won: " + EnemyRoundsWon;
        PlayerRoundsWonText.GetComponent<Text>().text = "Player Rounds Won: " + PlayerRoundsWon;
    }

    public void UpdateStatBeingComparedText(string ChooseStateComparison)
    {
        StatBeingComparedText.GetComponent<Text>().text = "Stat Being Compared: " + ChooseStateComparison;
    }
    public void UpdateButtonText(string gameState)
    {
        Button = GameObject.Find("Draw Cards Button");
        Button.GetComponentInChildren<Text>().text = gameState;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
/*
 public void UpdateRoundsText(int rounds, int enemyRoundsWon, int playerRoundsWon)
    {
        RoundsText.GetComponent<Text>().text = "Rounds: " + rounds;
        EnemyRoundsWonText.GetComponent<Text>().text = "Enemy Rounds Won: " + enemyRoundsWon;
        PlayerRoundsWonText.GetComponent<Text>().text = "Player Rounds Won: " + playerRoundsWon;
    }
 */