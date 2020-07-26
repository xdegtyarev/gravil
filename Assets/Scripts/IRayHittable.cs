using UnityEngine;
using System.Collections;

public interface IRayHittable{
	void RespondToCollision(RaycastHit data, Ray ray);
}
