using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EndEffect() { 
        Destroy(gameObject);
    }
}
