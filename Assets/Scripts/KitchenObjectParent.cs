using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface KitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenobject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKichenObject();

    public bool HasKitchenObject();
}
