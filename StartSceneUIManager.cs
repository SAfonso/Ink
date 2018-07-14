using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartSceneUIManager : MonoBehaviour {

    public GameObject credits;

    public void OpenCredits()
    {
        credits.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }

    public void CloseCredits()
    {
        credits.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
    }
}
