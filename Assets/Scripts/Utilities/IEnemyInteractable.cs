using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyInteractable
{
    bool IsEnemyInteractable { get; set; }
	void OnEnemyBulletHit(float damge);
}
