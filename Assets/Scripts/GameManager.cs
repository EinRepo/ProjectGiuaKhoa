using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private PlayerController playerController;

    private int fps;

    private float playerFirstYPosition;
    private int playerScore;
    private float timer;
    void Start()
    {
        playerFirstYPosition = 8f; //8 the height when camera shift to player
        playerScore = 0;

        InvokeRepeating("FPSCounterShow", 0, 0.2f);
    }


    void Update()
    {
        timer += Time.unscaledDeltaTime; //Timer for increase timescale


        IncreaseGameSpeed();
        PlayerScore();
        FPSCalculation();
        if (playerController.IsDead() == true)
        {
            GameOver();
        }



    }

    private void PlayerScore()
    {

        if (player.position.y > 8) //8 the height when camera shift to player
        {
            playerScore = (int)((player.position.y - playerFirstYPosition) * 100);
            scoreText.text = "Score: " + playerScore.ToString();
        }

    }

    private void IncreaseGameSpeed()
    {
        if (timer > 10f)
        {
            Time.timeScale += 0.1f;
            timer = 0;
        }
    }

    private void FPSCalculation()
    {
        fps = (int)(1 / Time.unscaledDeltaTime);
    }

    private void FPSCounterShow()
    {
        fpsText.text = "FPS: " + fps.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);

        //move the score field to the middle
        int smoothSpeed = 3;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float desiredPositionX = screenWidth / 2;
        float desiredPositionY = screenHeight / 1.7f;

        Vector2 desiredPosition = new Vector2(desiredPositionX, desiredPositionY);
        Vector2 desiredSize = new Vector3(2, 2, 2);

        Vector2 currentPosition = scoreText.transform.position;
        Vector2 currentSize = scoreText.transform.localScale;

        Vector2 newPosition = Vector2.Lerp(currentPosition, desiredPosition, Time.unscaledDeltaTime * smoothSpeed);
        Vector2 newSize = Vector2.Lerp(currentSize, desiredSize, Time.unscaledDeltaTime * smoothSpeed);

        scoreText.transform.position = newPosition;
        scoreText.transform.localScale = newSize;






    }
}
