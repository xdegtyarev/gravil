using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	static List<Field> fields = new List<Field>();

	public static void RegisterField(Field field){
		fields.Add(field);
	}

	public static void DeregisterField(Field field){
		fields.Remove(field);
	}

	public static Vector3 GetFieldsPointAffect(Vector3 point){
		var fieldAffection = new List<Vector3>();
		foreach(var o in fields){
			if(o.AffectsPoint(point)){
				fieldAffection.Add(o.AffectInPoint(point));
			}
		}
		Vector3 sum = Vector3.zero;
		foreach(var o in fieldAffection){
			sum+=o;
		}
		return sum.ResetZ();
	}
}
