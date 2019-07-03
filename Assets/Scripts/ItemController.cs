using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//Classe com os métodos pra adicionar slots e items, onde o usuario pega instrumentos e roupas
public class ItemController : MonoBehaviour {

	public GameObject prefabItems;
	public GameObject prefabSlots;
	public GameObject prefabSlots2;

	//Vestiment a
	private List<string> listItems = new List<string>();
	private List<GameObject> listSlots = new List<GameObject> ();
	private GameObject painelVestimenta;
	private int amountItems;

	//Vidrarias
	private List<string> listItemsGlasses = new List<string>();
	private List<GameObject> listSlotsGlasses = new List<GameObject> ();
	private GameObject painelVidrarias;
	public int amountItemsGlasses { get; set;}

	private MenuAnimator menuAnim;

	void Start(){
		menuAnim = GameObject.Find ("Camera").GetComponent<MenuAnimator>();
		painelVestimenta = GameObject.Find ("painel vestimenta lab");
		painelVidrarias = GameObject.Find ("painel vidrarias lab");
	}

	public void slotAnim(int speed){
		painelVestimenta.GetComponent<Animator> ().SetFloat ("speed",speed);
	}

	public void addSlots(int amount){
		for(int i = 0 ; i < amount ; i++){
			GameObject newSlot = Instantiate (prefabSlots2);
			newSlot.transform.SetParent (painelVestimenta.transform,false);
			listSlots.Add (newSlot);
		}
	}

	public void addSlotsGlasses(int amount){
		for(int i = 0 ; i < amount ; i++){
			GameObject newSlot = Instantiate (prefabSlots2);
			newSlot.transform.SetParent (painelVidrarias.transform,false);
			listSlotsGlasses.Add (newSlot);
		}
	}

	public void addItems(string name){
		listItems.Add (name);
		GameObject newItem = Instantiate (prefabItems);
		newItem.transform.SetParent (listSlots[amountItems].transform,false);
		newItem.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/"+name);
		amountItems++;

		if(amountItems == 3){
			painelVestimenta.GetComponent<Animator> ().SetFloat ("speed",-1);
			menuAnim.animButtonNext (1);
		}
	}

	public void addItemsGlasses(string name){
		listItemsGlasses.Add (name);
		GameObject newItem = Instantiate (prefabItems);
		newItem.transform.SetParent (listSlotsGlasses[amountItemsGlasses].transform,false);
		newItem.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/"+name);
		amountItemsGlasses++;

		if(amountItemsGlasses == 3){
			painelVidrarias.GetComponent<Animator> ().SetFloat ("speed",-1);
			menuAnim.animButtonNext (1);
		}
	}

	public List<string> getItemsFromGlasses(){ //Verifica quais items (vidrarias) já foram escolhidos
		return this.listItemsGlasses;
	}

	public void itemDestroyFromScene(GameObject obj){
		Destroy (obj);
	}
}