using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is just showing where this state component is located
namespace Assets.Code.States
{
    //shows inheritance from the iStatBase Interface
    public class FirstInstructionState : IStateBase
    {
        private StateManager manager;
        public GameObject BackGround;

        public FirstInstructionState(StateManager managerRef)//constructor
        {
            manager = managerRef;
            if (SceneManager.GetActiveScene().name != "Instrunctios Scene")
            {
                SceneManager.LoadScene("Instrunctios Scene");
            }

        }

        public void StateUpdate()
        {
            BackGround = GameObject.Find("BackGroundImage");
            BackGround.GetComponent<RawImage>().texture = manager.GameDataRef.PictureAccessor1;
        }

        public void ShowIt()
        {
            GUI.DrawTexture(new Rect(0, (Screen.height / 2)-100, 350, 150), manager.GameDataRef.GameSceneExplanations1, ScaleMode.StretchToFill);
            GUI.Box(new Rect((Screen.width / 2)-300, 0, 350, 80), "\nInstruction Scene 1");
            
            GUI.Box(new Rect((Screen.width / 2) - 300, (Screen.height / 2) - 100, 350, 80), "\n2. The Server and client button which is shown is for the " +
                "\n host between the two of you one of you will \n  have to decide who will be hosting the game");
            
            GUI.Box(new Rect((Screen.width / 2) - 300, (Screen.height / 2) + 50, 350, 80), "4.Once You have both pressed Your Respective Buttons\n You will be " +
                "Presented with the Game Interface \n Where the Actual GamePlay Begins \n this will be shown in the next instrunction scene");
            
            GUI.Box(new Rect(0, (Screen.height / 2) + 100, 300, 60), "3. By default the other player will \n  have to join by pressing the client button");
            
            GUI.Box(new Rect(0, 100, 400, 70), "1. The player who will act as the host will have to \n press the host(server + client) button before the player" +
                " \n who will act as the client if this does not happen then the \n  player who is the client will not be able to connect to the game");
            
            if (GUI.Button(new Rect(Screen.width-500, 0, 500, 60), "Press Here To Move To Next InstructionScreen"))
            {
                manager.SwitchState(new SecondInStructionState(manager));
            }
        }
    }

}

