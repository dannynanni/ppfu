﻿using UnityEngine;
using System.Collections;

public class P1CutsceneMoveScript : MonoBehaviour {

	public Transform target;
	public float speed;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
