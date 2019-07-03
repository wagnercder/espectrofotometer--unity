using UnityEngine;
using System.Collections;

public class WaitClass : MonoBehaviour {

	public GameObject redLight;

	public bool waitASecond(Espectrofotometro espec){
		StartCoroutine (wait (espec));
		return true;
	}

	public bool waitToReset(Espectrofotometro espec){
		StartCoroutine (waitReset (espec));
		return true;
	}

	IEnumerator wait(Espectrofotometro espec){
		yield return new WaitForSeconds (2);
		redLight.SetActive (true);
		yield return new WaitForSeconds (1.1f);
		redLight.SetActive (false);
		yield return new WaitForSeconds (1.1f);
		redLight.SetActive (true);
		yield return new WaitForSeconds (1.1f);
		redLight.SetActive (false);

		espec.calcAbs ();

		yield return new WaitForSeconds (10);

		espec.setImageDialog (9);
		espec.bt_restart.SetActive (true);

		yield break;
	}

	IEnumerator waitReset(Espectrofotometro espec){
		yield return new WaitForSeconds (2);
		redLight.SetActive (true);
		yield return new WaitForSeconds (1.1f);
		redLight.SetActive (false);
		yield return new WaitForSeconds (1.1f);
		redLight.SetActive (true);
		yield return new WaitForSeconds (1.1f);
		redLight.SetActive (false);

		espec.resetEspec ();
		espec.setState (3);
		GameObject.Find ("GameController").GetComponent<GameController> ().nextState ();
		yield break;
	}
}

