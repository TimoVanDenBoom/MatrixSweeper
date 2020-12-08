using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplyScript : MonoBehaviour
{
    private SoundEffectsScript soundFX;
    private SpriteRenderer spriteRenderer;
    private Transform multiplyPanel;
    private Transform victoryPanel;
    private Transform menu;
    private Transform options;
    private GameObject canvas;
    public GameObject multiplyBy;

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
        GameObject[] multiplyByObjects = GameObject.FindGameObjectsWithTag("MultiplyBy");

        for (int i = 0; i < multiplyByObjects.Length; i++)
        {
            Destroy(multiplyByObjects[i]);
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().setMouseOverMultiply(true);
        if (!victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
            spriteRenderer.material.color = Color.grey;
        if (!victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            GameObject minusone = Instantiate(multiplyBy, new Vector3(transform.position.x - 0.15f * 10 + 0.3f, transform.position.y + 0.8f, transform.position.z), Quaternion.identity, transform);
            minusone.GetComponentInChildren<Text>().text = (-1).ToString();
            for (int i = 1; i < 10; i++)
            {
                    if (i >= 5)
                    {
                        GameObject inGameMultiplyBy = Instantiate(multiplyBy, new Vector3(transform.position.x - 0.15f * 10 + 0.6f * (i - 5) + 0.3f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity, transform);
                        inGameMultiplyBy.GetComponentInChildren<Text>().text = (i+1).ToString();
                    }
                    else if (i < 5)
                    {
                        GameObject inGameMultiplyBy = Instantiate(multiplyBy, new Vector3(transform.position.x - 0.15f * 10 + 0.6f * i + 0.3f, transform.position.y + 0.8f, transform.position.z), Quaternion.identity, transform);
                        inGameMultiplyBy.GetComponentInChildren<Text>().text = (i+1).ToString();
                    }
            }
        }
    }

    private void OnMouseOver()
    {
        if (spriteRenderer.sortingOrder == 2 && !Input.GetMouseButton(0) && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
        }
    }
    private void OnMouseExit()
    {
        if (!multiplyPanel.gameObject.activeSelf && !Input.GetMouseButton(0) && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            spriteRenderer.material.color = Color.white;
        }
        GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>().setMouseOverMultiply(false);
    }

    private void OnMouseDown()
    {
        if (!victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            soundFX.playMouseDownOperationClip();
            spriteRenderer.material.color = Color.grey;
        }
    }

    void ClosePanel()
    {
        multiplyPanel.gameObject.SetActive(false);
        spriteRenderer.material.color = Color.white;
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0) 
            && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            soundFX.playMouseOverOperationClip();
        }
    }
}
