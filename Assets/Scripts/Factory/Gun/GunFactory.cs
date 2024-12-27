using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class GunFactory : MonoBehaviour
{
    [SerializeField] private InUseGun inUseGun;
    
    [Header("Pool setting")]
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    
    private Dictionary<int, ObjectPool<GameObject>> _gunPools = new Dictionary<int, ObjectPool<GameObject>>();
    private Dictionary<int, Transform> _gunPoolTransforms = new Dictionary<int, Transform>();
    private Dictionary<int, ObjectPool<GameObject>> _gunPrefPools = new Dictionary<int, ObjectPool<GameObject>>();
    private Dictionary<int, Transform> _gunPrefPoolTransforms = new Dictionary<int, Transform>();
    
    private void Awake()
    {
        foreach (var gun in inUseGun.guns)
        {
            int id = gun.gunId;

            _gunPools[id] = new ObjectPool<GameObject>(
                createFunc: () =>
                {
                    var go = Instantiate(gun.gunPrefab);
                    go.GetComponent<GunBase>().SetGunId(id);
                    return go;
                },
                actionOnGet: gun =>
                {
                    gun.SetActive(true);
                },
                actionOnRelease: gun =>
                {
                    gun.transform.SetParent(_gunPoolTransforms[id]);
                    gun.SetActive(false);
                },
                actionOnDestroy: Destroy,
                defaultCapacity: defaultCapacity,
                maxSize: maxPoolSize
            );
            var obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.name = "Gun Pool No." + gun.gunId;
            _gunPoolTransforms.Add(gun.gunId, obj.transform);
            
            _gunPrefPools[id] = new ObjectPool<GameObject>(
                createFunc: () =>
                {
                    var go = Instantiate(gun.gunPref);
                    go.GetComponent<GunPref>().SetGunId(id);
                    return go;
                },
                actionOnGet: gun => gun.SetActive(true),
                actionOnRelease: gun => gun.SetActive(false),
                actionOnDestroy: Destroy,
                defaultCapacity: defaultCapacity,
                maxSize: maxPoolSize
            );
            obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.name = "Gun Pref Pool No." + gun.gunId;
            _gunPrefPoolTransforms.Add(gun.gunId, obj.transform);
        }
    }

    private void Start()
    {
        GetGunPref((0, Vector2.zero, 0));
    }

    // Func
    public GameObject GetGun(int id)
    {
        if (_gunPools.ContainsKey(id))
        {
            var gun = _gunPools[id].Get();
            gun.transform.SetParent(_gunPoolTransforms[id]);
            return gun;
        }

        Debug.LogError($"Gun ID {id} not found in factory!");
        return null;
    }
    
    // Func
    public object ReturnGun(GameObject gun)
    {
        var id = gun.GetComponent<GunBase>().gunId;
        
        if (_gunPools.ContainsKey(id))
        {
            _gunPools[id].Release(gun);
        }
        else
        {
            Debug.LogError($"Gun ID {id} not found in factory!");
            Destroy(gun);
        }

        return null;
    }
    
    // Func 
    public object GetGunPref((int id, Vector2 position, float rotation) parameters)
    {
        if (_gunPrefPools.ContainsKey(parameters.id))
        {
            Debug.Log("Is getting gun pref");
            var gun = _gunPrefPools[parameters.id].Get();
            gun.transform.SetParent(_gunPrefPoolTransforms[parameters.id]);
            gun.transform.position = parameters.position;
            gun.transform.rotation = Quaternion.Euler(0, 0, parameters.rotation);
        }
        else
        {
            Debug.LogError($"Gun ID {parameters.id} not found in factory!");
        }

        return null;
    }
    
    // Func
    public object ReturnGunPref(GameObject gun)
    {
        var id = gun.GetComponent<GunPref>().gunId;
        
        if (_gunPrefPools.ContainsKey(id))
        {
            _gunPrefPools[id].Release(gun);
        }
        else
        {
            Debug.LogError($"Gun ID {id} not found in factory!");
            Destroy(gun);
        }

        return null;
    }
}