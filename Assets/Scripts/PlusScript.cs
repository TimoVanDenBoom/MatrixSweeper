using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlusScript : MonoBehaviour
{
    private SoundEffectsScript soundFX;
    private SpriteRenderer spriteRenderer;
    private RowScript rowScript;
    private Transform multiplyPanel;
    private Transform victoryPanel;
    private Transform menu;
    private Transform options;
    private GameObject canvas;

    private void Start()
    {
        soundFX = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rowScript = transform.parent.transform.GetComponent<RowScript>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        multiplyPanel = canvas.transform.GetChild(0);
        victoryPanel = canvas.transform.GetChild(1);
        menu = canvas.transform.GetChild(2);
        options = canvas.transform.GetChild(3);
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            soundFX.playMouseDownOperationClip();
            spriteRenderer.material.color = Color.grey;
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            rowScript.SetStartPosX(mousePos.x - transform.parent.transform.position.x);
            rowScript.SetStartPosY(mousePos.y - transform.parent.transform.position.y);
            rowScript.SetIsDragged(true);
        }
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "Number")
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = 6;
            }
        }
    }
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
        rowScript.SetIsDragged(false);
        foreach (Transform child in transform.parent.transform)
        {
            if (child.tag == "Number")
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
        foreach (Transform child in transform.parent.transform.parent.transform)
        {
            if ((child.localPosition - transform.parent.transform.localPosition).magnitude < (0.5 * transform.parent.transform.parent.localScale.x)  
                && child != transform.parent.transform)
            {
                transform.parent.transform.localPosition = rowScript.GetOriginalPos();
                bool worked = child.GetComponent<RowScript>().Addition(rowScript.getNumbers());
                if (worked)
                {
                    soundFX.playMouseUpAdditionClip();
                }
            }
        }
        transform.parent.transform.localPosition = rowScript.GetOriginalPos();
    }

    private void OnMouseOver()
    {
        if (!multiplyPanel.gameObject.activeSelf &&
            spriteRenderer.sortingOrder == 2 && !Input.GetMouseButton(0) && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf && !options.gameObject.activeSelf)
        {
            spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
        }
        if(spriteRenderer.material.color != Color.grey && rowScript.GetIsDragged())
        {
            spriteRenderer.material.color = Color.grey;
        }
    }
    private void OnMouseExit()
    {
        if (!multiplyPanel.gameObject.activeSelf && spriteRenderer.material.color != Color.grey)
        {
            spriteRenderer.material.color = Color.white;
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
