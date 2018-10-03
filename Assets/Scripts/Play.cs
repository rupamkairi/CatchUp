using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour {

    public Button play;

	// Use this for initialization
	void Start () {
        play.onClick.AddListener(HandleClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleClick()
    {
        Application.LoadLevel(index: 1);
        Application.UnloadLevel(index: 0);
    }
}
