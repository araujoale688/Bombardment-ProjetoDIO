using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton.
    public static GameManager Instance {  get; private set; }

    // API.
    public bool isGameOver { get; private set; }

    [Header("Camera")]
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [Header("Audio")]
    [SerializeField] 
    private AudioSource musicPlayer;
    [SerializeField]
    private AudioSource splash;
    [SerializeField] 
    private AudioSource gameOverSFX;

    [Header("Score")]
    [SerializeField] 
    private float score;
    [SerializeField] 
    private int highestScore;

    // Constants.
    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    private void Awake()
    {
        //Iniciar Singleton.
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //score.
        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE);
    }

    private void Update()
    {
        if(!isGameOver)
        {
            score += Time.deltaTime;

            if(GetScore() > GetHighstScore())
            {
                highestScore = GetScore();
            }
        }
    }

    public int GetScore()
    {
        return (int) Mathf.Floor(score);
    }

    public int GetHighstScore()
    {
        return highestScore;
    }

    public void EndGame()
    {
        if(isGameOver)
        {
            return;
        }

        isGameOver = true;

        musicPlayer.Stop();

        splash.Play();

        gameOverSFX.Play();

        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, GetHighstScore());

        StartCoroutine(ReloadScene(6f));
    }

    private IEnumerator ReloadScene(float delay)
    {
        virtualCamera.gameObject.SetActive(false);

        playerCamera.fieldOfView = 30f;

        yield return new WaitForSeconds(delay);

        string sceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName);
    }
}