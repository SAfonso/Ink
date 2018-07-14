using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour {

    private void OnEnable()
    {
        //GameManager.instance.coins.Add(this.gameObject);
        //this.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void DisableParticleRetarded() {
        StartCoroutine(DisablePart(0.5f));
    }

    private void DisableParticle()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(0).transform.position = this.transform.position;
    }

    //prueba en co-rutina
    private IEnumerator DisablePart(float secs)
    {
        yield return new WaitForSeconds(secs);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(0).transform.position = this.transform.position;
    }

}
