using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player player;

    private Animator anim;

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    private void Update(){
        anim.SetBool("IsWalking",player.Move());
    }
}
