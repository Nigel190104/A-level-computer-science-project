using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class NormalCardsScript : MonoBehaviour
{
    public int StrengthStat;
    public int SpeedStat;
    public  float SizeStat;
    public int MassStat;
    public string CardName;
    public Sprite CardImage;

    //values are passed into this constructor to be able to create objects of this class in the spawncards awake function
    public NormalCardsScript(int givenstrength, float givenSize, int  givenSpeed, int givenMass, string givenCardName, Sprite givenImage)
    {
        StrengthStat = givenstrength;
        SpeedStat = givenSpeed;
        MassStat = givenMass;
        SizeStat = givenSize;
        CardName = givenCardName;

        CardImage = givenImage;
    }
}
