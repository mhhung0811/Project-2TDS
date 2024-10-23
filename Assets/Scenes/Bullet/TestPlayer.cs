using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public FactorySpawnEvent factorySpawnEvent;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            factorySpawnEvent.Raise(FlyweightType.BasicBullet, transform.position, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}