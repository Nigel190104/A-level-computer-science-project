using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;

namespace Assets.Code.States
{
    public class FourthInStructionState : IStateBase
    {
        private StateManager manager;

        public FourthInStructionState(StateManager managerRef)//constructor
        {
            manager = managerRef;
            if (SceneManager.GetActiveScene().name != "Instrunctios Scene")
            {
                SceneManager.LoadScene("Instrunctios Scene");
            }

        }

        public void StateUpdate()
        {
        }

        public void ShowIt()

        {
            /*
            Debug.Log("In BeginState");
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), manager.gameDataRef.beginStateSplash, ScaleMode.StretchToFill);
            if(GUI.Button(new Rect(0, 0, 300, 100), "Press Here or Any Key to Continue") || Input.anyKeyDown)
            {
                manager.SwitchState(new SetupState(manager));
            }
            */
        }

    }
}