using UnityEngine;
using System.Collections;

public class Admin : MonoBehaviour {

	public Transform adminPanel;
	bool isAdmin = false;

	void Update() {

		if (Input.GetButtonDown ("Admin"))
			toogleAdminPanel ();
	}

	void toogleAdminPanel() {

		if (adminPanel.gameObject.activeSelf)
			adminPanel.gameObject.SetActive (false);
		else
			adminPanel.gameObject.SetActive (true);

	}
}
