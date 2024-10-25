using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(menuName = "Flyweight/Projectile Setting")]
public class ProjectileSetting : FlyweightSetting
{
    public float speed = 10f;
    public float damage = 10f;
    public FactoryDespawnEvent factoryDespawnEvent;
    
    public override Flyweight Create()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;
        
        var flyweight = go.GetOrAddComponent<Projectile>();
        flyweight.settings = this;
        
        return flyweight;
    }
}
