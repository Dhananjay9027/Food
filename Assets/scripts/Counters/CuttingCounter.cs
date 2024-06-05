using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter 
{
    [SerializeField] private CuttingRecipeSO[] CuttingRecipeSOArray;
    private int cuttingProgress;
    public event EventHandler <OnprogressChangedEventArgs> OnProgressChanged;
    public class OnprogressChangedEventArgs: EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;
    public override void Interact(player player)
    {
        if (!HasKitchenObject())
        {

            if (player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnprogressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
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
    public override void InteractAlternate(player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new OnprogressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectScriptableObjects output = GetOutput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.spawnKitchenObject(output, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObjects kitchenObjectScriptableObjects)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(kitchenObjectScriptableObjects);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectScriptableObjects GetOutput(KitchenObjectScriptableObjects input)
    {
        CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSO(input);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectScriptableObjects input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in CuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

