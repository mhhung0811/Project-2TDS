using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlyweightFactory : MonoBehaviour
{
    [SerializeField] private List<FlyweightSetting> projectileSettings;
    
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    
    private Dictionary<FlyweightType, FlyweightSetting> _flyweightSettingDictionary = new Dictionary<FlyweightType, FlyweightSetting>();
    readonly Dictionary<FlyweightType, IObjectPool<Flyweight>> _pools = new();
    
    private IObjectPool<Flyweight> GetPoolFor(FlyweightType type)
    {
        IObjectPool<Flyweight> pool;
        
        if (_pools.TryGetValue(type, out pool)) return pool;
        pool = new ObjectPool<Flyweight>(
            _flyweightSettingDictionary[type].Create,
            _flyweightSettingDictionary[type].OnGet,
            _flyweightSettingDictionary[type].OnRelease,
            _flyweightSettingDictionary[type].OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxPoolSize
        );
        _pools.Add(type, pool);
        return pool;
    }
    
    public void Spawn(FlyweightType type, Vector2 position, float rotation)
    {
        Debug.Log($"Spawning Flyweight: {type}");
        Debug.Log($"Position: {position}");
        Debug.Log($"Rotation: {rotation}");
        
        Flyweight fw = GetPoolFor(type)?.Get();
        if (fw == null) return;
        
        fw.transform.position = position;
        fw.transform.eulerAngles = new Vector3(0, 0, rotation);
    }
    public void ReturnToPool(Flyweight f) => GetPoolFor(f.settings.type)?.Release(f);

    private void Awake()
    {
        // Set up projectile dictionary
        foreach (var setting in projectileSettings)
        {
            _flyweightSettingDictionary.Add(setting.type, setting);
        }
    }
}