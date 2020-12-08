using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplyByScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseOver()
    {
        spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
    }
    private void OnMouseExit()
    {
        spriteRenderer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        transform.parent.transform.parent.transform.GetComponent<RowScript>().Multiplication(int.Parse(transform.GetComponentInChildren<Text>().text));
    }

    private void OnMouseUp()
    {
        GameObject[] multiplyByObjects = GameObject.FindGameObjectsWithTag("MultiplyBy");

        for (int i = 0; i < multiplyByObjects.Length; i++)
        {
            Destroy(multiplyByObjects[i]);
        }
    }

}
