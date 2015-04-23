using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyTextures : MonoBehaviour {
	public Transform building;
	List<MeshRenderer> meshList = new List<MeshRenderer>();
	public List<MaterialSet> listOfMaterials = new List<MaterialSet>();
	void Start () {
	
		foreach (MeshRenderer item in building.GetComponentsInChildren<MeshRenderer> ()) {
			meshList.Add (item);
		} 

		foreach (MeshRenderer item in meshList) {

			foreach (MaterialSet mat in listOfMaterials) {
				string name = item.name.ToLower();
				string key = mat.keyWord;
				if(name.IndexOf(key) >=0)ApplyMaterial(mat.material, item);
			}

		}

	}
	
	public void ApplyMaterial(Material mat, MeshRenderer mesh) {

		mesh.material = mat;

	}
}
