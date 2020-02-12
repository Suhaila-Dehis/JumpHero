using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpScript : MonoBehaviour
{
    public static PlayerJumpScript instance;
    public Rigidbody2D myBody;
    public Animator anim;

    [SerializeField]
    private float forceX, forceY;

    private float thresholdX = 7f;
    private float thresholdY = 14f;

    private bool setPower, didJump;


    Slider powerBar;
    float powerBarThreshold = 10;
    float powerBarValue = 0;

    private void Awake()
    {
        MakeInstance();
        Initialize();
    }
    private void Update()
    {
        SetPower();
    }

    public void SetPower(bool setPower)
    {
        this.setPower = setPower;

        if (!setPower)
        {
            Jump();
        }
    }

    void Jump()
    {
        myBody.velocity = new Vector2(forceX, forceY);
        forceX = forceY = 0;
        didJump = true;

        anim.SetBool("Jump", didJump);
        powerBarValue = 0;
        powerBar.value = powerBarValue;
    }


    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    void SetPower()
    {
        if (setPower)
        {
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            if (forceX > 6.5f)
            {
                forceX = 6.5f;
            }

            if (forceY > 13.5f)
            {
                forceY = 13.5f;
            }

            powerBarValue += powerBarThreshold * Time.deltaTime;
            powerBar.value = powerBarValue;
        }

    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (didJump)
        {
            didJump = false;
            anim.SetBool("Jump", didJump);

            if (target.tag.Equals("Platform"))
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.CreateNewPlatformAndLerp(target.transform.position.x);
                }
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.IncrementScore();
                }
            }
        }

        if (target.tag.Equals("Dead"))
        {
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.GameOverShowPanel();
            }
            Destroy(this.gameObject);
        }
    }



    private void Initialize()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();
        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }
}