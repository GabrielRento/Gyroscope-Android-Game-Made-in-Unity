using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform targetAnchor;

    [Header("Initial Descent (linear)")]
    [SerializeField] private float initialDescentAmountY = -5f;
    [SerializeField] private float initialDescentDuration = 3f;

    [Header("Timing -> Startup to target")]
    [SerializeField] private float startupMoveDuration = 1.2f;

    [Header("Follow")]
    [SerializeField] private Vector3 followOffset = new Vector3(0f, 6f, -10f);
    [SerializeField] private float followSmoothTime = 0.25f;

    private Vector3 velocity;
    private bool isFollowing;

    void Start()
    {
        isFollowing = false;
        StartCoroutine(RunStartupSequence());
    }

    void LateUpdate()
    {
        if (!isFollowing || targetAnchor == null) return;

        Vector3 targetPosition = targetAnchor.position + followOffset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            followSmoothTime
        );
    }

    private IEnumerator RunStartupSequence()
    {
        yield return InitialDescent();
        yield return MoveToTarget();
        isFollowing = true;
    }

    private IEnumerator InitialDescent()
    {
        if (initialDescentDuration <= 0f) yield break;

        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(0f, initialDescentAmountY, 0f);

        yield return LerpPosition(start, end, initialDescentDuration);
    }

    private IEnumerator MoveToTarget()
    {
        if (targetAnchor == null) yield break;

        Vector3 start = transform.position;
        Vector3 end = targetAnchor.position + followOffset;

        yield return SmoothStepPosition(start, end, startupMoveDuration);
    }

    private IEnumerator LerpPosition(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(from, to, Mathf.Clamp01(t));
            yield return null;
        }

        transform.position = to;
    }

    private IEnumerator SmoothStepPosition(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(from, to, smooth);
            yield return null;
        }

        transform.position = to;
    }

    public void SetTargetAnchor(Transform anchor)
    {
        targetAnchor = anchor;
    }

    public Vector3 FollowOffset
    {
        get => followOffset;
        set => followOffset = value;
    }
}
