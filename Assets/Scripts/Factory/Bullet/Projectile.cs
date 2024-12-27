using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Flyweight
{
    private new ProjectileSetting settings => (ProjectileSetting)base.settings;

    private Rigidbody2D _rb;

    void Awake()
    {
        // Setting up components
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        float rotation = transform.rotation.eulerAngles.z;
        float xDirection = Mathf.Cos(rotation * Mathf.Deg2Rad);
        float yDirection = Mathf.Sin(rotation * Mathf.Deg2Rad);
        _rb.velocity = settings.speed * new Vector2(xDirection, yDirection);
    }

    private void OnEnable()
    {
        StartCoroutine(delayDespawn());
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }

    IEnumerator delayDespawn()
    {
        yield return new WaitForSeconds(2);
        settings.flyweightEvent.Raise(this);
    }
}
