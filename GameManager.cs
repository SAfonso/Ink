using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DemiumGames.AdMobManager;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject levelPost;
    public float globalSpeed = 1f;
    public Color[] colors;

    public bool gameOver = true;
    public bool resetGame = false;

    public int points = 0;
    public int hits = 1;
    public int level;
    public int minMap;
    public int maxMap;
    public float maxPitchValue = 1.5f;
    public bool invincible = false;

    public float actualDistance = 1;

    public float lastSpeed;
    public float lastPitch;

    public bool canPlay;


    private List<int> levelDistances;
    //public List<GameObject> coins;
    private ParallaxColorChanger paralax;
    public int nextLevelDistance;
    public int actualLevel;

    public int resetCounter = 0;

    public GameObject containerOne, containerTwo;

    void Awake()
    {
 
        //Application.targetFrameRate = 60;

        //if (instance == null)
            instance = this;

        //else if (instance != this)
            //Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
        //InitGame();

        canPlay = PlayerPrefs.GetInt("canPlay") == 0? false : true;
       
    }

    void Start()
    {
        LevelLoaded(1);
        //gameOver = true;

    }

    private void InitGame() {
        
        instance.points = 0;
        instance.hits = 1;
        instance.gameOver = false;
        resetGame = false;
        instance.level = 0;
        instance.minMap = 1;
        instance.maxMap = 4;
        instance.globalSpeed = 5;
        instance.actualDistance = 1;
        instance.actualLevel = 1;
        instance.nextLevelDistance = 111;


        /*if (Constants.isFirstTime){
            AdMobManager.Instance.SetOnBannerLoaded(()=>{
                AdMobManager.Instance.HideBanner();
            });
            AdMobManager.Instance.LoadBanner(AdSize.Banner, AdPosition.Bottom);
            Constants.isFirstTime = false;
            if (PlayerPrefs.HasKey("resetCounter")){
                Constants.resetCounter = PlayerPrefs.GetInt("resetCounter")-1;
                if (Constants.resetCounter < 0){
                    Constants.resetCounter = 0;
                }                
                else if ((Constants.resetCounter < 3) && Constants.resetCounter != 1){
                    AdMobManager.Instance.LoadInter();
                }
            }else{
                Constants.resetCounter = 0;
            }
            
        }*/

        containerOne = GameObject.Find("Container");
        containerOne = GameObject.Find("Container 1");

        /*if(!PlayerPrefs.HasKey("topScore")){
            GameObject.Find("CanvasTutorial").SetActive(true);
        }else{
            GameObject.Find("CanvasTutorial").SetActive(false);
        }

        if(Constants.resetCounter == 1){
            AdMobManager.Instance.LoadInter();
        }else if(Constants.resetCounter == 4){
            AdMobManager.Instance.DestroyInter();
            Constants.resetCounter = 0;
        }*/ 
        //coins = new List<GameObject>();

    }

    private void LevelLoaded(int level)
    {
        //Sfx.instance.SetPitch(0.8f);
        //Sfx.instance.PlayBackGroundMusic();
        //levelPost = GameObject.FindGameObjectWithTag("Cartel");
        gameOver = false;
        InitGame();
        
    }

    public void GetNexLevelDistance(){
        instance.nextLevelDistance += (int)(Mathf.Pow(1.1f, instance.actualLevel) * 75);
    }

    private void Update()
    {
        if (!instance.gameOver)
        {

            UIManager.instance.SetScore(instance.points);
            //UIManager.instance.SetCoinsScore(instance.points);
            if (instance.actualDistance > instance.nextLevelDistance)
            {
                instance.actualLevel++;
                GetNexLevelDistance();
                instance.level++;
                UpdateLevels();
                GameObject.FindGameObjectWithTag("FondoParallax").GetComponent<ParallaxColorChanger>().ChangeColors();
                levelPost.GetComponent<PostController>().startMove = true;
                globalSpeed += 0.2f;
                Sfx.instance.IncrementPitch(0.01f);
                if (globalSpeed >= 7)
                {
                    globalSpeed = 7;
                }
            }
        }

    }

/*    public bool TimePass(){
        return (instance.timer <= 0f);
    }

    public void ResetTime(){
        instance.timer = 180f;
    }*/

    public float GetGlobalSpeed()
    {
        return globalSpeed;
    }

    public void SaveTopScore() {
        if (!PlayerPrefs.HasKey("topScore"))
        {
            PlayerPrefs.SetInt("topScore", instance.points);
            UIManager.instance.SetTopScore(0);
        }
        else {
            int topScore = PlayerPrefs.GetInt("topScore");
            if (UIManager.instance.maxScore > topScore)
            {
                PlayerPrefs.SetInt("topScore", UIManager.instance.maxScore);
                UIManager.instance.SetTopScore(topScore);
            }
            else {
                UIManager.instance.SetTopScore(topScore);
            }
        }
    }


    private void UpdateLevels() {
        switch (instance.actualLevel) {
            case 1: minMap = 1;
                maxMap = 4;
                break;
            case 2: minMap = 2;
                maxMap = 5;
                break;
            case 3: minMap = 3;
                maxMap = 6;
                break;
            case 4:minMap = 3;
                maxMap = 7;
                break;
            case 5: minMap = 3;
                maxMap = 8;
                break;
            case 6:
                minMap = 4;
                maxMap = 7;
                break;
            case 7:
                minMap = 3;
                maxMap = 9;
                break;
            case 8:
                minMap = 4;
                maxMap = 9;
                break;
            case 9:
                minMap = 5;
                maxMap = 9;
                break;
            case 10:
                minMap = 6;
                maxMap = 10;
                break;
            default:
                minMap = 7;
                maxMap = 10;
                break;
        }
    }
}


public class GameState{

    public int points;
    public int level; 
    
}