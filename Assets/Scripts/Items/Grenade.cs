using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float gravity;
    private float verticalSpeed;
	private float horizontalSpeed;
    private float height;
    private Vector2 basePos;
    private Vector2 direction;
    public Vector2 targetPos;

    private bool isThrowing = false;
	void Start()
    {
        StartCoroutine(OnExplode());
	}

    // Update is called once per frame
    void Update()
    {
        if(isThrowing)
        {
            verticalSpeed = verticalSpeed + gravity * Time.deltaTime;
            height = height + verticalSpeed * Time.deltaTime;

            basePos += direction * horizontalSpeed * Time.deltaTime;

			this.transform.position = new Vector3(basePos.x, basePos.y + height, 0.0f);

			if (height < 0.0f)
			{
				isThrowing = false;
				height = 0.0f;
				basePos = this.transform.position;
				Explode();
			}
		}
    }

    public void Throw(Vector2 pos)
    {
        isThrowing = true;
        basePos = this.transform.position;
		direction = (pos - basePos);
        verticalSpeed = direction.magnitude * 0.5f;
        float time = 2 * (-verticalSpeed / gravity);
		horizontalSpeed = direction.magnitude / time;
        direction = direction.normalized;
	}

    public void Explode()
    {
        EffectManager.Instance.PlayEffect(EffectType.EfExplode, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
	}

    private IEnumerator OnExplode()
    {
		yield return new WaitForSeconds(1f);
        Throw(targetPos);
	}
}
