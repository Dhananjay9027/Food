using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
     
    [SerializeField] private KitchenObjectScriptableObjects KitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.spawnKitchenObject(KitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }  
    }
    
}
