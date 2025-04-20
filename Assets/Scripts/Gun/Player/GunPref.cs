using System.Collections;
using UnityEngine;

public class GunPref : MonoBehaviour, IInteractable
{
	public GunType gunType;
    public bool isInteractable { get; set; }

    public GunTypeEvent unlockGun;
    private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    private void Start()
	{
		StartCoroutine(StateInit());
	}
    
    public void Interact(GameObject go)
    {
	    unlockGun?.Raise(gunType);
	    
	    Debug.Log("Gun pref unlocked: " + gunType);
	    
	    Destroy(gameObject);
    }

    private IEnumerator StateInit()
    {
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
		yield return new WaitForSeconds(1f);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
	}
}