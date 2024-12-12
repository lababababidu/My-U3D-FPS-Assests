using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int StaticTargetScore = 10;
    public int DynamicTargetScore = 30;
    public int score = 0;
    

    public bool HitStaticTarget(){
        score += StaticTargetScore;
        return true;
    }
    public bool HitDynamicTarget(){
        score += DynamicTargetScore;
        return true;
    }
}
