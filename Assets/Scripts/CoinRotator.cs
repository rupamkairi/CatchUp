using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour {

	void Update () {
		transform.Rotate(0, Random.Range(4, 8), 0, Space.World);		
	}
}
