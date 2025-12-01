using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    [Header("Config")]
    public UIManager uiManager;
    public float delayBeforeRestart = 5f;
    public bool pauseOnWin = true;
    public AudioSource winAudioSource;
    public AudioSource RollingAudioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winAudioSource != null)
            {
                winAudioSource.Play();
                RollingAudioSource.Stop();
            }
            StartCoroutine(HandleWin());
        }
    }

    IEnumerator HandleWin()
    {
        if (uiManager != null)
        {
            uiManager.ShowWinScreen();
        }

        if (pauseOnWin)
            Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(delayBeforeRestart);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
