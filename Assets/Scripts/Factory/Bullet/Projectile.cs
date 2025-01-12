using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Flyweight
{
    public new ProjectileSetting settings {
        get => (ProjectileSetting)base.settings;
		set => base.settings = value;
	}

    public Rigidbody2D _rb;

	public virtual void Awake()
    {
		_rb = GetComponent<Rigidbody2D>();
	}


	public virtual void OnEnable()
    {
		float rotation = transform.rotation.eulerAngles.z;
		float xDirection = Mathf.Cos(rotation * Mathf.Deg2Rad);
		float yDirection = Mathf.Sin(rotation * Mathf.Deg2Rad);
		_rb.velocity = settings.speed * new Vector2(xDirection, yDirection);
	}

	public virtual void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }
}
