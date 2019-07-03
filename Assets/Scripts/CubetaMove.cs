using UnityEngine;
using System.Collections;

public class CubetaMove : MonoBehaviour {

	private int cubetaPos;
	public Transform window;
	public Transform table;
	private Transform cubeta;
	private Transform cubetaFather;
	public float speed;

	void Start(){
		cubeta = GameObject.Find ("Cubeta").GetComponent<Transform> ();
		cubetaFather = GameObject.Find ("CubetaFather").GetComponent<Transform> (); 
	}

	// Update is called once per frame
	void Update () {    
		if (cubetaPos == 1 && (cubeta.position - window.position).sqrMagnitude > 0.1f) {
			cubeta.transform.position = Vector3.Lerp (cubeta.transform.position, window.transform.position, speed);
			cubeta.transform.rotation = Quaternion.Lerp (cubeta.transform.rotation, window.transform.rotation, speed);
		} else if(cubetaPos ==1) {
			//cubetaFather.Rotate (0,20 * Time.deltaTime, 0);
			//cubetaFather.rotation = new Vector3(0,0,0);
		}

		if (cubetaPos == 2) {
			cubeta.transform.position = Vector3.Lerp (cubeta.transform.position,table.transform.position,speed);
			cubeta.transform.rotation = Quaternion.Lerp (cubeta.transform.rotation, table.transform.rotation, speed);
		}
	}

	public void moveToWindow(){
		this.cubetaPos = 1;
	}

	public void moveToTable(){
		this.cubetaPos = 2;
	}
}
