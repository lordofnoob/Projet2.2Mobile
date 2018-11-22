using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public AnimationCurve anim;
	// Use this for initialization
	void Start () {
        float flechesize = anim.Evaluate(0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
