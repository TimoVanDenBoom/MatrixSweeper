using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideByScript : MonoBehaviour
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
        transform.parent.transform.parent.transform.GetComponent<RowScript>().Division(int.Parse(transform.GetComponentInChildren<Text>().text));
    }

    private void OnMouseUp()
    {
        GameObject[] divideByObjects = GameObject.FindGameObjectsWithTag("DivideBy");

        for (int i = 0; i < divideByObjects.Length; i++)
        {
            Destroy(divideByObjects[i]);
        }
    }
}
