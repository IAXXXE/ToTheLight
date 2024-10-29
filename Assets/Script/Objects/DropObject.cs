using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DropObject : ObjectBase
{
    public Vector3 posToGo;

    protected override void ExecuteAction()
    {
        transform.DOJump(posToGo, 3f, 2, 2f).OnComplete(() => { Destroy(gameObject); });
    }
}
