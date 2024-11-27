using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour,KitchenObjectParent
{
    public static Player Instance{get ; private set;}

    public event EventHandler <OnSelectedChangedEventArgs> OnSelectedChanged;
    public class OnSelectedChangedEventArgs : EventArgs{
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    public LayerMask countersLayerMask;
    private bool isMoving;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject KitchenObject;

    private void Awake(){
        if(Instance != null){
            Debug.LogError("more than one instance");
        }else{
            Instance = this;
        }
    }

    private void Start(){
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnIneractAlternative += GameInput_OnIneractAlternative;
    }

    private void GameInput_OnIneractAlternative(object sender, EventArgs e)
    {
        if(selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }

    private void Update(){
        HandleMove();
        HandleInteracte();
    }

    public bool Move(){
        return isMoving;
    }

    private void HandleInteracte(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,0,inputVector.y);

        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position,lastInteractDir,out RaycastHit hit,interactDistance,countersLayerMask)){
            if(hit.transform.TryGetComponent(out BaseCounter baseCounter)){
                // Has clear counter
                if(baseCounter != selectedCounter){
                    SetSelectedCounter(baseCounter);
                }
            }else{
                // No clear counter
                SetSelectedCounter(null);
            }
        }else{
            // No counter in front of the player
                SetSelectedCounter(null);
        }   
    }

    private void HandleMove(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,0,inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDir,moveDistance);

        if(!canMove){
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            canMove = moveDir.x!=0 && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDirX,moveDistance);

            if(canMove){
                moveDir = moveDirX;
            }else{
                // Cannnot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0,0,moveDir.y).normalized;
                canMove = moveDir.z !=0 && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDirZ,moveDistance);

                if(canMove){
                    // Can move only in the Z
                    moveDir = moveDirZ;
                }else{
                    // Cannot move in ant direction;
                }
            }
        }
        if(canMove){
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        isMoving = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;

        OnSelectedChanged?.Invoke(this,new OnSelectedChangedEventArgs{
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return this.kitchenObjectHoldPoint;
    }

    public void SetKitchenobject(KitchenObject kitchenObject)
    {
       this.KitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this.KitchenObject;
    }

    public void ClearKichenObject()
    {
        this.KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
