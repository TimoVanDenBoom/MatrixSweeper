using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountOfMovesScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;


    // Update is called once per frame
    void Update()
    {
        text.text = "Moves: " + GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().GetNumberOfActions();
    }
}
