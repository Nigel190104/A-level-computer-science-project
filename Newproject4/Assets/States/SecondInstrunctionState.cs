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
    public class SecondInStructionState: IStateBase
    {
        //creatin a variabel of statmnager type which is used to acces the statmanager script
        private StateManager manager;
        
        //this is used ot change the backgorund depenign on what state the game is in
        public GameObject BackGround;

        //when a state is changed then it will pass in the manager variable to give control of the
        //game over to this state by making the mnager variable to be of the new state
        public SecondInStructionState(StateManager managerRef)//constructor
        {
            manager = managerRef;
            //this will check if the scene this state is controlling is currently loaded if not then it will load it
            if (SceneManager.GetActiveScene().name != "Instrunctios Scene")
            {
                SceneManager.LoadScene("Instrunctios Scene");
            }

        }

        public void StateUpdate()
        {
            //this funds the gameobject background and then set it to the picture assigned to
            //the variable in the gamdata component but this variabel si
            //accessed through the state manager which has alreasy got a reference to it rather than
            //creatign a regerence to the gamedtat component in eveyr state component in the game
            BackGround = GameObject.Find("BackGroundImage");
            BackGround.GetComponent<RawImage>().texture = manager.GameDataRef.PictureAccessor4;
        }

        public void ShowIt()
        {
           //this creates a gui button which when presseed will take the player back to the previous instuncion page by chagning the state back to the first state
           //the first digit in the new Rect paranthesis is the x co-ordinate of the button on the screen, the second is the y co-roridnate on the screen when
           //it is displayed as well as this the messgaein quotation marks is the text that the button will show inside it
           if (GUI.Button(new Rect(0, 0, 200, 60), "Press Here To Move \n To previous InstructionScreen"))
           {
                manager.SwitchState(new FirstInstructionState(manager));
           }

           //this drawtexture gui fucntion will create or draw an image on the screen at the y and x c-rodinate specified in the game, the scalemode stretchtofill
           //normally will fill the wholescreen but since i have specified the heiht and width i want the image to be it doesnt matter
           //the manager,gamedataref.gamescenexplantion two represents the image that will be shown in the game instrucntion scene
            GUI.DrawTexture(new Rect(0, (Screen.height / 2) - 100, 380, 200), manager.GameDataRef.GameSceneExplanations2, ScaleMode.StretchToFill);
            //this just draws a box with text inside it
            GUI.Box(new Rect((Screen.width / 2) - 300, 0, 350, 80), "\nInstruction Scene 2");
            
            GUI.Box(new Rect((Screen.width / 2) - 300, (Screen.height / 2) - 100, 350, 100), "\n2. The draw cards button is used to be able spawn card \nin each of the player areas you must only " +
                "\nclick this once every turn or else you will have to \nrestart the game as the gamestate wont change");
            
            GUI.Box(new Rect((Screen.width / 2) - 300, (Screen.height / 2) + 50, 350, 120), "4.The 8 areas in the middel of the screen represent \nwhere you can put cards only the ones wit the\n" +
                " purple outline can be used by either players\n you can only use those two areas for now until bug fixes\n" +
                "are made which will allow for the legendary card slots and special effect slots to work");
            GUI.Box(new Rect(0, (Screen.height / 2) + 100, 350, 100), "3. once you have both drawn cards by pressing \nthe button then the text in the button it will change ot the \n" +
                "compiling cards phasein which you cant draw anymore\n cards you can drag you cards in your areas\n to your respective card slots in the game");
            
            GUI.Box(new Rect(0, 100, 400, 70), "1.Once you have both dragged cards to the middle\n of the screen then the text in the draw cards button " +
                "\nwill change from the compiling results cards phase to \nremove card from game");
            
            GUI.DrawTexture(new Rect(Screen.width-400, (Screen.height / 2) - 100, 400, 200), manager.GameDataRef.GameSceneExplanations4, ScaleMode.StretchToFill);
            GUI.Box(new Rect(Screen.width - 400, (Screen.height / 2) + 100, 400, 100), "In the top Corner it show the text displaying \nthe cards stats for that round you will have to select the \nbest card you " +
                "have to win that round if you are able to if not then \nyou will have to strategically plan the rounds in \nwhcich you play specific cards in the game");

            //when the gui button is pressed it will change the state as well as what will be displayed to the player
            if (GUI.Button(new Rect(Screen.width - 500, 0, 500, 60), "Press Here To Move To Next InstructionScreen"))
            {
                manager.SwitchState(new ThirdInStructionState(manager));
            }
        }

    }
}

