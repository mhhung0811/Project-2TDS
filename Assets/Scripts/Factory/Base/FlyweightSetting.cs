using UnityEngine;

public abstract class FlyweightSetting : ScriptableObject
{
    public FlyweightType type;
    public GameObject prefab;

    public abstract Flyweight Create();
    
    public virtual void OnGet(Flyweight f) => f.gameObject.SetActive(true);
    public virtual void OnRelease(Flyweight f) => f.gameObject.SetActive(false);
    public virtual void OnDestroyPoolObject(Flyweight f) => Destroy(f.gameObject);
}