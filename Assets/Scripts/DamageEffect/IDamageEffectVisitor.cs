using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageEffectVisitor
{
    void Visit(Player player);
	void Visit(Enemy enemy);
}
