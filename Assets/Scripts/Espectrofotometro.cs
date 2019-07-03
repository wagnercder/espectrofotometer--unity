using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum EspecStates{
	step0,step1,step2,step3,step4,step5
}

public class Espectrofotometro : MonoBehaviour {
	public GameObject dialogBoxEspec;
	public Image textDialogEspec;
	private List<Sprite> listDialog = new List<Sprite>(15);
	private int dialogIndex = 0;

	public TextMesh absText;
	public TextMesh traText;

	public TextMesh conText;
	public TextMesh eText;

	private float absorvancia;
	private float transmitancia;

	private float concentracao;
	private float coeficienteAbs = 9250;
	private float caminhoOtico = 1.0f;

	private WaitClass wait;
	private bool accessWaitClass;

	public GameObject bt_next;
	public GameObject bt_insertAlcool;
	public GameObject bt_moveToEspec;
	public GameObject bt_calculoAlcool;
	public GameObject bt_removeCubeta;
	public GameObject bt_addSolution;
	public GameObject bt_calculoEspec;
	public GameObject bt_restart;

	public EspecStates currentState;

	PathFollow pathFollow;

	// Use this for initialization
	void Start () {
		bt_next.SetActive (false);
		bt_insertAlcool.SetActive (false);
		bt_moveToEspec.SetActive (false);
		bt_calculoAlcool.SetActive (false);
		bt_removeCubeta.SetActive (false);
		bt_addSolution.SetActive (false);
		bt_calculoEspec.SetActive (false);
		bt_restart.SetActive (false);

		listDialog.Add (Resources.Load<Sprite> ("espec_text_1"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_2"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_3"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_4"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_5"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_6"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_7"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_8"));
		listDialog.Add (Resources.Load<Sprite> ("espectrofotometro_text"));
		listDialog.Add (Resources.Load<Sprite> ("espec_text_9"));

		wait = GameObject.Find ("espectrofotometro").GetComponent<WaitClass> ();

		absText.text = "Abs: "+absorvancia;
		traText.text = "TR: "+transmitancia;
		conText.text = "C: "+concentracao;
		eText.text = "E: "+coeficienteAbs;

		currentState = EspecStates.step0;
		textDialogEspec.sprite = listDialog [0];

		pathFollow = GameObject.Find ("Camera").GetComponent<PathFollow> ();

	}
	
	// Update is called once per frame
	void Update () {
		absText.text = "Abs: "+absorvancia;
		traText.text = "TR: "+transmitancia+"%";
		conText.text = "C: "+concentracao;
		eText.text = "E: "+coeficienteAbs;
	}

	#region calc espec methods
	public void startCalc(){
		wait.waitASecond (this); //a classe WaitClass é responsável somente por organizar a ordem das luzes seguida da chamada do cálculo
	}

	public void calcAbs(){
		concentracao = Random.Range (0.000010f, (2.99f/coeficienteAbs));	

		Debug.LogWarning ("concentracão: " + concentracao + " absortividade: "+ coeficienteAbs + " caminho ótico: "+caminhoOtico);

		this.absorvancia = concentracao * coeficienteAbs * caminhoOtico;
		this.transmitancia = (Mathf.Pow (10, -absorvancia)) * 100;

		setImageDialog (9);
		Debug.LogWarning (this.absorvancia);
	}

	public void startReset(){
		setImageDialog (3);
		wait.waitToReset (this);
	}

	public void resetEspec(){
		this.absorvancia = 0;
		this.transmitancia = 100;
	}
		
	public void setCoeficienteAbs(float abs){ this.coeficienteAbs = abs;}
	#endregion

	#region animations and actions
	public void animTextEspecDialog(int speed){
		GameObject.Find("Dialog_Box_Espectro").GetComponent<Animator>().SetFloat ("speed2", speed);
	}

	public void cleanCubeta(int speed){
		GameObject.Find ("Pisseta_table").GetComponent<Animator>().SetFloat("speed2",speed);
	}

	public void moveCubetaEspec(int speed){
		pathFollow.moveToSeeEspec ();
		GameObject.Find ("espectrofotometro").GetComponent<Animator>().SetFloat("speed",speed);
		GameObject.Find ("Cubeta").GetComponent<Animator>().SetFloat("speed",speed);
		StartCoroutine (timerEspec(3));
	}

	public void removeCubetaEspec(){
		pathFollow.moveToSeeEspec ();
		GameObject.Find ("espectrofotometro").GetComponent<Animator>().SetFloat("speed",1);
		GameObject.Find ("Cubeta").GetComponent<Animator>().SetFloat("speed",-1);
		StartCoroutine (timerCubeta(3));
	}

	public void moveCubetaEspecFinish(int speed){
		pathFollow.moveToSeeEspec ();
		GameObject.Find ("espectrofotometro").GetComponent<Animator>().SetFloat("speed",speed);
		GameObject.Find ("Cubeta").GetComponent<Animator>().SetFloat("speed",speed);
		StartCoroutine (timerEspec(3));
	}

	public void beckerToCubeta(int speed){
		GameObject.Find ("becker_model").GetComponent<Animator>().SetFloat("speed_rotate",speed);
		setImageDialog (5);
		setState (4);
		StartCoroutine(timerUntilStepFour (4));

	}

	public void setImageDialog(int index){
		textDialogEspec.sprite = listDialog [index];
	}

	public void setState(int step){
		switch (step) {
		case 1:
			currentState = EspecStates.step1;
			break;
		case 2:
			currentState = EspecStates.step2;
			break;
		case 3:
			currentState = EspecStates.step3;
			break;
		case 4:
			currentState = EspecStates.step4;
			break;
		case 5:
			currentState = EspecStates.step5;
			break;
		}
	}

	public void removeButtonsEspec(){
		bt_next.SetActive (false);
		bt_insertAlcool.SetActive (false);
		bt_moveToEspec.SetActive (false);
		bt_calculoAlcool.SetActive (false);
		bt_removeCubeta.SetActive (false);
		bt_addSolution.SetActive (false);
		bt_calculoEspec.SetActive (false);
	}

	public void removeDialog(){
		dialogBoxEspec.SetActive (false);
	}
	#endregion

	IEnumerator timerEspec(float time){
		yield return new WaitForSeconds (time);
		GameObject.Find ("espectrofotometro").GetComponent<Animator>().SetFloat("speed",-1);
		pathFollow.moveToEspec ();

		if (currentState == EspecStates.step4) {
			yield return new WaitForSeconds (8f);
			setImageDialog (7);
			yield return new WaitForSeconds (6f);
			setImageDialog (8);
			yield return new WaitForSeconds (4f);
			bt_calculoEspec.SetActive (true);
		}else
			bt_calculoAlcool.SetActive (true);
		
		yield break;
	}

	IEnumerator timerCubeta(float time){
		yield return new WaitForSeconds (time);
		GameObject.Find ("espectrofotometro").GetComponent<Animator>().SetFloat("speed",-1);
		pathFollow.moveToCubeta();
		bt_addSolution.SetActive (true);
		yield break;
	}

	IEnumerator timerUntilStepFour(float time){
		yield return new WaitForSeconds (time);
		GameObject.Find ("GameController").GetComponent<GameController> ().nextState ();
		yield break;
	}
}
