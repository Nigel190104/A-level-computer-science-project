using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;

//this is just showing where this state component is located
namespace Assets.Code.States
{
    //shows inheritance from the iStatBase Interface
    public class ThirdInStructionState : IStateBase
    {
        private StateManager manager;

        public ThirdInStructionState(StateManager managerRef)//constructor
        {
            manager = managerRef;
            //this changes the state of the game to the actual gameplay in the game
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                SceneManager.LoadScene("GameScene");
            }

        }

        public void StateUpdate()
        {
        }

        public void ShowIt()

        {
            
        }

    }
}
