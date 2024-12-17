using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMove
{
    Rigidbody2D RB { get; set; }
    float MoveSpeed { get; set; }
	bool IsFacingRight { get; set; }
    void MoveEnemy(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
