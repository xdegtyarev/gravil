using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
	[SerializeField] SpriteRenderer[] animatedCircle;
	[SerializeField] Transform[] animatedCircleTransform;
	[SerializeField] AnimationCurve distanceImpulseGraph;
	[SerializeField] Transform radiusView;
	[SerializeField] float gravity;
	[SerializeField] float radius;
	[SerializeField] float animationSpeed;
	[SerializeField] float animationProgress;

	void Awake(){
		for (int i = 0; i < animatedCircleTransform.Length; i++) {
            animatedCircleTransform[i].localScale = Vector3.one * Mathf.Lerp(1f, radius*2f, ((float)i)/animatedCircleTransform.Length);
        }
        radiusView.localScale = Vector3.one*(transform.localScale.x+radius*2f);
        Level.RegisterField(this);
	}

	void Update(){
		animationProgress+=Time.deltaTime*animationSpeed;
		if(animationProgress>=1f){
			animationProgress-=1f;
		}

		for (int i = 0; i < animatedCircleTransform.Length; i++) {
			var eval = distanceImpulseGraph.Evaluate(animatedCircleTransform[i].localScale.x/radius*0.5f) + 0.1f;
			if(animatedCircleTransform[i].localScale.x <= 1f){
				animatedCircleTransform[i].localScale = Vector3.one * radius*2f;
			}else{
				animatedCircleTransform[i].localScale -= Vector3.one * eval * Time.deltaTime;
			}
		}
	}

	public bool AffectsPoint(Vector3 point){
		return Vector3.Distance(transform.position, point)<(radius+transform.localScale.x*0.5f);
	}

	public Vector3 AffectInPoint(Vector3 point){
		var distance = Vector3.Distance(transform.position, point);
		var dir = (transform.position-point).normalized;
		return dir*distanceImpulseGraph.Evaluate((distance-transform.localScale.x*0.5f)/radius)*gravity;
	}

}
