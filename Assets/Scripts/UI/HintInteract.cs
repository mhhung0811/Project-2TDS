using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintInteract : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public string hintMessage;
    public GameObject outLineInteract;
    public bool canInteract = false;

	void Start()
    {
        
    }

    void Update()
    {

    }

    public void OffInteract()
    {
		canInteract = false;
        outLineInteract.SetActive(false);
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (outLineInteract != null && canInteract)
			{
				outLineInteract.SetActive(true);
			}
		}
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
			if (outLineInteract != null)
            {
				outLineInteract.SetActive(false);
			}
		}
    }
}
