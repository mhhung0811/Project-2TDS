using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintPressButton : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public string hintMessage;
    public GameObject hintPanel;
    public GameObject outLineInteract;

	void Start()
    {
        if(hintPanel)
        {
            hintText.text = hintMessage;
            hintPanel.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(hintPanel)
            {
                hintPanel.SetActive(true);
            }


            if (outLineInteract != null)
			{
				outLineInteract.SetActive(true);
			}
		}
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(hintPanel != null)
			{
				hintPanel.SetActive(false);
			}
			if (outLineInteract != null)
            {
				outLineInteract.SetActive(false);
			}

		}
    }
}
