using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DemiumGames.AdMobManager;

public class LoadScript : MonoBehaviour {

    AsyncOperation async;

    public void ToggleMusic(){
        if (FindObjectOfType<Toggle>().isOn == true){
            Debug.Log(PlayerPrefs.GetInt("canPlay"));
            PlayerPrefs.SetInt("canPlay", 1);
            GameObject.FindObjectOfType<AudioSource>().mute = true;
        }else if (FindObjectOfType<Toggle>().isOn == false){
            Debug.Log(PlayerPrefs.GetInt("canPlay"));
            PlayerPrefs.SetInt("canPlay", 0);
            GameObject.FindObjectOfType<AudioSource>().mute = false;
        }
    }


    void Start(){
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        WaitToRestart();
        StartAudio();
        Debug.Log(PlayerPrefs.GetInt("canPlay"));
        Debug.Log(GameObject.FindObjectOfType<AudioSource>().mute);
    }


	void Update(){
		if (Input.GetKey(KeyCode.Escape)){
            PlayerPrefs.SetInt("resetCounter", Constants.resetCounter);
            AdMobManager.Instance.DestroyAllInstances();
			Application.Quit();
		}
	}


    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void Restart(){
        if ((Constants.resetCounter == 3)){
                AdMobManager.Instance.ShowInter();
                Constants.resetCounter++;
                SceneManager.LoadScene(1);
                
        }else{
            Constants.resetCounter++;
            SceneManager.LoadScene(1);
            
        }


    }

    public void GoToInit(){
        AdMobManager.Instance.HideBanner();
        SceneManager.LoadScene(0);
    }

    private void StartAudio(){
        if(SceneManager.GetActiveScene().buildIndex == 0){
            if(PlayerPrefs.HasKey("canPlay")){
                if(PlayerPrefs.GetInt("canPlay") == 0){
                    FindObjectOfType<Toggle>().isOn = false;
                    ToggleMusic();
                }else if (PlayerPrefs.GetInt("canPlay") == 1){
                    FindObjectOfType<Toggle>().isOn = true;
                    ToggleMusic();
                }
            }else
                PlayerPrefs.SetInt("canPlay", 1);
        }

    }

    IEnumerator WaitToRestart(){
        yield return new WaitForSeconds(.1f);
    }


}
