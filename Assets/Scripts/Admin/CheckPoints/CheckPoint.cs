using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]

public class CheckPoint  : MonoBehaviour {

	public bool isActive = false;
    public int id;
  
    void Start() {

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void Update()
    {
        if (isActive) transform.Rotate(Vector3.down * Time.deltaTime * 50);
       
    }
	void OnTriggerEnter(Collider col) {

        if (isActive)
        {
            Trigger();
        } 
	}

	void Trigger() {

		Debug.Log("Trigger");
        isActive = false;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
	}
}
