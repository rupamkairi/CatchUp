using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public LevelGenerator levelScript;
    public float speed;
    [HideInInspector]public int Money;
    public Text PointText;
    public Text EndText;
    public Joystick joystick;

    private bool isCaught;
    private int Level;
    private int CoinCount;
    private Rigidbody rb;



    // Make call to Setup Game
    void Awake()
    {
        levelScript = GetComponent<LevelGenerator>();
        initGame();
    }

    // Setup Game
    public void initGame()
    {
        levelScript.SetupScene();
		CoinCount = levelScript.coinCount;
    }

    // Starts Game Elements
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Set Points at the start.
        /// <Save_From_Last_Scene>
        Money = PlayerPrefs.GetInt("Score", 5);
        SetMoneyText();

        // Set Ending Text component and setting it to null
        EndText.text = null;
    }

    void Update()
    {
        // If Player get caught
        if (isCaught)
        {
            // Slower the time and Restart, show End Text, go to another Scene
            Time.timeScale = 0.2f;
            EndText.text = "ARRESTED! 5 MONEY PENALTY";
            SetMoneyText();
            StartCoroutine(Restart());
        }

        // If Player collected all Coins
		if (CoinCount == 0)
		{
            // Slower the time, Congratulate, go to the another level or Scene
			Time.timeScale = 0.2f;
			EndText.text = "GREAT ! LEVEL CLEARED";
			Level++;
			StartCoroutine(Restart());
		}

        // Reset Score values
        if (Input.GetButtonUp("Reset"))
        {
            ResetGame();
        }

        // Setting the Money value and storing it.
        PlayerPrefs.SetInt("Score", Money);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Setting movement for Keyboard
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = (movement * speed);

        // Setting movement for Touch Input devices
        joystickControl();
    }

    void OnTriggerEnter(Collider other)
    {
        // colletct Coins when collided
        if (other.gameObject.tag == "Coin")
        {
            // remove Coin after collision, Increase Point
            // Destroy(other.gameObject);
            other.gameObject.SetActive (false);
            Money ++;
            CoinCount--;
            SetMoneyText();
        }

        if (other.gameObject.tag == "Enemy")
        {
            // If collided to AI, Player get caught
            Money -= 5;
            isCaught = true;
        }
    }

    // Set Money amount to Money Text
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

    // Logic for Joystic type touch control
    /// <Get_Asset> from <https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631>
    void joystickControl()
    {
        Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
        transform.Translate(moveVector * (speed + 0.5f) * Time.deltaTime);
    }

    // Resets all Game Values
    public void ResetGame()
    {
        Debug.Log("RESET Button clicked");
        EndText.text = "YOU CHOSEN TO RESTART THE SPREE";
        Time.timeScale = 0.2f;
        Money = 5;
        SetMoneyText();
        StartCoroutine(Restart());
    }

    
}
