using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
	public VoidIntVector2FloatFuncProvider spawnFunc;
	public bool isInteractable { get; set; }
	public int id;
	private Animator animator;
	private bool isOpen = false;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	void Start()
    {
	}

    void Update()
    {
        
    }

    public IEnumerator Open()
    {
		isOpen = true;
		animator.SetBool("IsOpen", true);
		yield return new WaitForSeconds(0.5f);
		spawnFunc.GetFunction()?.Invoke((id, transform.position + new Vector3(0, -0.5f, 0), 0));
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
