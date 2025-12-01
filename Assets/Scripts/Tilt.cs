using UnityEngine;

public class Tilt : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform board;

    [Header("Settings")]
    [SerializeField] private float maxAngle = 15f;
    [SerializeField] private float smooth = 0.12f;

    private float curPitch;
    private float curRoll;
    private float velPitch;
    private float velRoll;
    private Vector3 calibration;

    void Start()
    {
        if (board == null) board = transform;
        Input.gyro.enabled = true;
        Calibrate();
    }

    void Update()
    {
        Vector3 g = Input.gyro.gravity - calibration;

        float targetRoll = -g.y * 90f;
        float targetPitch = -g.x * 90f;

        if (maxAngle > 0f)
        {
            targetRoll = Mathf.Clamp(targetRoll, -maxAngle, maxAngle);
            targetPitch = Mathf.Clamp(targetPitch, -maxAngle, maxAngle);
        }

        curRoll = Mathf.SmoothDampAngle(curRoll, targetRoll, ref velRoll, smooth);
        curPitch = Mathf.SmoothDampAngle(curPitch, targetPitch, ref velPitch, smooth);

        board.localRotation = Quaternion.Euler(curPitch, 0f, curRoll);
    }


    public void Calibrate()
    {
        calibration = Input.gyro.gravity;
    }
}
