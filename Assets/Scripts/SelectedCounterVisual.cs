using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] CounterVisualArray;
    private void Start(){
        Player.Instance.OnSelectedChanged += Player_OnSelectedChanged;
    }

    private void Player_OnSelectedChanged(object sender, Player.OnSelectedChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter){
            Show();
        }else{
            Hide();
        }
    }

    private void Show(){
        foreach(var item in CounterVisualArray){
            item.SetActive(true);
        }
    }

    private void Hide(){
        foreach(var item in CounterVisualArray){
            item.SetActive(false);
        }
    }
}
