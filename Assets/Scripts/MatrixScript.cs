using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
public class MatrixScript : MonoBehaviour
{
    public bool isRandom;
    public int matrixSize;
    public List<MultiDimensionalIntList> matrix;
    public GameObject row;
    private List<GameObject> rows = new List<GameObject>();
    private bool winCondition;
    List<int> compare = new List<int>();
    private bool mouseOverDivide;
    private bool mouseOverMultiply;
    private int[,] initialState;
    private int numberOfActions;
    private int possibleNumberOfActionsToWin;
    private float originalHeight;
    private Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        numberOfActions = 0;
        transform.localScale = new Vector3(matrixSize / 2f, matrixSize / 2f);
        transform.position = new Vector3(matrixSize / 2f, matrixSize / 2f);
        initialState = new int[matrixSize, matrixSize];
        initialScale = transform.localScale;

        for (int i = 0; i < matrixSize; i++)
        {
            List<int> nums = new List<int>();
            rows.Add(Instantiate(row));
            rows[i].transform.position = new Vector3(matrixSize / 2f, i + 0.5f);
            rows[i].transform.parent = this.transform;
            for(int j = 0; j < matrixSize; j++)
            {
                nums.Add(0);
            }
            nums[matrixSize - i - 1] = 1;
            rows[i].GetComponent<RowScript>().setNumbers(nums);
        }
        if (isRandom)
        {
            Randomize();
        }
        else
        {
            for(int i = 0; i < matrixSize; i++)
            {
                rows[i].GetComponent<RowScript>().setNumbers(matrix[i].intList);
            }
        }
        possibleNumberOfActionsToWin = numberOfActions;
        numberOfActions = 0;
        for (int i = 0; i < matrixSize; i++)
        {
            for(int j = 0; j < matrixSize; j++)
            {
                initialState[i, j] = rows[i].GetComponent<RowScript>().getNumbers()[j];
            }
        }
        GameObject.FindWithTag("MainCamera").transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        for (int i = 0; i < matrixSize; i++)
        {
            compare.Add(0);
        }
        winCondition = false;
        mouseOverDivide = false;
        mouseOverMultiply = false;
        originalHeight = (float)Screen.height;
    }

    void Randomize()
    {
        int operationsAmount = Random.Range(5, 20) * matrixSize;
        for (int i = 0; i < operationsAmount; i++)
        {
            int operation = Random.Range(0, 4);
            int row1 = Random.Range(0, matrixSize);
            int row2 = Random.Range(0, matrixSize);
            while(row1 == row2)
            {
                row2 = Random.Range(0, matrixSize);
            }
            switch (operation)
            {
                case 0:
                    rows[row1].GetComponent<RowScript>().Addition(rows[row2].GetComponent<RowScript>().getNumbers());
                    break;
                case 1:
                    rows[row1].GetComponent<RowScript>().Subtraction(rows[row2].GetComponent<RowScript>().getNumbers());
                    break;
                case 2:
                    int factor = Random.Range(2, 8);
                    int sign = Random.Range(0, 2);
                    if(sign == 1)
                    {
                        factor *= -1;
                    }
                    rows[row1].GetComponent<RowScript>().Multiplication(factor);
                    break;
                case 3:
                    rows[row1].GetComponent<RowScript>().Swap(rows[row2]);
                    break;
            }
        }
    }

    void RandomizeByDeterminant()
    {
        double det = 0;
        while(System.Math.Abs(det) != 1)
        {
            int[,] matrix = new int[matrixSize,matrixSize];
            for (int j = 0; j < matrixSize; j++)
            {
                List<int> nums = new List<int>();
                for (int i = 0; i < matrixSize; i++)
                {
                    int toAdd = Random.Range(-99, 100);
                    nums.Add(toAdd);
                    matrix[j, i] = toAdd;
                }
                rows[j].GetComponent<RowScript>().setNumbers(nums);
            }
            det = determinantOfMatrix(matrix, matrixSize);
        }
    }

    int determinantOfMatrix(int[,]
                               mat, int n)
    {
        int D = 0; // Initialize result 

        // Base case : if matrix  
        // contains single element 
        if (n == 1)
            return mat[0, 0];

        // To store cofactors 
        int[,] temp = new int[matrixSize, matrixSize];

        int sign = 1; // To store sign multiplier 

        // Iterate for each  
        // element of first row 
        for (int f = 0; f < n; f++)
        {
            // Getting Cofactor of mat[0,f] 
            getCofactor(mat, temp, 0, f, n);
            D += sign * mat[0, f] *
                 determinantOfMatrix(temp, n - 1);

            // terms are to be added 
            // with alternate sign 
            sign = -sign;
        }
        return D;
    }

    void getCofactor(int[,] mat, int[,] temp,
                        int p, int q, int n)
    {
        int i = 0, j = 0;

        // Looping for each element  
        // of the matrix 
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                // Copying into temporary matrix  
                // only those element which are 
                // not in given row and column 
                if (row != p && col != q)
                {
                    temp[i, j++] = mat[row, col];

                    // Row is filled, so  
                    // increase row index and 
                    // reset col index 
                    if (j == n - 1)
                    {
                        j = 0;
                        i++;
                    }
                }
            }
        }
    }

    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButtonDown(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                null))
        {
            panel.SetActive(false);
            foreach (Transform child in transform)
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.tag != "Number" && grandchild.GetComponent<SpriteRenderer>().material.color != Color.white)
                    {
                        grandchild.GetComponent<SpriteRenderer>().material.color = Color.white;
                    }
                }
            }
        }
    }
    private void Update()
    {
        float currentHeight = Screen.height;
        transform.localScale = new Vector3((float)initialScale.x * (float)(currentHeight / originalHeight), (float)initialScale.y * (float)(currentHeight / originalHeight));
        winCondition = true;
        foreach(GameObject row in rows)
        {
            for (int i = 0; i < matrixSize; i++)
            {
               // Debug.Log(row.transform.localPosition.y);
                compare[matrixSize - i - 1] = 1;
                float number = -((float)matrixSize - 1) / (float)matrixSize + (i * 2 / (float)matrixSize);
                if(row.transform.localPosition.y == number || row.transform.GetComponent<RowScript>().GetIsDragged())
                {
                    if(!row.transform.GetComponent<RowScript>().getNumbers().SequenceEqual(compare))
                    {
                        winCondition = false;
                    }
                }
                compare[matrixSize - i - 1] = 0;
            }
        }
        if (winCondition)
        {
            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        }

        if (!mouseOverDivide && Input.GetMouseButtonDown(0))
        {
            GameObject[] divideByObjects = GameObject.FindGameObjectsWithTag("DivideBy");

            for (int i = 0; i < divideByObjects.Length; i++)
            {
                Destroy(divideByObjects[i]);
            }
        }

        if (!mouseOverMultiply && Input.GetMouseButtonDown(0))
        {
            GameObject[] multiplyByObjects = GameObject.FindGameObjectsWithTag("MultiplyBy");

            for (int i = 0; i < multiplyByObjects.Length; i++)
            {
                Destroy(multiplyByObjects[i]);
            }
        }
        if (!FadeScript.isTransitioning) {
            if (Input.GetKeyDown("escape") && !GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject.activeSelf)
            {
                if (!GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.activeSelf &&
                    !GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).gameObject.activeSelf)
                {
                    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).gameObject.activeSelf)
                {
                    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).GetComponent<FadeScript>().FadeOut();
                    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).GetComponent<FadeScript>().FadeIn();
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }

    public void setMouseOverDivide(bool set)
    {
        mouseOverDivide = set;
    }

    public void setMouseOverMultiply(bool set)
    {
        mouseOverMultiply = set;
    }

    public int[,] getInitialState()
    {
        return initialState;
    }
    public List<GameObject> getRows()
    {
        return rows;
    }

    public void IncreaseNumberOfActions()
    {
        numberOfActions++;
    }

    public void SetNumberOfActions(int number)
    {
        numberOfActions = number;
    }

    public int GetNumberOfActions()
    {
        return numberOfActions;
    }

    public int GetPossibleNumberOfActionsToWin()
    {
        return possibleNumberOfActionsToWin;
    }

}
