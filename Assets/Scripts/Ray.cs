using UnityEngine;
using System.Collections.Generic;

public class Ray : MonoBehaviour {
	[SerializeField]Camera camera;
	[SerializeField]int resolution;
	[SerializeField]float minReflAffectDist;
	[SerializeField]float initialVelocity;
	[SerializeField]float dampingVelocity;
	[SerializeField]LineRenderer lineRenderer;
	public float currentVelocity;
	public Vector3 lastPoint;
	public Vector3 castingDir; //normalized
	List<Vector3> path = new List<Vector3>(); //add 1 field to store power;
	bool isCasting;

	void Awake(){
		lineRenderer.gameObject.GetComponent<Renderer>().sortingLayerName = "Ray";
	}

	void BeginCast(){
		currentVelocity = initialVelocity;
		path = new List<Vector3>();
		lastPoint = transform.position.ResetZ();
		castingDir = (camera.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized.ResetZ();
		path.Add(lastPoint.ResetZ(0f));
		isCasting = true;
	}

	void EndCast(){
		isCasting = false;
		path.Clear();
		//start hiding light from source and fade out
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			BeginCast();
		}
		if(Input.GetMouseButtonUp(0)){
			EndCast();
		}
		if(isCasting){
			float delta = Time.deltaTime/resolution;
			for(int i = 0;i<resolution; i++){
				CheckCollision(delta);
				castingDir = (Level.GetFieldsPointAffect(lastPoint)*delta + castingDir.normalized.ResetZ()*currentVelocity); //Vv = V * A * t + v0*V
				lastPoint += castingDir*delta; //S=v*t
				currentVelocity-=dampingVelocity*delta; //Damping v = v0-Ademp*dt
				path.Add(lastPoint);
				if(currentVelocity<0f){
					EndCast();
					break;
				}
			}
		}
	}

	void LateUpdate(){
		if(isCasting){
			Render();
		}
	}

	void CheckCollision(float delta){
		foreach(var o in Physics.RaycastAll(lastPoint, castingDir,currentVelocity*delta) ){
			var responder = o.collider.GetComponent<IRayHittable>();
			if(responder!=null){
				path.Add(o.point);
				lastPoint = o.point;
				responder.RespondToCollision(o,this);
				return;
			}
		}
	}

	void Render(){
		lineRenderer.SetVertexCount(path.Count);
		for(int i = 0; i<path.Count; i++){
			lineRenderer.SetPosition(i, path[i].ResetZ(1f)); //Line renderer sorting bug
		}
	}
}
