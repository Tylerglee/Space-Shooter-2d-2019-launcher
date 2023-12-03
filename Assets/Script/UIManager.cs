using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField] 
    private Text _gameOver2Text;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]    
    private Sprite[] _liveSprites;
  
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {      
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameOver2Text.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null )
        {
            Debug.LogError("GameManager is NULL.");
        }
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives )
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0) 
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverFlickerRoutine());
    }
    
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameManager.Gameover();
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
           _gameOver2Text.gameObject.SetActive(true);
            _gameOver2Text.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver2Text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
   
 }