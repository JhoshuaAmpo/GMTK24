using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ET_BaseMouth : EatTrait
{
    CircleCollider2D col;
    protected override void Awake() {
        base.Awake();
        col = GetComponent<CircleCollider2D>();
        mouthSize = col.radius * 2 * col.radius * 2;
    }
}
