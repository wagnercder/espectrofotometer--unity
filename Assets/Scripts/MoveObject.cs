using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {

	public GameObject cubeta;
	public GameObject cubeta_table;
	public Transform glass;
	public Transform cloth;
	public Transform vidraria_1;
	public Transform vidraria_2;
	public Transform vidraria_3;

	public Transform point_of_view_Cubeta;

	private GameObject auxGameObject;
	private Transform auxPointView;

	float speed = 2;

	private bool go{ get; set; }

	// Update is called once per frame
	void Update () {
		if (go) {
			if ((auxGameObject.transform.position - auxPointView.position).sqrMagnitude > 0.1f) {
				auxGameObject.transform.position = Vector3.Lerp (transform.position, auxPointView.position, speed);
				auxGameObject.transform.rotation = Quaternion.Lerp (transform.rotation, auxPointView.rotation, speed);
			} else
				this.go = false;
		}
	}

	public void viewCubeta(bool boolean){
		cubeta.SetActive (boolean);

		if (boolean)
			cubeta_table.SetActive (false);
		else
			cubeta_table.SetActive (true);
	}

	public void viewVBaloon(){
		auxGameObject = GameObject.Find ("VBaloon");
		auxPointView = GameObject.Find ("point_view_9").transform;
		go = true;
	}

	public void viewPisseta(){
		auxGameObject = GameObject.Find ("Pisseta");
		auxPointView = GameObject.Find ("point_view_10_Pisseta").transform;
		go = true;
	}

	public void viewBecker(){
		auxGameObject = GameObject.Find ("Becker");
		auxPointView = GameObject.Find ("point_view_10_Becker").transform;
		go = true;
	}


}
