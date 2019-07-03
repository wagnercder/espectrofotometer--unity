using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuAnimator : MonoBehaviour {

	PathFollow pathFollow;
	Espectrofotometro espec;
	LabInitialFollow initialFollow;
	CubetaMove cubetaPath;
	ItemController itemController;

	private Animator animStart;
	private string action = "first";
	private string flagPanel {get; set;}
	public bool showMenu{get; set;} //Aciona o menu somente quando a câmera chegar no seu devido lugar

	void Start () {
		//chamadas iniciais scripts
		itemController = GameObject.Find("Canvas").GetComponent<ItemController>();
		animStart = GameObject.Find("Iniciar").GetComponent<Animator>();
		pathFollow = GetComponent<PathFollow> ();
		initialFollow = GetComponent<LabInitialFollow> ();
		espec = GameObject.Find ("espectrofotometro").GetComponent<Espectrofotometro> ();
		cubetaPath = GameObject.Find("Cubeta").GetComponent<CubetaMove> ();
	}

	void Update(){
		Debug.Log ("Objetos na lista: "+itemController.getItemsFromGlasses().Count);
	}

	public void animIniciar(){
		Debug.Log ("iniciou a animaćão");
		animStart.SetFloat ("speed", 1);
	}

	public void animGlassesButton(int speed){
		GameObject.Find("Button_oculos").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animClothButton(int speed){
		GameObject.Find("Button_clothes").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animMaskButton(int speed){
		GameObject.Find("Button_mask").GetComponent<Animator>().SetFloat ("speed", speed);
	}
	 
	public void animScrollButton(int speed){
		GameObject.Find("ScrollPanel").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animVidrariaButton_1(int speed){ // Verifica se o item já não foi pego, e caso já, não continua animando o botão
		List<string> listItems = itemController.getItemsFromGlasses ();
		bool have = false; 

		for (int i = 0; i < listItems.Count; i++)
			if (listItems [i] == "balao_vol")
				have = true;

		if(!have || speed == -1)
			GameObject.Find("Button_VBaloon").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animVidrariaButton_2(int speed){ // Verifica se o item já não foi pego, e caso já, não continua animando o botão
		List<string> listItems = itemController.getItemsFromGlasses ();
		bool have = false; 

		for (int i = 0; i < listItems.Count; i++)
			if (listItems [i] == "Pisseta")
				have = true;

		if(!have || speed == -1)
			GameObject.Find("Button_Pisseta").GetComponent<Animator>().SetFloat ("speed", speed);		
	}

	public void animVidrariaButton_3(int speed){ // Verifica se o item já não foi pego, e caso já, não continue animando o botão
		List<string> listItems = itemController.getItemsFromGlasses ();
		bool have = false; 

		for (int i = 0; i < listItems.Count; i++) 
			if (listItems [i] == "Becker")
				have = true;

		if(!have || speed == -1)
			GameObject.Find("Button_Becker").GetComponent<Animator>().SetFloat ("speed", speed);		
	}

	public void animVidrariaSlots(int speed){
		if(itemController.amountItemsGlasses != 3)
			GameObject.Find("painel vidrarias lab").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animRightGlasses(int speed){
		if (itemController.getItemsFromGlasses().Count<3 || speed == -1)
			GameObject.Find("Button_right").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animLeftGlasses(int speed){
		if (itemController.getItemsFromGlasses ().Count < 3 || speed == -1) {
			GameObject.Find ("Button_left").GetComponent<Animator> ().SetFloat ("speed", speed);
		}
	}

	public void animCubetaButton(int speed){
		GameObject.Find("Button_Cubeta").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animButtonNext(int speed){
		GameObject.Find("Button_next").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	//Animacões text dialog 
	public void animTextCubeta(int speed){
		GameObject.Find("Dialog_Box_Cubeta").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTextPisseta(int speed){
		GameObject.Find("Dialog_Box_Pisseta").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTextBecker(int speed){
		GameObject.Find("Dialog_Box_Becker").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTextBVolumetric(int speed){
		GameObject.Find("Dialog_Box_VolumetricBaloon").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	//Animacão 3D porta armários
	public void animDoorWardrobe1(int speed){
		GameObject.Find("cabdoor1").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animDoorWardrobe2(int speed){
		GameObject.Find("cabdoor2").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	//Animacão Door lab
	public void animDoorLab(int speed){
		GameObject.Find("Door").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	//Animacão dos títulos de etapas
	public void animTopTitle_Vidraria(int speed){
		GameObject.Find("top_title_vidraria").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTopTitle_Vestuario(int speed){
		GameObject.Find("top_title_vestuario").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTopTitle_Prepar(int speed){
		GameObject.Find("top_title_prepar").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTopTitle_Solucao(int speed){
		GameObject.Find("top_title_solucao").GetComponent<Animator>().SetFloat ("speed", speed);
	}

	public void animTopTitle_Espec(int speed){
		GameObject.Find("top_title_espec").GetComponent<Animator>().SetFloat ("speed", speed);
	}
}
