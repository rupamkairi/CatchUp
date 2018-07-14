using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public LevelGenerator levelScript;
    public float speed;
    public Text PointText;
    public Text EndText;
    public Text levelText;
    public Joystick joystick;

    private bool isCaught;
    private int Level;
    private int Money;
    private int CoinCount;
    private Rigidbody rb;


    void Awake()
    {
        levelScript = GetComponent<LevelGenerator>();
        initGame();
    }

    public void initGame()
    {
        levelScript.SetupScene();
		CoinCount = levelScript.coinCount;
		Debug.Log(CoinCount);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Money = 5;
        SetMoneyText();

        EndText.text = null;
		levelText.text = "LEVEL " + Level.ToString();
    }

    void Update()
    {
        if (isCaught)
        {
            Time.timeScale = 0.2f;
            StartCoroutine(Restart());
        }

		if (CoinCount == 0)
		{
			Debug.Log("Win");
			Time.timeScale = 0.4f;
			EndText.text = "GREAT ! LEVEL CLEARED";
			Level++;
			StartCoroutine(Restart());
		}
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = (movement * speed);

        joystickControl();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            Money += 2;
            CoinCount--;
            SetMoneyText();
			Debug.Log(CoinCount);
        }

        if (other.gameObject.tag == "Enemy")
        {
            isCaught = true;
            EndText.text = "ARRESTED ! 5 MONEY PENALTY";
            Money -= 5;
        }
    }

    void SetMoneyText()
    {
        PointText.text = "MONEY " + Money.ToString();
    }

    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void joystickControl()
    {
        Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
        transform.Translate(moveVector * speed * Time.deltaTime);
    }
}
