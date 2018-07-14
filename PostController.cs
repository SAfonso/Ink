using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostController : MonoBehaviour {

    private float posY;
	private TextMeshPro textPost;
	public bool startMove = false;

    [Range(0f, 100f)] public float offsetY;

    private float initialPosition;

	// Use this for initialization
	void Start () {
		posY = transform.position.y;
		textPost = this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        initialPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeLevelText();
        if (!GameManager.instance.gameOver && startMove) {
            posY -= Time.deltaTime * GameManager.instance.globalSpeed + offsetY;
            this.transform.position = new Vector3(0, posY, 0);
			if (this.transform.position.y <= -10){
                posY = initialPosition;    // devolvemos a la pos original
                this.transform.position = new Vector3(0, posY, 0);
                startMove = false;
			}

		}
	}

	public void ChangeLevelText(){
		textPost.text = "LEVEL " + GameManager.instance.level.ToString();
	}
}
