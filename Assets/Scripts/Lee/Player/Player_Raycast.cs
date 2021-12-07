using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Raycast : MonoBehaviour
{
    //트랜스폼을 담을 변수
    public Transform m_tr;
    public Transform r_tr;

    public GameObject Target;

    //레이 길이를 지정할 변수
    public float distance = 20.0f;

    //충돌 정보를 가져올 레이케스트 히트
    public RaycastHit hit;

    //레이어 마스크 정보를 지정할 변수
    public LayerMask m_layerMask = -1;

    //충돌 정보를 여러개 담을 레이케스트 히트배열
    public RaycastHit[] hits;

    // Start is called before the first frame update
    void Start()
    {
        //트랜스폼을 받아온다
        m_tr = GetComponent<Transform>();
        r_tr.position = r_tr.position + new Vector3(0f, 1f, 0);
    }

    private void FixedUpdate()
    {
        //레이 세팅
        Ray ray = new Ray();

        //레이 시작 지점 세팅
        ray.origin = r_tr.position;

        //방향 설정
        ray.direction = r_tr.forward;

        //if (Physics.Raycast(ray, out hit, distance, m_layerMask))
        //{
        //    print(hit.collider.name + "를 충돌체로 검출해 냈다!");
        //}

        //RaycastAll은 RaycastHit []를 반환한다
        hits = Physics.RaycastAll(ray, distance, m_layerMask);

        //hits의 길이를 확인하고 사용한다!
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].collider.gameObject.GetComponent<RenderManager>().show();
                //hits[i].collider.gameObject.GetComponent<Mesh>();
                // hits[i].collider.gameObject.
                //print(hits[i].collider.gameObject.name + " " + i);
               // hits[i].collider.gameObject.GetComponent<EnemyFinder>().Look();
            }
        }
        else
        {
            return;
        }
        OnDrawRayLine();
    }
    public void OnDrawRayLine()
    {
        if (hit.collider != null)
        {
            Debug.DrawLine(r_tr.position, r_tr.position + r_tr.forward * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawLine(r_tr.position, r_tr.position + r_tr.forward * this.distance, Color.white);
        }
    }
}
