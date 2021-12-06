// ------------------------------------------------------------------------------------------------------------
// 							 Rotate.cs
//  	Authors: Leonardo M. Lopes <euleoo@gmail.com> - http://about.me/leonardo_lopes
// ------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	[Range (-100f, 100f)]
	public float speed = 20f;

	void Update () {
		this.transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
	}
}
