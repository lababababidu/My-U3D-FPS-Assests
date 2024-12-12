using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Public variables for score, ammo, and current bullet count
    public int score;
    public int ammo;
    public int currentAmmo;


    void OnGUI()
    {
        // Define the style for the labels
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // Display the score
        GUI.Label(new Rect(10, 10, 200, 30), "Score: " + score.ToString(), style);

        // Display the ammo
        GUI.Label(new Rect(10, 50, 200, 30), "Ammo: " + ammo.ToString(), style);

        // Display the current bullet
        GUI.Label(new Rect(10, 90, 200, 30), "Current Ammo: " + currentAmmo.ToString(), style);
    }

    void Update(){
        score = GameObject.Find("Score Controller").GetComponent<ScoreCounter>().score;
        ammo = GameObject.Find("Crossbow(Clone)").GetComponent<WeaponController>().ammunition;
        currentAmmo = GameObject.Find("Crossbow(Clone)").GetComponent<WeaponController>().ammo;
    }
}