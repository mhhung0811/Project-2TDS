using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlyweightFactory : MonoBehaviour
{
    [SerializeField] private List<FlyweightSetting> projectileSettings;
    [SerializeField] private List<Transform> projectilePools;
    
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    
    private Dictionary<FlyweightType, FlyweightSetting> _flyweightSettingDictionary = new Dictionary<FlyweightType, FlyweightSetting>();
    private Dictionary<FlyweightType, Transform> _flyweightPoolDictionary = new Dictionary<FlyweightType, Transform>();
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
        Flyweight fw = GetPoolFor(type)?.Get();
        if (fw == null) return;
        
        fw.transform.SetParent(_flyweightPoolDictionary[type]);
        fw.transform.position = position;
        fw.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    public void ReturnToPool(Flyweight f) => GetPoolFor(f.settings.type)?.Release(f);

    private void Awake()
    {
        // Set up projectile dictionary
        foreach (var setting in projectileSettings)
        {
            _flyweightSettingDictionary.Add(setting.type, setting);
            
            // Set up pool
            var obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.name = setting.type + " Pool";
            _flyweightPoolDictionary.Add(setting.type, obj.transform);
        }
    }
}