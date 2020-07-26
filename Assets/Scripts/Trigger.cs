using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour, IRayHittable {
    [SerializeField] bool single;
    bool triggered;

    public void RespondToCollision(RaycastHit data, Ray ray) {
        ray.castingDir = Vector3.zero;
    	// ray.currentVelocity = 0f;
    	TriggerEvent();
    }

    public void TriggerEvent(){
    	if(triggered && single){
    		return;
    	}else{
    		triggered = true;
            GetComponent<Animator>().SetTrigger("Triggered");
    		Debug.Log("Trigger");
    	}
    }


}
