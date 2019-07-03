using UnityEngine;
using System.Collections;

public class PathFollow : MonoBehaviour {

	public Transform[] followObjects;
	public float speed = 2;
	public float reachDist = 2f;
	public int curPos{get; set;}
	public int interfaceCurPos{ get; set;}
	public Rect right;
	private bool passed = false;

	public bool initialPath{ get; set;} // Caso o percurso inicial da Classe LabInitialFollow tenha terminado, será verdadeiro
	public bool pathToTable{ get; set;}
	public bool pathToGlasses{ get; set;}
	public bool rightGlasses{ get; set;}
	public bool leftGlasses{ get; set;}
	public bool glassToTable{ get; set;}

	public bool pathToEspec{ get; set;}
	public bool pathToCubeta{ get; set;}

	public bool animationButtonControl{ get; set;} // Precisei criar essa variável pois preciso que determinadas animacões 
	//sejam criadas somente uma vez após chegar no local determinado

	public bool pathHomogenize{ get; set;}
	public bool pathCubetaView{ get; set;}
	public bool pathToSeeEspec{ get; set;}

	public GameObject scroll;

	// Update is called once per frame
	void Update () {
		if(initialPath){
			if ((GetComponent<Camera>().transform.position - followObjects [2].transform.position).sqrMagnitude > 0.1f && !passed) {
				transform.position = Vector3.Lerp (transform.position, followObjects [2].position, speed);
				transform.rotation = Quaternion.Lerp (transform.rotation, followObjects [2].rotation, speed);
			} else {
				passed = true;
				transform.position = Vector3.Lerp (transform.position, followObjects [curPos].position, speed);
				transform.rotation = Quaternion.Lerp (transform.rotation, followObjects [curPos].rotation, speed);
			}

			if (pathToTable) {
				if((GetComponent<Camera>().transform.position - followObjects [1].transform.position).sqrMagnitude < 0.1f){
					this.curPos = 3;
				}

				if ((GetComponent<Camera> ().transform.position - followObjects [3].transform.position).sqrMagnitude < 0.1f) {
					this.curPos = 4;
				}

				if ((GetComponent<Camera> ().transform.position - followObjects [4].transform.position).sqrMagnitude < 0.1f) {
					pathToTable = false;

					if (GameObject.Find ("GameController").GetComponent<GameController> ().auxState == GameState.Substances) {
						scroll.SetActive (true);
					}

				}
			}

			if (pathToGlasses) {
				if((GetComponent<Camera>().transform.position - followObjects [7].transform.position).sqrMagnitude < 0.1f){
					pathToGlasses = false;

					if (GameObject.Find ("GameController").GetComponent<GameController> ().auxState == GameState.Glasses && !animationButtonControl) {
						GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animVidrariaButton_1 (1);
						GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animVidrariaSlots (1);
						GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animRightGlasses (1);
						GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animDoorWardrobe1 (1);
						Debug.Log ("Animacoes glasses");
						animationButtonControl = true;
					}
				}
			}

			if(glassToTable){
				if ((GetComponent<Camera> ().transform.position - followObjects [4].transform.position).sqrMagnitude < 0.1f) {
					pathToTable = false;
				}
			}

			if(pathToCubeta){
				if ((GetComponent<Camera> ().transform.position - followObjects [10].transform.position).sqrMagnitude < 0.1f) {
					pathToCubeta = false;
				}
			}

			if(pathToEspec){
				if ((GetComponent<Camera> ().transform.position - followObjects [9].transform.position).sqrMagnitude < 0.1f) {
					pathToEspec = false;
				}
			}

			if(pathHomogenize){
				if ((GetComponent<Camera> ().transform.position - followObjects [11].transform.position).sqrMagnitude < 0.1f) {
					pathToEspec = false;
				}
			}

			if(pathToSeeEspec){
				if ((GetComponent<Camera> ().transform.position - followObjects [4].transform.position).sqrMagnitude < 0.1f) {
					pathToSeeEspec = false;
				}
			}
		}
	}


	public void moveToCenter(){
		//this.curPos = 0;
		this.passed = false;
	}

	public void moveToRight(){
		this.passed = false;
	}

	public void moveToCubeta(){
		this.curPos = 10;
		this.pathToCubeta = true;
	}

	public void moveToEspec(){
		this.curPos = 9;
		this.pathToEspec = true;
	}

	public void moveToWardRobe(){
		StartCoroutine (readyToGo ());
	}

	public void moveToTable(){
		this.curPos = 1;
		pathToTable = true;
	}

	public void moveToGlasses(){
		this.curPos = 7;
		pathToGlasses = true;
	}

	public void moveToGlasses_left(){
		this.curPos = 7;
		pathToGlasses = true;
	}

	public void moveToGlasses_right(){
		this.curPos = 8;
		pathToGlasses = true;
	}

	public void moveFromGlassesToTable(){
		this.curPos = 5;
		glassToTable = true;
	}

	public void moveToHomogenize(){
		this.curPos = 11;
		pathHomogenize = true;
	}

	public void moveToSeeEspec(){
		this.curPos = 4;
		pathToSeeEspec = true;
	}
		

	IEnumerator readyToGo(){ //Esse método apenas faz com que a câmera espere animaćão da porta terminar (até que a porta abra totalmente)
		yield return new WaitForSeconds (1);
		initialPath = true;
		this.curPos = 2;
		GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animIniciar ();
		GameObject.Find ("GameController").GetComponent<GameController> ().setStateToWardrobe ();
		GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animTopTitle_Vestuario (1);

		yield break;
	}

	public void removeScroll(){
		scroll.SetActive (false);
	}
}
