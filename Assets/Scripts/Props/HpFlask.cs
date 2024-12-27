using UnityEngine;

public class HpFlask : MonoBehaviour, IInteractable
{
    public bool isInteractable { get; set; }
    public void Interact(GameObject go)
    {
        var player = go.GetComponent<Player>();
        if (player != null)
        {
            player.HP.SetValue(player.HP.CurrentValue + 1);
            Destroy(gameObject);
        }
    }
}