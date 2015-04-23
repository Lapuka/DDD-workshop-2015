using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPointManager : MonoBehaviour {

	public Transform checkPointObj;
	public Transform player;
	public List<CheckPoint> listOfCheckpoints = new List<CheckPoint>();
	public bool enabled = false;
    int id = 0;
	void Start() {



	}
	void Update() {

		if (Input.GetButtonDown("i")) {

			AddCheckPoint();
		}

		if (Input.GetButtonDown("o")) {

			EnableAll();
		}

		if (Input.GetButtonDown("p")) {
			
			//ResetCheckpoint();
            foreach (CheckPoint item in listOfCheckpoints)
            {
                if (item.isActive == true) Teleport(item);
            }
			
		}


	}
	public void AddCheckPoint() {

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Transform newCheckPoint = Instantiate (checkPointObj, player.position,player.rotation) as Transform;
		CheckPoint ck = newCheckPoint.GetComponent<CheckPoint>();
        ck.id = id;
        id++;
		listOfCheckpoints.Add (ck);

	}
	public void RemoveCheckPoint() {


	}
	

	public void EnableAll() {


			foreach (CheckPoint item in listOfCheckpoints) {
			if (enabled) {

				item.isActive = true;

			}else {

				item.isActive = false;
			}

		}

		enabled = !enabled;
	}

	public void Teleport(CheckPoint cp) {

		player.position = cp.transform.position;

	}
    public void Teleport(int id)
    {
        foreach (CheckPoint item in listOfCheckpoints)
        {
            if (item.id == id) player.position = item.transform.position;
        }
       

    }
}
