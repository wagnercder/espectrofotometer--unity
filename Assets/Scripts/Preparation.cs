using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum PrepareState {
	step0,step1,step2,step3,step4,step5,step6,step7,step8,step9,step10,final
}

public class Preparation : MonoBehaviour {

	public GameObject dialogBox;
	public Image textDialog;
	public GameObject solid;
	public GameObject cover;
	private List<Sprite> listDialog = new List<Sprite>(15);
	private List<float> listTime = new List<float> (10);
	private int dialogIndex = 0;
	public PrepareState prepare{ get; set;}

	public GameObject bt_AddAmostra;
	public GameObject bt_HomogenizeBecker;
	public GameObject bt_Alcool;
	public GameObject bt_Funil;
	public GameObject bt_BeckerBaloon;
	public GameObject bt_PissetaBallon;
	public GameObject bt_funilOut;
	public GameObject bt_coverBaloon;
	public GameObject bt_Homogenize;
	public GameObject bt_removeCover;
	public GameObject bt_baloonToBecker;

	public bool flag;

	// Use this for initialization
	void Start () {
		listDialog.Add (Resources.Load<Sprite> ("1_prep"));
		listDialog.Add (Resources.Load<Sprite> ("2_prep"));
		listDialog.Add (Resources.Load<Sprite> ("3_prep"));
		listDialog.Add (Resources.Load<Sprite> ("4_prep"));
		listDialog.Add (Resources.Load<Sprite> ("5_prep"));

		textDialog.sprite = listDialog [0];

		bt_AddAmostra.SetActive (false);
		bt_HomogenizeBecker.SetActive (false);
		bt_Alcool.SetActive (false);
		bt_Funil.SetActive (false);
		bt_BeckerBaloon.SetActive (false);
		bt_PissetaBallon.SetActive (false);
		bt_funilOut.SetActive (false);
		bt_coverBaloon.SetActive (false);
		bt_Homogenize.SetActive (false);
		bt_removeCover.SetActive (false);
		bt_baloonToBecker.SetActive (false);

		prepare = PrepareState.step0;    
		flag = true;
	}

	public void nextDialog(){
		dialogIndex++;
		if(dialogIndex < listDialog.Count)
			textDialog.sprite = listDialog [dialogIndex];
	}

	public void addSolidToSolve(){
		solid.SetActive (true);
		prepare = PrepareState.step1;
	}

	public void solveSolutionAnim(int speed){
		GameObject.Find ("Pisseta_table").GetComponent<Animator> ().SetFloat ("speed",speed);
		prepare = PrepareState.step2;
		flag = true;
	}

	public void homogenizeBeckerAnim(int speed){
		GameObject.Find ("becker_model").GetComponent<Animator> ().SetFloat ("speed_rotate",speed);
		prepare = PrepareState.step3;
		flag = true;
	}

	public void funilAnim(int speed){
		GameObject.Find ("funnel_glass").GetComponent<Animator> ().SetFloat ("speed",speed);
		prepare = PrepareState.step4;
		flag = true;
	}

	public void beckerToBaloonAnim(int speed){
		GameObject.Find ("becker_model").GetComponent<Animator> ().SetFloat ("speed",speed);
		prepare = PrepareState.step5;
		flag = true;
	}

	public void pissetaToBaloonAnim(int speed){
		GameObject.Find ("Pisseta_table").GetComponent<Animator> ().SetFloat ("speed2",speed);
		prepare = PrepareState.step6;
		flag = true;
	}

	public void addCoverToBaloon(){
		cover.SetActive (true);
		prepare = PrepareState.step8;
	}

	public void homogenezarAnim(int speed){
		GameObject.Find ("BalaoVol").GetComponent<Animator> ().SetFloat ("speed",speed);
		StartCoroutine (timeToFinishAnimRotateBalvol());
		flag = true;
	}

	public void removeCoverFromBaloon(){
		cover.SetActive (false);
		prepare = PrepareState.step10;
	}

	public void fillBeckerAnim(int speed){
		GameObject.Find ("BalaoVol").GetComponent<Animator> ().SetFloat ("speed",speed);
		StartCoroutine (timeToFinish());
		flag = true;
	}

	public void animTextPreparation(int speed){
		GameObject.Find("Dialog_Box_Preparation").GetComponent<Animator>().SetFloat ("speed2", speed);
	}

	public void setStepTo7(){
		prepare = PrepareState.step7;
	}

	IEnumerator timeToFinishAnimRotateBalvol(){
		yield return new WaitForSeconds(5);
		prepare = PrepareState.step9;
		GameObject.Find ("Camera").GetComponent<PathFollow> ().moveToCubeta ();
		yield break;
	}

	IEnumerator timeToFinish(){
		yield return new WaitForSeconds(5);
		GameObject auxObject = GameObject.Find ("Dialog_Box_Preparation") as GameObject;
		GameObject.Find ("Camera").GetComponent<MenuAnimator> ().animCubetaButton (1);
		auxObject.SetActive (false);
		flag = false;
		removeAllButtons ();
		yield break;
	}

	public void removeAllButtons(){
		bt_AddAmostra.SetActive (false);
		bt_HomogenizeBecker.SetActive (false);
		bt_Alcool.SetActive (false);
		bt_Funil.SetActive (false);
		bt_BeckerBaloon.SetActive (false);
		bt_PissetaBallon.SetActive (false);
		bt_funilOut.SetActive (false);
		bt_coverBaloon.SetActive (false);
		bt_Homogenize.SetActive (false);
		bt_removeCover.SetActive (false);
		bt_baloonToBecker.SetActive (false);
	}
}
