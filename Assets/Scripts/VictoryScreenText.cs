using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = "Matrix was made with " + GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().GetPossibleNumberOfActionsToWin() + " random actions. " +
            "\n You used " + GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().GetNumberOfActions() + " actions.";
    }
}
