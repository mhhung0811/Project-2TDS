using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintPressButton : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public string hintMessage;
    public GameObject hintPanel;
    void Start()
    {
        hintText.text = hintMessage;
        hintPanel.SetActive(false);
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintPanel.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintPanel.SetActive(false);
        }
    }
}
