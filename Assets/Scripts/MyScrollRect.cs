using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyScrollRect : MonoBehaviour {
	public RectTransform panel;		//to hold the scroll panel
	public Button[] buts;		
	public RectTransform center;	//center to compare the distance for each button

	public float[] distance; 	//All button distance for the center
	public float[] distanceReposition; //
	private bool dragging = false; 	// Will be true, while we drag the panel
	private int bttnDistance;	//will hold the distance between the buttons
	private int minButtonNum;	//To hold the number of the button, with smallest distance to center

	int bttnLenght;

	// Use this for initialization
	void Start () {
		bttnLenght = buts.Length;
		distance = new float[bttnLenght];
		distanceReposition = new float[bttnLenght];

		///Get diatance between the buttons
		bttnDistance = (int)Mathf.Abs(buts[1].GetComponent<RectTransform>().anchoredPosition.x - 
			buts[0].GetComponent<RectTransform>().anchoredPosition.x);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < buts.Length; i++) {
			distanceReposition[i] = center.GetComponent<RectTransform> ().position.x - 
				buts [i].GetComponent<RectTransform> ().position.x;
			distance [i] = Mathf.Abs (distanceReposition[i]);

			if (distanceReposition [i] > 1000) {
				float curX = buts [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY = buts [i].GetComponent<RectTransform> ().anchoredPosition.y;

				Vector2 newAnchoredPos = new Vector2 (curX + (bttnLenght * bttnDistance),curY);
				buts [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos;
			}

			if (distanceReposition [i] < -1000) {
				float curX = buts [i].GetComponent<RectTransform> ().anchoredPosition.x;
				float curY = buts [i].GetComponent<RectTransform> ().anchoredPosition.y;

				Vector2 newAnchoredPos = new Vector2 (curX - (bttnLenght * bttnDistance),curY);
				buts [i].GetComponent<RectTransform> ().anchoredPosition = newAnchoredPos;
			}
		}

		float minDistance = Mathf.Min (distance);

		for(int a=0;a<buts.Length;a++){
			if (minDistance == distance [a]) {
				minButtonNum = a;
			}
		}

		if (!dragging) {
			//LerpToBttn (minButtonNum * -bttnDistance);
			LerpToBttn(-buts[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
		}
	}

	void LerpToBttn(float position){
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;
	}

	public void startDrag(){
		this.dragging = true;
	}

	public void endDrag(){
		this.dragging = false;
	}
}
