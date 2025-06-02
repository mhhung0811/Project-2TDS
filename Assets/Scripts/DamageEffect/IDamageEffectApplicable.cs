using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageEffectApplicable
{
    void Accept(IDamageEffectVisitor visitor);
}
