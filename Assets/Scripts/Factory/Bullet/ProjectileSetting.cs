using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Flyweight/Projectile Setting")]
public class ProjectileSetting : FlyweightSetting
{
    public float speed = 10f;
    public float damage = 10f;
    [FormerlySerializedAs("factoryDespawnEvent")] public FlyweightEvent flyweightEvent;
    
    public override Flyweight Create() // Instantiate callback OnEnable
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;
        
        var flyweight = go.GetOrAddComponent<Projectile>();
        flyweight.settings = this;

		return flyweight;
    }

	public override void OnGet(Flyweight f)
	{
        return;
	}
}
