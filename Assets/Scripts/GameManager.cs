using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }

    public Paddle paddle { get; private set; }

    public Bricks[] bricks { get; private set; }

    public int level = 1;

    public int Score = 0;

    Text ScoreUI;

    public int lives = 3;

    //GameObject PanelSelesai;
    //Text txWin;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
        //PanelSelesai = GameObject.Find("PanelSelesai");
       // PanelSelesai.SetActive(false);
    }
    
    private void NewGame()
    {
        this.Score = 0;
        this.lives = 3;
        
        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        if (level > 2)
        {
            SceneManager.LoadScene("You Win");
        }
        else
        {
            SceneManager.LoadScene("Level" + level);
        }
       
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Bricks>();
    }

    public void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        this.lives = 3;
        //NewGame();
    }

    public void Miss()
    {
        this.lives--;

        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver(); 
        }
    }

    public void Hit(Bricks bricks)
    {
        this.Score += bricks.points;
        ScoreUI = GameObject.Find("ScoreText").GetComponent<Text>();
        Debug.Log("Score = " + Score);
        ScoreUI.text = "Score = " + Score;
        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
        //if (Score == 7800)
        {
            //PanelSelesai.SetActive(true);
            //txWin = GameObject.Find("Win").GetComponent<Text>();
            //txWin.text = "You Win";
            //Destroy(gameObject);
            //return;
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }
}