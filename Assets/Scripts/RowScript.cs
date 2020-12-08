using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RowScript : MonoBehaviour
{
    private List<int> numbers = new List<int>();
    private List<GameObject> numbersObject = new List<GameObject>();
    public GameObject number;
    int rowSize;
    private float startPosX;
    private float startPosY;
    private bool isDragged = false;
    private Vector3 originalPos;
    private Vector3 originalScale;
    private Vector3 initialPosition;
    private SoundEffectsScript soundFX;
    private RowScript rowScript;
    private Transform multiplyPanel;
    private Transform victoryPanel;
    private Transform menu;
    private Transform options;
    private GameObject canvas;
    // Start is called before the first frame update
    private void Start()
    {

        rowSize = transform.parent.transform.GetComponent<MatrixScript>().matrixSize;
        for (int i = 0; i < rowSize; i++){
            numbersObject.Add(Instantiate(number));
            numbersObject[i].transform.position = new Vector3((transform.position.x-((rowSize-1)/2f)) + i, transform.position.y);
            numbersObject[i].transform.parent = transform;
        }
        foreach(Transform child in transform)
        {
            if(child.tag == "Plus")
            {
                child.transform.position = new Vector3((transform.position.x - ((rowSize - 1) / 2f)) + rowSize + 1, transform.position.y);
            }
            else if(child.tag == "Minus")
            {
                child.transform.position = new Vector3((transform.position.x - ((rowSize - 1) / 2f)) - 2, transform.position.y);
            }
            else if(child.tag == "Multiply")
            { 
                child.transform.position = new Vector3((transform.position.x - ((rowSize - 1) / 2f)) + rowSize + 2, transform.position.y);
            }
            else if(child.tag == "Divide")
            {
                child.transform.position = new Vector3((transform.position.x - ((rowSize - 1) / 2f)) - 3, transform.position.y);
            }
        }
        transform.GetComponent<BoxCollider2D>().size = new Vector2(rowSize, 1);
        originalPos = transform.localPosition;
        initialPosition = transform.localPosition;
        originalScale = transform.localScale;
        soundFX = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();
        rowScript = transform.GetComponent<RowScript>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        multiplyPanel = canvas.transform.GetChild(0);
        victoryPanel = canvas.transform.GetChild(1);
        menu = canvas.transform.GetChild(2);
        options = canvas.transform.GetChild(3);
    }
    public List<int> getNumbers()
    {
        return numbers;
    }

    public void setNumbers(List<int> nums)
    {
        numbers = nums;
    }

    public bool Addition(List<int> nums)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if(numbers[i] + nums[i] > 99 || numbers[i] + nums[i] < -99)
            {
                return false;
            }
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().IncreaseNumberOfActions();
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i] = numbers[i] + nums[i];
        }
        return true;
    }

    public bool Subtraction(List<int> nums)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] - nums[i] > 99 || numbers[i] - nums[i] < -99)
            {
                return false;
            }
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().IncreaseNumberOfActions();
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i] = numbers[i] - nums[i];
        }
        return true;
    }

    public bool Multiplication(int factor)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] * factor > 99 || numbers[i] * factor < -99)
            {
                return false;
            }
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().IncreaseNumberOfActions();
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i] = numbers[i] * factor;
        }
        return true;
    }

    public bool Division(int denominator)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] / denominator > 99 || numbers[i] / denominator < -99)
            {
                return false;
            }
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().IncreaseNumberOfActions();
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i] = numbers[i] / denominator;
        }
        return true;
    }

    public void Swap(GameObject obj)
    {
        Vector3 positionRow1 = transform.localPosition;
        transform.localPosition = obj.transform.localPosition;
        originalPos = obj.transform.localPosition;
        obj.transform.localPosition = positionRow1;
        obj.GetComponent<RowScript>().SetOriginalPos(positionRow1);
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().IncreaseNumberOfActions();
    }

    private void Update()
    {
        for(int i = 0; i < rowSize; i++)
        {
            numbersObject[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(numbers[i].ToString());
        }
        if (isDragged)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            soundFX.playMouseDownRowClip();
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            startPosX = mousePos.x - transform.position.x;
            startPosY = mousePos.y - transform.position.y;
            this.isDragged = true;
        }
        foreach(Transform child in transform)
        {
            if(child.tag == "Number" && rowScript.isDragged)
            {
                child.localScale = new Vector3(1f, 1f);
                child.GetComponent<SpriteRenderer>().sortingOrder = 7;
            }
        }
    }
    private void OnMouseUp()
    {
        if (isDragged)
        {
            soundFX.playMouseUpRowClip();
        }
        isDragged = false;
        foreach (Transform child in transform)
        {
            if (child.tag == "Number")
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
        foreach(Transform child in transform.parent.transform)
        {
            if((child.localPosition - transform.localPosition).magnitude < (0.5 * transform.parent.localScale.x) && child != transform)
            {
                transform.localPosition = originalPos;
                Swap(child.gameObject);
            }
        }
        transform.localPosition = originalPos;
    }

    private void OnMouseEnter()
    {
        if (transform.localScale.magnitude < originalScale.magnitude + 0.05f && !Input.GetMouseButton(0) && !multiplyPanel.gameObject.activeSelf
            && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            soundFX.playMouseOverRowClip();
            transform.localScale += new Vector3(.05f, .05f);
        }
        foreach (Transform child in transform)
        {
            if (child.tag == "Number" && !isDragged)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = 6;
            }
        }
    }
    private void OnMouseExit()
    {
        if (!isDragged)
        {
            transform.localScale = originalScale;
        }
        foreach (Transform child in transform)
        {
            if (child.tag == "Number" && !isDragged)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
    }


    public void SetOriginalPos(Vector3 pos)
    {
        originalPos = pos;
    }
    public Vector3 GetOriginalPos()
    {
        return originalPos;
    }
    public void SetIsDragged(bool drag)
    {
        isDragged = drag;
    }
    public bool GetIsDragged()
    {
        return isDragged;
    }
    public void SetStartPosX(float x)
    {
        startPosX = x;
    }
    public void SetStartPosY(float y)
    {
        startPosY = y;
    }

    public Vector3 GetInitialPosition()
    {
        return initialPosition;
    }

    
}
