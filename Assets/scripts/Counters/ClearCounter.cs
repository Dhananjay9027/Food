using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter 
{

    [SerializeField] private KitchenObjectScriptableObjects KitchenObjectSO;
 

    public override void Interact(player player)
    { 
          if(!HasKitchenObject())
        {

            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
          else
        {
            if(player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
   
}
