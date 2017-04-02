using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;

public class HumanNavigation : Navigation
{
	
	// Use this for initialization
	private void Start ()
	{
		Initialize();      
	}
	
	// Update is called once per frame
	private void Update ()
	{
		UpdateWanderPath();
	}

	
}
