using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
	public GameObject gunPref;
	public GameObject hpItem;
	public GameObject ammoCell;
	public bool isInteractable { get; set; }
	public int id;
	private Animator animator;
	private bool isOpen = false;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

    public IEnumerator Open()
    {
		isOpen = true;
		animator.SetBool("IsOpen", true);
		yield return new WaitForSeconds(0.5f);
		
		if (gunPref)
		{
			Instantiate(gunPref, transform.position + new Vector3(0, -0.75f, 0), Quaternion.identity);
		}

		if(hpItem)
		{
			Instantiate(hpItem, transform.position + new Vector3(-0.75f, -0.75f, 0), Quaternion.identity);
		}

		if (ammoCell)
		{
			Instantiate(ammoCell, transform.position + new Vector3(0.75f, -0.75f, 0), Quaternion.identity);
		}
	}

	public void Interact(GameObject go)
	{
		if(isOpen)
		{
			return;
		}

		StartCoroutine(Open());
	}
}
