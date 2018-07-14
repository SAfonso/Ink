using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoin : MonoBehaviour {

	private bool coinChecked = false;

    void Update () {
		if (GameManager.instance.invincible){
			SetBonus();
		}else{
			coinChecked = false;
		}
	}

	public void SetBonus(){
		if (!coinChecked){
			this.transform.GetChild(0).gameObject.SetActive(true);
			coinChecked = true;
		}
	}
}
