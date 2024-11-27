using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KichenObjectSO KichenObjectSO;
    private KitchenObjectParent kichenObjectParent;

    public KichenObjectSO GetKitchenObjectSO(){
        return KichenObjectSO; 
    }

    public void SetKitchenObjectParent(KitchenObjectParent kichenObjectParent){
        if(this.kichenObjectParent != null){
            this.kichenObjectParent.ClearKichenObject();
        }
        this.kichenObjectParent = kichenObjectParent;
        
        // if(kichenObjectParent.HasKitchenObject()){
        //     Debug.LogError("KitchenObjectParent already has a kitchenObject");
        // }
        kichenObjectParent.SetKitchenobject(this);

        transform.parent = kichenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public KitchenObjectParent GetKitchenObjectParent(){
        return this.kichenObjectParent;
    }

    // 用于cuttingCounter，用来销毁食物，并且将子父级关系清除
    public void DestroySelf(){
        kichenObjectParent.ClearKichenObject();

        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KichenObjectSO kichenObjectSO, KitchenObjectParent kitchenObjectParent){
        Transform obj = Instantiate(kichenObjectSO.prefab);
                //obj.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        KitchenObject kitchenObject =  obj.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}
