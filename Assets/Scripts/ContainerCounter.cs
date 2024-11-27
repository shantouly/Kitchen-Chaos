using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KichenObjectSO KichenObjectSO;

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            if(!HasKitchenObject()){                
                KitchenObject.SpawnKitchenObject(KichenObjectSO,player);
                // 播放动作
                OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
