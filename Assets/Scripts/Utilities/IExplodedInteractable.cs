using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplodedInteractable
{
    bool CanExplodeInteractable { get; set; }
	void OnExplode(float damage);
}
