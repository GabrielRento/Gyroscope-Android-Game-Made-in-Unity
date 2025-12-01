using UnityEngine;
using System.Collections;

public class FinalTriggerEventCameraMovement : MonoBehaviour
{
    [SerializeField] private float newOffsetY = 0.20f;
    [SerializeField] private float transitionDuration = 1f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        CameraManager cam = Camera.main.GetComponent<CameraManager>();
        if (cam != null)
        {
            StartCoroutine(AnimateCameraOffset(cam));
        }
    }

    private IEnumerator AnimateCameraOffset(CameraManager cam)
    {
        Vector3 startOffset = cam.FollowOffset;
        Vector3 endOffset = new Vector3(
            startOffset.x,
            newOffsetY,
            startOffset.z
        );

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;

            cam.FollowOffset = Vector3.Lerp(startOffset, endOffset, t);

            yield return null;
        }

        cam.FollowOffset = endOffset;
    }
}
