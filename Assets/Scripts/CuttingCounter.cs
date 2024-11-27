using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter
{
    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs{
        public float progressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
     public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                if(HasReceipeObjectInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        }else{
            if(!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {   // 如果上面有食物的话
        if(HasKitchenObject() && HasReceipeObjectInput(GetKitchenObject().GetKitchenObjectSO())){
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
                KichenObjectSO outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObject,this);
            }
        }
    }

    private bool HasReceipeObjectInput(KichenObjectSO kichenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kichenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KichenObjectSO GetOutputForInput(KichenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null){
            return cuttingRecipeSO.output;
        }else{
             return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KichenObjectSO kichenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(kichenObjectSO == cuttingRecipeSO.input){
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
