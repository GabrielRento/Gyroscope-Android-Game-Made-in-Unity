using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game UI")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("End Game Screens")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI loseReasonText;

    void Start()
    {
        HideAllScreens();
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        int current = 0;
        int total = 0;
        if (GameManager.Instance != null)
        {
            current = GameManager.Instance.coins;
            total = GameManager.Instance.coinsToWin;
        }

        if (coinText != null)
            coinText.text = $"{current}{total}";
    }

    public void UpdateTimerText(int time)
    {
        if (timerText != null)
            timerText.text = $"Tempo: {time}s";
    }

    public void ShowWinScreen()
    {
        if (winScreen != null)
            winScreen.SetActive(true);
        if (loseScreen != null)
            loseScreen.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        if (loseScreen != null)
            loseScreen.SetActive(true);
        if (winScreen != null)
            winScreen.SetActive(false);
    }

    void HideAllScreens()
    {
        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);
    }
}
