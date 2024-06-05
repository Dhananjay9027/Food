using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{
    [SerializeField] private Transform CounterTop;
    private KitchenObject kitchenObject;
    public virtual void Interact(player player)
    {
         
    }
    public virtual void InteractAlternate(player player)
    {

    }
    public Transform GetkitchenobjectFollowTransform()
    {
        return CounterTop;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }
}
