using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObjects kitchenObjectSO;
    private IKitchenObjectParent KitchenObjectParent;
    public KitchenObjectScriptableObjects GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent IkitchenObjectParent)
    {
        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

        this.KitchenObjectParent = IkitchenObjectParent;
        if (IkitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a object");
        }
        IkitchenObjectParent.SetKitchenObject(this);
        transform.parent = IkitchenObjectParent.GetkitchenobjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }
    public IKitchenObjectParent GetkitchenobjectParent()
    {
        return KitchenObjectParent;
    }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject() ;
        Destroy(gameObject);
    }
    public static KitchenObject spawnKitchenObject(KitchenObjectScriptableObjects kitchenObjectScriptableObjects,IKitchenObjectParent kitchenobjectparent)
    {
        Transform KitchenObjectTransform = Instantiate(kitchenObjectScriptableObjects.Prefab);
        KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenobjectparent);
        return kitchenObject;
    }
}
