using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class ParallaxColorChanger : MonoBehaviour {

    [Range(0, 6)] public int Level;
    private SpriteRenderer Fondo,Bg3,Bg2,Bg1;

    public List<Color> LevelColor = new List<Color>();

    private float FadeTime;

    private ParallaxColors colComp;
    private int level = -1;

    public float InitialLevelTime = 2f;

	// Use this for initialization
	void Start () {
        Bg1 = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Bg2 = this.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        Bg3 = this.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        Fondo = this.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>();
        ChangeColors();
	}

    private void Update()
    {
        FadeTime = InitialLevelTime - ( Time.deltaTime * GameManager.instance.globalSpeed );
    }


    public void ChangeColors() {
        if (level < LevelColor.Count){
            level++;
        }else{
            level = 0;
        }
        switch (level)
        {
            case 0:
                Fondo.DOColor(LevelColor[0], FadeTime);
                Bg3.DOColor(LevelColor[0], FadeTime);
                Bg2.DOColor(LevelColor[0], FadeTime);
                Bg1.DOColor(LevelColor[0], FadeTime);
                break;
            case 1:
                Fondo.DOColor(LevelColor[1], FadeTime);
                Bg3.DOColor(LevelColor[1], FadeTime);
                Bg2.DOColor(LevelColor[1], FadeTime);
                Bg1.DOColor(LevelColor[1], FadeTime);
                break;
            case 2:
                Fondo.DOColor(LevelColor[2], FadeTime);
                Bg3.DOColor(LevelColor[2], FadeTime);
                Bg2.DOColor(LevelColor[2], FadeTime);
                Bg1.DOColor(LevelColor[2], FadeTime);
                break;
            case 3:
                Fondo.DOColor(LevelColor[3], FadeTime);
                Bg3.DOColor(LevelColor[3], FadeTime);
                Bg2.DOColor(LevelColor[3], FadeTime);
                Bg1.DOColor(LevelColor[3], FadeTime);
                break;
            case 4:
                Fondo.DOColor(LevelColor[4], FadeTime);
                Bg3.DOColor(LevelColor[4], FadeTime);
                Bg2.DOColor(LevelColor[4], FadeTime);
                Bg1.DOColor(LevelColor[4], FadeTime);
                break;
            case 5:
                Fondo.DOColor(LevelColor[5], FadeTime);
                Bg3.DOColor(LevelColor[5], FadeTime);
                Bg2.DOColor(LevelColor[5], FadeTime);
                Bg1.DOColor(LevelColor[5], FadeTime);
                break;
            case 6:
                Fondo.DOColor(LevelColor[6], FadeTime);
                Bg3.DOColor(LevelColor[6], FadeTime);
                Bg2.DOColor(LevelColor[6], FadeTime);
                Bg1.DOColor(LevelColor[6], FadeTime);
                break;
        }
    }   
}
