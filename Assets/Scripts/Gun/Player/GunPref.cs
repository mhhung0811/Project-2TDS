using System.Collections;
using UnityEngine;

public class GunPref : MonoBehaviour, IInteractable
{
    public int gunId { get; private set; }
    public bool isInteractable { get; set; }
    public VoidGameObjectFuncProvider returnGunPrefFunc;
    public GameObjectIntFuncProvider getGunFunc;
    private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    private void Start()
	{
		StartCoroutine(StateInit());
	}

	public void SetGunId(int id)
    {
        gunId = id;

    }
    public void Interact(GameObject go)
    {
        // Get gun
        GameObject obj = getGunFunc.GetFunction()?.Invoke((gunId));
        if (obj != null && obj.GetComponent<GunBase>() != null)
        {
            go.GetComponent<PlayerInventory>().AddGun(obj.GetComponent<GunBase>());
        }
        else
        {
            Debug.LogError("Failed to get gun from gun pref.");
        }
        
        // Return gun pref
        returnGunPrefFunc.GetFunction()?.Invoke(gameObject);
    }

    public IEnumerator StateInit()
    {
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
		yield return new WaitForSeconds(1f);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
	}
}