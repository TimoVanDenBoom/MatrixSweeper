using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryScript : MonoBehaviour
{
    private int [,] matrix;
    private List<GameObject> rows;
    // Start is called before the first frame update
    void Start()
    {
        matrix = GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().getInitialState();
        rows = GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().getRows();
    }

    
    public void ReturnToInitialState()
    {
        for(int i=0; i < rows.Count; i++)
        {
            List<int> list = new List<int>();

            for(int j = 0; j < rows.Count; j++)
            {
                list.Add(matrix[i, j]);
            }
            rows[i].GetComponent<RowScript>().setNumbers(list);
            rows[i].transform.localPosition = rows[i].GetComponent<RowScript>().GetInitialPosition();
            rows[i].GetComponent<RowScript>().SetOriginalPos(rows[i].GetComponent<RowScript>().GetInitialPosition());
        }
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().SetNumberOfActions(0);
    }
}
