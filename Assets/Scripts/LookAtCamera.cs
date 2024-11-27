using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode{
        LookAt,             // 进度条从左边开始
        LookAtInverted,     // 进度条从右边开始
        CameraForward,      // 这个会使得Bar显示的笔直，不会随着镜头的改变而改变方向
        CameraInverted,     // 笔直且右边开始
    };

    [SerializeField] private Mode mode;

    private void LateUpdate(){
        switch(mode){
            case Mode.LookAtInverted:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAt:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        
    }
}
