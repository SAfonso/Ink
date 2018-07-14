using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoParallax : MonoBehaviour {

	public Transform Player;
	public Transform Fondo1,Fondo2,Fondo3;
	private Vector2 f1_init, f2_init, f3_init;
    
    [Header("Velocidades")]
    private float FactorFondo1 = 0.2f;
    private float FactorFondo2 = 0.1f;
    private float FactorFondo3 = 0.08f;
    private float smoothTime1 = 0.25f;
    private float smoothTime2 = 0.3f;
    private float smoothTime3 = 0.5f;

    private float endy1, endy2, endy3;

    void Start()
    {
        f1_init = Fondo1.position;
        f2_init = Fondo2.position;
        f3_init = Fondo3.position;

        endy1 = f1_init.y - 0.75f;
        endy2 = f2_init.y - 1.6f;
        endy3 = f3_init.y - 1.8f;
    }

	void Update() {
      
        Fondo1.position = new Vector2(Mathf.Lerp(Fondo1.position.x, f1_init.x - Player.position.x * FactorFondo1,  smoothTime1), Mathf.Lerp(Fondo1.position.y, endy1, 72f ));
        Fondo2.position = new Vector2(Mathf.Lerp(Fondo2.position.x, f2_init.x - Player.position.x * FactorFondo2,  smoothTime2), Mathf.Lerp(Fondo2.position.y, endy2, 72f ));
        Fondo3.position = new Vector2(Mathf.Lerp(Fondo3.position.x, f3_init.x - Player.position.x * FactorFondo3,  smoothTime3), Mathf.Lerp(Fondo3.position.y, endy3, 72f ));
    }

}
