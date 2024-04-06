using UnityEngine;
using Assets.Code.States;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    private IStateBase activeState;

    //this is done so that other states can access the card but making
    //sure that it cannot be changed or viewed in the
    [HideInInspector]
    public GameData GameDataRef;
    void Awake()
    {
    }

    // Start is called before the first frame update
    //when the game starts it changes the state of
    //the game to the first instruntcion state as well as assigning the
    // gameDatRef equal to the gamedata component this can be done as
    //the gamedata script is attached to the statemanager Object in the game
    void Start()
    {
        activeState = new FirstInstructionState(this);
        GameDataRef = GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        //this checks that if activestate has a value assigned
        //to it then it will  perform what is located
        //in the stateUpdate Function in that state
        if(activeState != null)
        {
            activeState.StateUpdate();
        }
    }
    //this controls everythign to do with GUi elements and is accessed
    //by the differetn states in the game to allow for different actions or processes
    // to occure when a gui button is interacted with in the game
    void OnGUI()
    {
        if(activeState != null)
        {
            activeState.ShowIt();
        }
    }
    //this is used to change the state in the game so that
    //only one state can be in control of the game game at any time
    //and once certain actions are performed in the varoious states the state is changed
    //this is called from within other states
    public void SwitchState(IStateBase newState)
    {
        activeState = newState;
    }
}
