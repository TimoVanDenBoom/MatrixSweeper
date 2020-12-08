using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsVictoryText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    // Update is called once per frame
    void Update()
    {
        text.text = "You used " + GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().GetNumberOfActions() + " actions to win.";
    }
}