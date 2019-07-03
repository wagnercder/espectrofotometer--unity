using UnityEngine;
using System.Collections;

public class LabInitialFollow : MonoBehaviour {

	private Transform myPosition;
	public Transform[] ways;
	private bool readyToGo;
	private bool passed;
	private float speed = 1.6f;
	private int i = 0;
	private PathFollow menuPath;
	private MenuAnimator menuAnim;

	// Use this for initialization
	void Start () {
		menuAnim = GetComponent<MenuAnimator> ();
		menuPath = GetComponent<PathFollow> ();
		myPosition = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		/*if(readyToGo){
			if (!menuPath.initialPathFinish && i < 4) {
				if ((transform.position - ways [i].transform.position).sqrMagnitude > 2f) {
					Debug.Log ((transform.position - ways [i].transform.position).sqrMagnitude);
					transform.position = Vector3.Lerp (transform.position, ways [i].position, speed * Time.deltaTime);
					transform.rotation = Quaternion.Lerp (transform.rotation, ways [i].rotation, speed * Time.deltaTime);
					passed = true;
				} else {
					i++;
					if (i == 4) {
						menuPath.initialPathFinish = true;
						menuPath.curPos = 0;
						menuAnim.showMenu = true;
					}
				}
			} 
		}*/
	}

	public void startPath(){
		this.readyToGo = true;
	}
}
