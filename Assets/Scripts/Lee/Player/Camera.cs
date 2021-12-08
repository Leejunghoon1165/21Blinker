using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    internal CameraClearFlags clearFlags;

    
    Renderer ObstacleRenderer;  // 레이 검출된 건물의 렌더링
    void Update()
    {
        transform.position = target.position + offset;

        FadeOutwall();


    }


    // 카메라에서 레이를 쏴 건물을 검출하여 해당 건물을 투명화 시킴 ( 미구현 )
    private void FadeOutwall()
    {
        
    
    }
}
