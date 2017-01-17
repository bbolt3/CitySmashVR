using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;

public class HumanNavigation : MonoBehaviour {

	public GameObject targetA;
	public GameObject targetB;

	private NavMeshAgent agent;
    private int targetNumber = 0;
    private bool isPickedUp = false;
    private VRTK_InteractableObject interactableObject;

	// Use this for initialization
	private void Start ()
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
        interactableObject = gameObject.GetComponent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
        interactableObject.InteractableObjectUngrabbed += new InteractableObjectEventHandler(ObjectReleased);
        
	}
	
	// Update is called once per frame
	private void Update ()
	{

        if (!agent.hasPath && !isPickedUp)
            SetWanderingPoint(targetNumber);
        else if (agent.hasPath && agent.velocity.sqrMagnitude == 0f && !isPickedUp)
            SetWanderingPoint(targetNumber);
	}

    private void SetWanderingPoint(int pointNumber)
    {
        switch (pointNumber)
        {
            case 0:
                agent.SetDestination(new Vector3(targetA.transform.position.x, transform.position.y, targetA.transform.position.z));
                break;
            case 1:
                agent.SetDestination(new Vector3(targetB.transform.position.x, transform.position.y, targetB.transform.position.z));
                break;
        }

       targetNumber = targetNumber == 0? 1 : 0;
    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        isPickedUp = true;
        agent.velocity = Vector3.zero;
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.Stop();
    }

    private void ObjectReleased(object sender, InteractableObjectEventArgs e)
    {
        isPickedUp = false;
        //agent.updatePosition = true;
        //agent.updateRotation = true;
        agent.Resume();
    } 
}
