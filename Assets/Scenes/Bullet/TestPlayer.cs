using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TestPlayer : MonoBehaviour
{
    [FormerlySerializedAs("factorySpawnEvent")] public FlyweightTypeVector2FloatEvent flyweightTypeVector2FloatEvent;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            flyweightTypeVector2FloatEvent.Raise((FlyweightType.BasicBullet, new Vector2(this.transform.position.x, this.transform.position.y), 0));
            yield return new WaitForSeconds(0.5f);
        }
    }
}