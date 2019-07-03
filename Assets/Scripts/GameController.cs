using UnityEngine;
using System.Collections;

public enum GameState {
	Wardrobe, Glasses, Substances, Preparation, Espectrophotometer,none
}

public class GameController : MonoBehaviour {

	GameState currentState;
	public GameState auxState;
	MenuAnimator animator;
	ItemController itemController;
	PathFollow pathFollow;
	Preparation prepareObject;
	Espectrofotometro especObject;

	int onlyOne = 0;

	void Start(){
		animator = GameObject.Find ("Camera").GetComponent<MenuAnimator> ();
		itemController = GameObject.Find("Canvas").GetComponent<ItemController>();
		pathFollow = GameObject.Find ("Camera").GetComponent<PathFollow> ();
		prepareObject = GameObject.Find ("GameController").GetComponent<Preparation> ();
		especObject = GameObject.Find ("espectrofotometro").GetComponent<Espectrofotometro> ();

		currentState = GameState.none;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (auxState);

		if(currentState == GameState.Preparation){ //Estado de escolha de substâncias
			nextState ();
		}

		if(currentState == GameState.Wardrobe){ //Estado Correspondente ao armário de roupas
			animator.animClothButton (1);
			animator.animGlassesButton (1);
			animator.animMaskButton (1);
			itemController.addSlots (3);
			itemController.slotAnim (1);
			currentState = GameState.none;
		}

		if(currentState == GameState.Substances){ //Estado de escolha de substâncias
			Debug.LogWarning ("Estado: Escolha de substancias");
			currentState = GameState.none;
		}

		if(currentState == GameState.Glasses){ //Estado de escolha de substâncias
			Debug.LogWarning ("Estado: Vidraria");
			GameObject.Find ("Canvas").GetComponent<ItemController> ().addSlotsGlasses(3); //Adiciona os slots ao painel de vidradias
			currentState = GameState.none;
		}
	}

	public void setStateToWardrobe(){
		auxState = currentState = GameState.Wardrobe;
	}

	public void setStateToGlasses(){
		auxState = currentState = GameState.Glasses;
	}

	public void setStateToSubstances(){
		auxState = currentState = GameState.Substances;
	}

	public void setStateToPrepare(){
		auxState = currentState = GameState.Preparation;
	}

	public void setStateToEspec(){
		auxState = currentState = GameState.Espectrophotometer;
	}

	public void nextState(){ //Se o estado for X, mover para o próximo estado selecionado
		if(auxState == GameState.Espectrophotometer){
			if (especObject.currentState == EspecStates.step0) { //Adicionar amostra no becker
				especObject.setImageDialog(0);
				especObject.animTextEspecDialog(1); 
				especObject.bt_next.SetActive (true);
			}

			if (especObject.currentState == EspecStates.step1) { //Adicionar amostra no becker
				especObject.setImageDialog(1);
				especObject.bt_insertAlcool.SetActive (true);
			}

			if (especObject.currentState == EspecStates.step2) { //Pisseta no Becker
				especObject.setImageDialog(2);
				especObject.bt_moveToEspec.SetActive (true);
			}

			if (especObject.currentState == EspecStates.step3) { //Homogenizar solucao no becker
				especObject.setImageDialog(4);
				especObject.bt_removeCubeta.SetActive (true);
			}

			if (especObject.currentState == EspecStates.step4) { //Funil no balão
				especObject.setImageDialog(6);
				especObject.removeButtonsEspec ();
				especObject.bt_moveToEspec.SetActive (true);
			}
		}

		if(auxState == GameState.Preparation){
			if (prepareObject.prepare == PrepareState.step0 && prepareObject.flag) { //Adicionar amostra no becker
				prepareObject.animTextPreparation(1);
				prepareObject.bt_AddAmostra.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step1 && prepareObject.flag) { //Pisseta no Becker
				prepareObject.bt_Alcool.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step2 && prepareObject.flag) { //Homogenizar solucao no becker
				prepareObject.bt_HomogenizeBecker.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step3 && prepareObject.flag) { //Funil no balão
				prepareObject.bt_Funil.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step4 && prepareObject.flag) { // Becker no balão
				prepareObject.bt_BeckerBaloon.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step5 && prepareObject.flag) { //Pisseta no balão
				prepareObject.bt_PissetaBallon.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step6 && prepareObject.flag) { // Becker no balão
				prepareObject.bt_funilOut.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step7 && prepareObject.flag) { // Becker no balão
				prepareObject.bt_coverBaloon.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step8 && prepareObject.flag) { // Becker no balão
				if(onlyOne == 0){
					prepareObject.bt_Homogenize.SetActive (true);
					onlyOne++;
				}
			}

			if (prepareObject.prepare == PrepareState.step9 && prepareObject.flag) { // Becker no balão
				prepareObject.bt_removeCover.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.step10 && prepareObject.flag) { // Becker no balão
				prepareObject.bt_baloonToBecker.SetActive (true);
			}

			if (prepareObject.prepare == PrepareState.final && prepareObject.flag) { //Homogenizar
				setStateToEspec();
				pathFollow.moveToEspec();
				prepareObject.flag = false;
			}
		}

		if(auxState == GameState.Glasses){
			animator.animLeftGlasses (-1); //Retira o botão de left glasses
			animator.animRightGlasses (-1); //Retira o botão de right glasses
			pathFollow.moveToCubeta (); //Vai para mesa do espectrômetro
			animator.animTopTitle_Vidraria (-1);
			animator.animTopTitle_Prepar (1);

			setStateToPrepare();
		}

		if(auxState == GameState.Substances){
			pathFollow.moveToGlasses (); // Move para o armário de vidrarias
			setStateToGlasses ();
			animator.animTopTitle_Solucao (-1);
			animator.animTopTitle_Vidraria (1);
		}

		if(auxState == GameState.Wardrobe){
			pathFollow.moveToTable (); // Move para o armário
			setStateToSubstances ();
			animator.animTopTitle_Vestuario (-1);
			animator.animTopTitle_Solucao (1);
		}
	}

	public void restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
}