using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class player : MonoBehaviour , IKitchenObjectParent
{
    [SerializeField] private float moveSpeed = 7f; //serialisze.. use to show it on unity so that we can change it from thier
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask CounterlayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    /*public static player instanceFiled;
    public static player GetInstanceField()
    {
        return instanceFiled;
    }
    public static void SetInstance(player instanceFiled)
    {
        player.instanceFiled = instanceFiled;
    }*/
    public static player Instance { get; private set; }// does same thing as commented code at the top do
    public event EventHandler<OnSelectCounterChangeEventArgs> OnSelectCounterChange;
    public class OnSelectCounterChangeEventArgs : EventArgs
    {
        public BaseCounter Selectcounter;
    }

    private Vector3 LastIntereactDirr;
    private bool IsWalking;
    private BaseCounter selected_counter;
    private KitchenObject kitchenObject;
    

    private void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction  ;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selected_counter != null)
        {
            selected_counter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
       if(selected_counter!=null)
        {
            selected_counter.Interact(this);
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    public bool Is_Walking()
    {
        return IsWalking;
    }
    
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0.5f,inputVector.y); //i don;t know why I write .5f
        Vector3 pataniv = new Vector3(0, 0.5f, 0); //new 0.5f vector to check LastIntreact  
        float interactdistance = 2f;
        if(moveDir !=pataniv)
        {
            LastIntereactDirr = moveDir;
        }
        if(Physics.Raycast(transform.position, LastIntereactDirr, out RaycastHit raycastHit, interactdistance,CounterlayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))  //take the component and check if it is desired component or not
            {
                if(baseCounter!=selected_counter)
                {
                    SetSelectCounter(baseCounter);
                }
            }
            else
            {
                SetSelectCounter(null);
            }
        }
        else
        {
            SetSelectCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //write f for float
        float playerRadius = .7f;
        float moveDistance = moveSpeed * Time.deltaTime;
        float playHeight = 2f;
        Vector3 patani = new Vector3(0, 2, 0);
        bool canMove = !Physics.CapsuleCast(transform.position - patani, transform.position + Vector3.up * playHeight, playerRadius, moveDir, moveDistance); //tarnsform.position is player position 
        if (!canMove)
        {
            //attemt only x movement
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = canMove =moveDir.x!=0 && !Physics.CapsuleCast(transform.position - patani, transform.position + Vector3.up * playHeight, playerRadius, moveDirx, moveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            }
            else
            {
                //attemp z movement
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z).normalized;
                canMove = canMove = moveDir.z!=0 && !Physics.CapsuleCast(transform.position - patani, transform.position + Vector3.up * playHeight, playerRadius, moveDirz, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirz;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;  // Time.deltaTime is use to set movement as per the frame  rate of machine i.e how many second it take to render 1 frame
        }
        // transform.forward = moveDir; can use this but for smoothness using the below
        IsWalking = moveDir != Vector3.zero; //set false when it is not moving *for animation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void SetSelectCounter(BaseCounter selectCounter)
    {
        this.selected_counter = selectCounter;
        OnSelectCounterChange?.Invoke(this, new OnSelectCounterChangeEventArgs
        {
            Selectcounter = selected_counter
        });
    }

    public Transform GetkitchenobjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
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
