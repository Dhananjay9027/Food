using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    private const string IS_WALK = "isWalk";
    [SerializeField] private player player;
    private Animator animator;  //access the animation
    private void Awake()  //awake call only on awake
    {
        animator=GetComponent<Animator>();
    }
    private void Update()  //update value 
    {
        animator.SetBool(IS_WALK, player.Is_Walking());//.set to set values and all
    }
}
