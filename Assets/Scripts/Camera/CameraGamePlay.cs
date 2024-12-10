using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGamePlay : MonoBehaviour
{
	public Transform Player;
	void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = new Vector3(Player.position.x, Player.position.y, this.transform.position.z);
	}
}
