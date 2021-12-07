using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiJoy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform player;
    Vector3 move;
    public float speed;
    bool walking;

    public RectTransform pad;  //패드
    public RectTransform stick;  //스틱

    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;  //스틱의 위치는 손가락이 터치된곳
        //스틱의 고정된 위치는 패드의 반지름의 반에서 벗어나지 않는 위치로 고정
        stick.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position,pad.rect.width * 0.5f);

        move = new Vector3(stick.localPosition.x, 0, stick.localPosition.y).normalized;

        if (!walking)   //Bool 값 지정하여 움직이는 작동시에 애니메이션이 반복 작동 되는것을 막음
        {
            walking = true;
            player.GetComponent<Animator>().SetBool("isWalk", move != Vector3.zero);
        }
            
    }
       
    //처음 패드 생성 위치
    public void OnPointerDown(PointerEventData eventData)
    {
        pad.position = eventData.position;  //처음 패드의 위치는 손가락이 터치된곳
        pad.gameObject.SetActive(true);   //손가락 터치시에 패드가 나타나게함
        StartCoroutine("Movement");   //처음 터치할시 움직임 코르틴  호출 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.localPosition = Vector2.zero;  //손가락을 뗄경우 다시 돌아가게 
        pad.gameObject.SetActive(false);      //뗄경우 사라짐
        StopCoroutine("Movement");   //손을 뗄경우 코르틴 멈춤
        move=Vector3.zero;  //손을 뗄경우는 움직임 멈춤


        walking = false;
        player.GetComponent<Animator>().SetBool("isWalk", move != Vector3.zero);
       
    }

    IEnumerator Movement()
    {
        while(true)
        {
            //player.transform.position += move * speed * Time.deltaTime;
            //translate -> 현재 좌표를 기준으로 이동시킴
            player.Translate(move * speed * Time.deltaTime,Space.World);

            //회전
            //Quaternion.Slerp(A,B,C) -> A의 회전값을 B의 회전값으로 C속도로 보정하면서 회전한다   //아래에서 숫자 5는 회전 속도 
            if (move != Vector3.zero)
                player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(move), 5 * Time.deltaTime);
            //transform.LookAt(transform.position + move);

            yield return null;
        }
    }
   
}
