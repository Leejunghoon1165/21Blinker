using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Transform playerpos;
   
    Vector3 dir;
    float acceleration; //가속도

    float velocity; // 한프레임의 가속도

    void Start() {
        
    }

	
    void Update() {
		playerpos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        CoinMove();
		
    }

    public void CoinMove()
	{
		
		dir= (playerpos.position - transform.position).normalized;

		acceleration= 0.2f;

		velocity = (velocity + acceleration* Time.deltaTime);

		float distance = Vector3.Distance(transform.position, playerpos.position);

		

        if (distance <= 5.0f)

		{

			transform.position = new Vector3(transform.position.x + (dir.x * velocity),

												   transform.position.y,

													 transform.position.z+(dir.z * velocity));

		}

		else

		{

			velocity = 0.0f;

		}

	}


    
}
