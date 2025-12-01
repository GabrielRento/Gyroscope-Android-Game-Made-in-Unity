using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float gameTime = 60f;
    public int coinsToWin = 4;

    [Header("UI References")]
    [SerializeField] private UIManager uiManager;

    [Header("Final Gate")]
    [SerializeField] private Animator finalGateAnimator;
    [SerializeField] private AudioSource OpenGate;

    public int coins = 0;

    private float currentTime;
    private bool gameActive = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentTime = gameTime;
        UpdateUI();
        if (uiManager != null)
            uiManager.UpdateCoinText();
    }

    void Update()
    {
        if (gameActive)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();

            if (currentTime <= 0)
            {
                currentTime = 0;
                LoseGame();
            }
        }
    }

    void UpdateUI()
    {
        if (uiManager != null)
            uiManager.UpdateTimerText(Mathf.CeilToInt(currentTime));
    }

    public void AddCoin()
    {
        coins++;
        if (uiManager != null)
            uiManager.UpdateCoinText();

        if (coins >= coinsToWin)
            OpenFinalGate();
    }

    public void OpenFinalGate()
    {

        if (finalGateAnimator != null)
            finalGateAnimator.SetTrigger("Trigger");
            OpenGate.Play();
    }

    public void LoseGame()
    {
        if (gameActive)
        {
            gameActive = false;
            if (uiManager != null)
                uiManager.ShowLoseScreen();
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
