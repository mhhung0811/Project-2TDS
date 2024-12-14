using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsWithinStrikingDistance { get; set; }
    float AttackRange { get; set; }
	void SetStrikingDistanceBool(bool isWithinStrikingDistance);
}
