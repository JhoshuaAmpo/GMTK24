using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ET_BaseMouth : EatTrait
{
    CapsuleCollider2D col;
    protected override void Awake() {
        base.Awake();
        col = GetComponent<CapsuleCollider2D>();
        mouthSize = col.size.x * col.size.y;
    }
}
