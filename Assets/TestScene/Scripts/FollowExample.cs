using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowExample : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
        this.StartChain()
            .Play(transform.CCFollow(target, 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
