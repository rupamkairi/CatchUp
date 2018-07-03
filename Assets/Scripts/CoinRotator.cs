using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour {

	void Update () {
		transform.Rotate(0, 5, 0, Space.World);		
	}
}
