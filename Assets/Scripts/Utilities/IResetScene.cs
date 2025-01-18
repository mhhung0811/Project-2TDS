using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetScene
{
	public bool isReset { get; set; }
	public void ResetScene();
}
