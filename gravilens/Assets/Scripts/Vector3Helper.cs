using UnityEngine;
using System.Collections;

public static class Vector3Helper {
	public static Vector3 ResetZ(this Vector3 v,float val=0f){
		return new Vector3(v.x,v.y,val);
	}

	public static Vector3 ResetX(this Vector3 v,float val=0f){
		return new Vector3(val,v.y,v.z);
	}

	public static Vector3 ResetY(this Vector3 v,float val=0f){
		return new Vector3(v.x,val,v.z);
	}
}
