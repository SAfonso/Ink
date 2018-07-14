using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DemiumGames.AdMobManager;

public class UIManager : MonoBehaviour {

    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject highScorePanel;
    public GameObject canvasInt;

    public GameObject emptyPanel;
    public GameObject inGamePanel;


    public GameObject score;
    private TextMeshProUGUI scoreText;
    public GameObject hit;
    private TextMeshProUGUI hitText;
    public GameObject topScore;
    public TextMeshProUGUI[] topScoreText;
    public GameObject highScore;
    public TextMeshProUGUI[] highScoreText;
    public GameObject mascara;
    public GameObject topCoins;
    private TextMeshProUGUI topCoinsText;

    private Image[] myImages;
    private TextMeshProUGUI[] myTextPro;

    public Slider barraOld, barraActual;

    public CanvasGroup HighScoreCanvas;

    int puntos;

    public int maxScore = 0;
    private float startTime = 0f;


    private int currentPoints = 0;
    public static UIManager instance;

    float panelPositionY;


    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        instance.panelPositionY = instance.startPanel.transform.position.y;
    }

    void Start(){
        /*
        foreach (Image image in instance.highScorePanel.GetComponentsInChildren<Image>()){
            image.DOFade(0,0);
        }
        foreach (TextMeshProUGUI text in instance.highScorePanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.DOFade(0,0);
        }
        foreach (Image image in instance.canvasInt.GetComponentsInChildren<Image>()){
            image.DOFade(0, 0);
        }
        */
        HighScoreCanvas.alpha = 0f;
        HighScoreCanvas.interactable = false;
        HighScoreCanvas.blocksRaycasts = false;
        //

        instance.gameOverPanel.SetActive(false);
        instance.highScorePanel.SetActive(false);
        instance.canvasInt.gameObject.SetActive(false);
        instance.scoreText = score.gameObject.GetComponent<TextMeshProUGUI>();
        instance.hitText = hit.gameObject.GetComponent<TextMeshProUGUI>();
        instance.topCoinsText = topCoins.gameObject.GetComponent<TextMeshProUGUI>();

        myImages = instance.canvasInt.GetComponentsInChildren<Image>();
        myTextPro = instance.highScorePanel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void OnLevelWasLoaded(int level)
    {
        instance.scoreText = score.gameObject.GetComponent<TextMeshProUGUI>();
        instance.hitText = hit.gameObject.GetComponent<TextMeshProUGUI>();
        instance.topCoinsText = topCoins.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        startTime += Time.deltaTime;
    }

    public void ActiveInGamePanel() {
        instance.inGamePanel.gameObject.SetActive(true);
    }

    public void DeactiveInGamePanel() {
        instance.inGamePanel.gameObject.SetActive(false);
    }


	public void StartExit () {
        instance.panelPositionY = instance.startPanel.transform.position.y;
        instance.startPanel.transform.DOMoveX(emptyPanel.transform.position.x, 1f);
        GameManager.instance.gameOver = false;
	}
	
	public void GameOverEnter () {

        instance.inGamePanel.gameObject.SetActive(false);
        instance.highScorePanel.gameObject.SetActive(true);
        AdMobManager.Instance.ShowBanner();
        /*
        foreach (Image image in instance.highScorePanel.GetComponentsInChildren<Image>()){
            image.DOFade(1, .5f);
        }
        foreach (TextMeshProUGUI text in instance.highScorePanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.DOFade(1,.5f);
        }
        */
        HighScoreCanvas.DOFade(1f, 0.5f);
        HighScoreCanvas.interactable = true;
        HighScoreCanvas.blocksRaycasts = true;
        instance.canvasInt.gameObject.SetActive(true);
            foreach (Image image in myImages){
                image.DOFade(1, .5f);
            }
    }

    public void ContinueGO(){
        foreach (Image image in myImages){
            image.DOFade(0, .5f);
        }
        foreach (TextMeshProUGUI text in myTextPro)
        {
            text.DOFade(0,.5f);
        }
        instance.highScorePanel.gameObject.SetActive(false);
    }

    public void HideGameStart() {
        if (instance.startPanel.gameObject.activeSelf) {
            instance.startPanel.gameObject.SetActive(false);
        }
        
    }

    public void SetScore(int points) {
        //instance.scoreText.text = "" + points;
        GameManager.instance.actualDistance += (GameManager.instance.globalSpeed * Time.deltaTime );
        instance.scoreText.SetText("" + (int)GameManager.instance.actualDistance + " m");
                
        instance.maxScore = (int)GameManager.instance.actualDistance;
 
    }

    public void SetTopScore(int points) {
        puntos = points;
        foreach (TextMeshProUGUI text in instance.topScoreText){
            text.text = "" + points + "m";
        }
        foreach (TextMeshProUGUI text in instance.highScoreText)
            text.text = "" + instance.maxScore + "m";
        instance.topCoinsText.text = "" + GameManager.instance.points;

        if(points >= instance.maxScore){
            barraOld.DOValue(1f,0.3f);
            barraActual.DOValue ( (float)instance.maxScore/(float)points, 0.3f);
        }
        else{
            barraOld.DOValue ( (float)points/(float)instance.maxScore, 0.3f);
            barraActual.DOValue(1f,0.3f);
        }
    }

    public void SetHits(int hits) {
        if (hits >= 1)
        {
            instance.mascara.gameObject.transform.position = new Vector3(0.35f * (hits-1), instance.mascara.transform.position.y, 0);
        }
    }

    public void SetCoinsScore (int coins) {
        hitText.text = "" + coins;
    }
}
