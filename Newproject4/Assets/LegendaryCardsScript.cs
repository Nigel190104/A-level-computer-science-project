using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class LegendaryCardsScript : MonoBehaviour
{
    public int StrengthStat;
    public int SpeedStat;
    public float SizeStat;
    public int MassStat;
    public string CardName;
    public Sprite CardImage;

    //values are passed into this constructor to be able to create objects of this class in the spawncards awake function
    public LegendaryCardsScript(int givenstrength, float givenSize, int  givenSpeed, int givenMass, string givenCardName, Sprite givenImage)
    {
        StrengthStat = 2 * givenstrength;
        SpeedStat = 2 * givenSpeed;
        MassStat = 2 * givenMass;
        SizeStat = 2 * givenSize;
        CardName = givenCardName;

        CardImage = givenImage;
    }
}
