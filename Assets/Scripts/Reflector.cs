using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour,IRayHittable {
    [SerializeField] float consumptionAngle;

    public void RespondToCollision(RaycastHit data, Ray ray) {
    	Debug.Log("Angle is: " + Vector3.Angle(-ray.castingDir, data.normal));
        ray.castingDir = Vector3.Reflect(ray.castingDir, data.normal.ResetZ());
    }

}
