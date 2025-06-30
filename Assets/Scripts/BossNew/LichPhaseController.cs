using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhaseController : MonoBehaviour
{
    public Transform posTele;
    public Vector2Event OnLichHandPull;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndPhaseLich1Handler()
    {
        OnLichHandPull?.Raise(posTele.position);
	}
}
