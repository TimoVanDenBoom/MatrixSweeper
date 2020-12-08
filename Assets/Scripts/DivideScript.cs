using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;

public class DivideScript : MonoBehaviour
{
    private SoundEffectsScript soundFX;
    private Transform multiplyPanel;
    private Transform victoryPanel;
    private Transform menu;
    private Transform options;
    private GameObject canvas;
    private SpriteRenderer spriteRenderer;
    public GameObject divideBy;

    private void Start()
    {
        soundFX = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        multiplyPanel = canvas.transform.GetChild(0);
        victoryPanel = canvas.transform.GetChild(1);
        menu = canvas.transform.GetChild(2);
        options = canvas.transform.GetChild(3);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseUp()
    {
        GameObject[] divideByObjects = GameObject.FindGameObjectsWithTag("DivideBy");

        for (int i = 0; i < divideByObjects.Length; i++)
        {
            Destroy(divideByObjects[i]);
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().setMouseOverDivide(true);
        if(!victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
            spriteRenderer.material.color = Color.grey;

        if (!victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            List<int> toBeDivided = new List<int>();
            List<int> divisors = new List<int>();
            divisors.Add(-1);
            bool add = true;
            foreach (int i in transform.parent.transform.GetComponent<RowScript>().getNumbers())
            {
                toBeDivided.Add(i);
            }
            for (int j = 2; j <= toBeDivided.Select(x => Math.Abs(x)).Max(); j++)
            {
                foreach (int i in toBeDivided)
                {
                    if (i % j != 0)
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    divisors.Add(j);
                }
                add = true;
            }

            for (int i = 0; i < divisors.Count; i++)
            {
                if(divisors.Count < 8)
                {
                    GameObject inGameDivideBy = Instantiate(divideBy, new Vector3(transform.position.x - 0.3f * divisors.Count + 0.6f * i + 0.3f, transform.position.y + 0.8f, transform.position.z), Quaternion.identity, transform);
                    inGameDivideBy.GetComponentInChildren<Text>().text = divisors[i].ToString();
                }
                else
                {
                    if(i >= divisors.Count / 2)
                    {
                        GameObject inGameDivideBy = Instantiate(divideBy, new Vector3(transform.position.x - 0.15f * divisors.Count + 0.6f * (i - divisors.Count / 2) + 0.3f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity, transform);
                        inGameDivideBy.GetComponentInChildren<Text>().text = divisors[i].ToString();
                    }
                    else if(i < divisors.Count / 2)
                    {
                        GameObject inGameDivideBy = Instantiate(divideBy, new Vector3(transform.position.x - 0.15f * divisors.Count + 0.6f * i + 0.3f, transform.position.y + 0.8f, transform.position.z), Quaternion.identity, transform);
                        inGameDivideBy.GetComponentInChildren<Text>().text = divisors[i].ToString();
                    }
                }
                
            }
        }
    }
    private void OnMouseOver()
    {
        if (!multiplyPanel.gameObject.activeSelf &&
            spriteRenderer.sortingOrder == 2 && !Input.GetMouseButton(0) && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
        }
    }
    private void OnMouseExit()
    {
        if (!multiplyPanel.gameObject.activeSelf 
            && !Input.GetMouseButton(0) && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            spriteRenderer.material.color = Color.white;
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().setMouseOverDivide(false);
    }

    private void OnMouseDown()
    {
        if (!victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            spriteRenderer.material.color = Color.grey;
            soundFX.playMouseDownOperationClip();
        }
    }

    private void OnMouseEnter()
    {
        if (!multiplyPanel.gameObject.activeSelf && !Input.GetMouseButton(0) 
            && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            soundFX.playMouseOverOperationClip();
        }
    }
}
