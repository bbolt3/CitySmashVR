using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;

public abstract class Navigation : MonoBehaviour {

	public GameObject[] targets;

	private int targetNumber = 0;
	protected NavMeshAgent agent;
	protected VRTK_InteractableObject interactableObject;
	protected bool isWandering = true;
	protected bool isPickedUp = false;
    
	// Use this for initialization
	protected void Initialize()
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
		interactableObject = gameObject.GetComponent<VRTK_InteractableObject>();
		interactableObject.InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
		interactableObject.InteractableObjectUngrabbed += new InteractableObjectEventHandler(ObjectReleased);
	}

	protected void UpdateWanderPath()
	{ 
		if (!agent.hasPath && !isPickedUp)
			SetWanderingPoint(targetNumber);
		else if (agent.hasPath && Vector3.Distance(gameObject.transform.position, targets[targetNumber].transform.position) <= agent.stoppingDistance && !isPickedUp)
			SetWanderingPoint(targetNumber);
	}

	internal void SetWanderingPoint(int pointNumber)
	{
		if (pointNumber < targets.Length)
		{
            if (targets[pointNumber] != null)
            {
                var pos = targets[pointNumber].transform.position;
                agent.SetDestination(new Vector3(pos.x, transform.position.y, pos.z));
                targetNumber = pointNumber == (targets.Length - 1) ? 0 : targetNumber + 1;
            }
		}
       
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
	{
		//Object picked up, set velocity to zero. updatePosition and rotation required to actually stop unit
		isPickedUp = true;
		agent.velocity = Vector3.zero;
		agent.updatePosition = false;
		agent.updateRotation = false;
		agent.Stop();
	}

	private void ObjectReleased(object sender, InteractableObjectEventArgs e)
	{
		//release unit and start on path if still valid. Don't reset position and rotation here, will fire before unit has completely stopped moving
		isPickedUp = false;
		//agent.updatePosition = true;
		//agent.updateRotation = true;
		agent.Resume();
	}

}
