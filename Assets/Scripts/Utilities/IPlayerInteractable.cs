using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInteractable
{
	bool IsPlayerInteractable { get; set; }
	void OnPlayerBulletHit();
}
