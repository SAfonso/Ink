using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class player_control : MonoBehaviour {
	
	
	public Ease tweenType = Ease.OutBack;
	public float jumpTime = 0.3f;
	private bool canJump;
    public float comboTime = 1.0f;
    public int comboHit = 1;

    private float actualComboTime;

    public float inmortalTime = 5.0f;
    private float timeToInmortality;

    private bool isSmall = true;
    private bool isJumping = false;
    private ParticleSystem particle;
    private GameObject tutorial;
    private Animator myAnim;


	void Start()
	{
		DOTween.Init();
        jumpTime = 1 / GameManager.instance.GetGlobalSpeed();
        timeToInmortality = Time.time;
        particle = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        tutorial = UIManager.instance.transform.GetChild(3).gameObject;
        myAnim = this.gameObject.GetComponent<Animator>();
	}

	void Update () {
        isJumping = false;
        // Player Jump
        if (Input.GetMouseButtonDown(0) && canJump && !GameManager.instance.gameOver && !GameManager.instance.invincible){
			if(tutorial.activeSelf){
                tutorial.SetActive(false);
            }
            this.transform.DOJump(new Vector2 (Mathf.Sign(this.transform.position.x) * (-2.1f), this.transform.position.y),0.5f,1, jumpTime).SetEase(tweenType);
            this.transform.localScale= new Vector3(1, this.transform.localScale.y * -1, 1);
            Sfx.instance.PlayFallSound();
            isJumping = true;
		}

        // If combo time is over and we are not invincible we reduce the combo Hit to 1 (Initial value at once)
        if (actualComboTime < Time.time && !GameManager.instance.invincible){
            comboHit = 1;
        }

        // If we are inmortal and big and inmortal time is over we shrink the player
        if (Time.time > timeToInmortality && GameManager.instance.invincible && !isSmall) {
            Camera.main.GetComponent<ShakeScreen>().DoScreenShake(); // screen shake
            Sfx.instance.PlayShrinkSound(); // shringk sound
            GameManager.instance.invincible = false; // No longer invincible

            // We shrink the player, play the animation
            this.transform.position = new Vector3(Mathf.Sign(this.transform.position.x) * ( 2.1f), this.transform.position.y, 0);
            this.transform.localScale = new Vector3(1, Mathf.Sign(this.transform.position.x) * 1, 1);
            this.GetComponent<Animator>().SetTrigger("grow");                   //Activa esto
            this.GetComponent<Animator>().SetTrigger("small");                      
            // Reset values as they were before invincible state
            GameManager.instance.globalSpeed = GameManager.instance.lastSpeed;
            Sfx.instance.SetPitch(GameManager.instance.lastPitch);
            
            isSmall = true;
            comboHit = 1;
        }

        // If we are invincible...
        if (GameManager.instance.invincible) {
            // ...and small
            this.transform.position = new Vector3(Mathf.Sign(this.transform.position.x) * 0.2f, this.transform.position.y, 0);
            this.transform.localScale = new Vector3(12, Mathf.Sign(this.transform.position.x) * 12, 0);
            if (isSmall && canJump ){
                // WE GET BIGGGGG!!!!!
                Camera.main.GetComponent<ShakeScreen>().DoScreenShake(); // screen shake  Activa esto
                Sfx.instance.PlayGrowSound(); // grow sound

                // We get big, play the animation
                this.GetComponent<Animator>().SetTrigger("grow"); // grow animation       Activa esto         

                // New values for invincible time
                GameManager.instance.lastSpeed = GameManager.instance.globalSpeed;
                GameManager.instance.globalSpeed = 8;


                GameManager.instance.lastPitch = Sfx.instance.backgroundMusic.pitch;
                Sfx.instance.SetPitch(1.3f);

                isSmall = false;
            }
         
        }
        UIManager.instance.SetHits(comboHit);
    }

    public void CheckContainers(){
        float value = Mathf.Abs(GameManager.instance.containerOne.transform.position.y - GameManager.instance.containerTwo.transform.position.y);
        if (value != 10){
            float realPos = 10 - value;
            GameManager.instance.containerTwo.transform.position = new Vector2(GameManager.instance.containerTwo.transform.position.x , GameManager.instance.containerTwo.transform.position.y - realPos);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Suelo"))
        {
            canJump = true;
            myAnim.SetBool("jumping", false);
        }
        else if (other.CompareTag("Vacio") && !GameManager.instance.invincible) { // We DIEEE!!!!
            if (!isJumping){
                if (!GameManager.instance.gameOver) {
                    canJump = false;                    //Controlar si se está moviendo y cancelarlo
                    Sfx.instance.PlayDeathSound();
                    this.transform.DOMove(new Vector2(Mathf.Sign(this.transform.position.x) * (2.3f), this.transform.position.y), 0.1f).SetEase(tweenType);
                    myAnim.SetBool("isDead", true);
                    Sfx.instance.SetPitch(0.8f);
                    GameManager.instance.gameOver = true;
                    GameManager.instance.SaveTopScore();
                    StartCoroutine(DeadParticles());
                    
                    Invoke("GameOverPanel", 1.5f);
                }
            }
            
            
        }
    }

        IEnumerator DeadParticles()
        {
            yield return new WaitForSeconds(0.2f);
            particle.Play();
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moneda")) {
            if (actualComboTime >= Time.time && !GameManager.instance.invincible)
            {
                comboHit++;
            }
            if (comboHit >= 10 && !GameManager.instance.invincible) {
                comboHit = 10;
                GameManager.instance.invincible = true;
                timeToInmortality = Time.time + inmortalTime;

            }
            if (GameManager.instance.invincible) {
                comboHit = 10;
            }
            
            // We active the particle
            GameObject particle = other.transform.GetChild(0).gameObject;
            particle.SetActive(true);
            particle.transform.DOMove(new Vector2(0, 3.71f), 0.5f);
            other.GetComponent<Moneda>().DisableParticleRetarded();          

            // Sound
            Sfx.instance.PlayPickSound();

            GameManager.instance.points++;
            actualComboTime = Time.time + comboTime;

            // We hide the "coin"
            //                                                               Activa esto
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider>().enabled = false;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Suelo")) {
            canJump = false;
            myAnim.SetBool("jumping", true);
        }
    }

    private void GameOverPanel() {
        UIManager.instance.GameOverEnter();
    }
}
