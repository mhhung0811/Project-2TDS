using UnityEngine;

public interface IPlayerItem
{
    public int manaCost { get; set; }
    public float cooldown { get; set; }
    public void UseItem(Vector2 pos);
}