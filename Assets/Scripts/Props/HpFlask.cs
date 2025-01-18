using System.Collections;
using UnityEngine;

public class HpFlask : MonoBehaviour, IInteractable
{
    public Rigidbody2D rb;
	public bool isInteractable { get; set; }

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
        var player = go.GetComponent<Player>();
        if (player != null)
        {
            player.HP.CurrentValue += 2;
			if(player.HP.CurrentValue > player.MaxHP.CurrentValue)
			{
				player.HP.CurrentValue = player.MaxHP.CurrentValue;
			}
			Destroy(gameObject);
        }
    }

	public IEnumerator StateInit()
	{
		rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
		yield return new WaitForSeconds(1f);
		rb.gravityScale = 0;
		rb.velocity = Vector2.zero;
	}
}