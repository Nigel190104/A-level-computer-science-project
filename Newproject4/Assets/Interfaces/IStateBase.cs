namespace Assets.Code.Interfaces
{
    public interface IStateBase 
    {
        void StateUpdate();
        void ShowIt();
    }
}

//this script is used as the basis of all the state scripts it is an interface this
//essentially is used ot make sure that all states contian these functions as they will derive from this interface component

//thsi is done as the statemanage will always be accesing the methos in this script so if any of
//the states dont inlcude them then errors will be generating impeding gameplay