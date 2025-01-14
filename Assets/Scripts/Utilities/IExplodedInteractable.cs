using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplodedInteractable
{
    bool IsExplodedInteractable { get; set; }
	void OnExplode();
}
