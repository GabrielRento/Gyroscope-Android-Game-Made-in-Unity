using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIManager uiManager;

    [Header("Rolling Pitch")]
    [SerializeField] private AudioSource rollingAudio;
    [SerializeField] private float pitchMin = 0.6f;
    [SerializeField] private float pitchMax = 2f;
    [SerializeField] private float speedMaxForPitch = 10f;

    [Header("Rolling Volume")]
    [SerializeField] private float volumeMax = 1f;
    [SerializeField] private float speedMaxForVolume = 8f;
    [SerializeField] private float speedSilenceThreshold = 0.2f;

    private Rigidbody rb;
    private Vector3 inputVector;
    public AudioSource coinAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (uiManager != null)
            uiManager.UpdateCoinText();
    }

    void Update()
    {
        UpdateRollingPitch();
        UpdateRollingVolume();
    }

    void UpdateRollingPitch()
    {
        float speed = rb.linearVelocity.magnitude;
        float t = Mathf.Clamp01(speed / speedMaxForPitch);
        rollingAudio.pitch = Mathf.Lerp(pitchMin, pitchMax, t);
    }

    void UpdateRollingVolume()
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed <= speedSilenceThreshold)
        {
            rollingAudio.volume = 0f;
            return;
        }

        float t = Mathf.Clamp01((speed - speedSilenceThreshold) / (speedMaxForVolume - speedSilenceThreshold));
        rollingAudio.volume = Mathf.Lerp(0f, volumeMax, t);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinAudioSource.Play();
            Destroy(other.gameObject);
            if (GameManager.Instance != null)
                GameManager.Instance.AddCoin();
        }
    }
}
