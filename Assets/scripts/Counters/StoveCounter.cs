using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryRecipeSO[] fryRecipeSOs;
    public override void Interact(player player)
    {

        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                   
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectScriptableObjects kitchenObjectScriptableObjects)
    {
        FryRecipeSO  FryingRecipeSO = GetFryingRecipeSO(kitchenObjectScriptableObjects);
        return FryingRecipeSO != null;
    }
    private KitchenObjectScriptableObjects GetOutput(KitchenObjectScriptableObjects input)
    {
        FryRecipeSO FryingRecipeSO = GetFryingRecipeSO(input);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private FryRecipeSO GetFryingRecipeSO(KitchenObjectScriptableObjects input)
    {
        foreach (FryRecipeSO FryingRecipeSO in fryRecipeSOs)
        {
            if (FryingRecipeSO.input == input)
            {
                return FryingRecipeSO;
            }
        }
        return null;
    }
}
