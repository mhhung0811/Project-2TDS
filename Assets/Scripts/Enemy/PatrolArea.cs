using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolArea : MonoBehaviour
{
	public List<Transform> patrolPoints = new List<Transform>();
    public float height = 5f;
	public float width = 10f;

	private void Awake()
	{
		patrolPoints.Clear();
        foreach (Transform child in transform)
        {
            patrolPoints.Add(child);
        }
    }

	void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool CheckEnemyInPatrolArea(Vector3 enemyPos)
    {
        if(enemyPos.x > transform.position.x - width / 2 && enemyPos.x < transform.position.x + width / 2 &&
			enemyPos.y > transform.position.y - height / 2 && enemyPos.y < transform.position.y + height / 2)
		{
			return true;
		}
		return false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		// Draw area Box
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));

		// Draw points
		for (int i = 0; i < patrolPoints.Count; i++)
        {
            if(patrolPoints[i] != null)
            {
                Gizmos.DrawWireSphere(patrolPoints[i].position, 0.5f);

                if(i < patrolPoints.Count - 1 && patrolPoints[i] != null)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
				}
			}
        }

        if(patrolPoints.Count > 1)
        {
            Gizmos.DrawLine(patrolPoints[patrolPoints.Count - 1].position, patrolPoints[0].position);
		}
	}
}
