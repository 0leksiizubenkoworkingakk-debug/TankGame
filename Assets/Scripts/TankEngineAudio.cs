using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class TankEngineAudio : MonoBehaviour
    {
       [Header("Audio")]
    [SerializeField] private AudioClip engineClip;
    [SerializeField] private AudioSource engineAudio;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float movementThreshold = 0.1f;

    [Header("Volume")]
    [SerializeField, Range(0f, 1f)] private float minVolume = 0.2f;
    [SerializeField, Range(0f, 1f)] private float maxVolume = 1f;
    [SerializeField] private float volumeSmoothSpeed = 3f;

    [Header("Pitch")]
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.5f;
    [SerializeField] private float pitchSmoothSpeed = 2f;

    private Rigidbody tankRigidbody;

    private float targetVolume;
    private float targetPitch;

    private void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();

        if (engineAudio == null)
            engineAudio = GetComponent<AudioSource>();

        engineAudio.clip = engineClip;
        engineAudio.loop = true;
        engineAudio.playOnAwake = false;
        engineAudio.volume = 0f;

        targetPitch = minPitch;
        engineAudio.pitch = minPitch;

        if (engineClip != null)
            engineAudio.Play();
    }

    private void Update()
    {
        UpdateEngineSound();
        SmoothAudio();
    }

    private void UpdateEngineSound()
    {
        float speed = new Vector3(
            tankRigidbody.linearVelocity.x,
            0f,
            tankRigidbody.linearVelocity.z
        ).magnitude;

        bool isMoving = speed > movementThreshold;

        if (!isMoving)
        {
            targetVolume = 0f;
            targetPitch = minPitch;
            return;
        }

        float speedPercent = Mathf.Clamp01(speed / maxSpeed);

        targetVolume = Mathf.Lerp(
            minVolume,
            maxVolume,
            speedPercent
        );

        targetPitch = Mathf.Lerp(
            minPitch,
            maxPitch,
            speedPercent
        );
    }

    private void SmoothAudio()
    {
        engineAudio.volume = Mathf.MoveTowards(
            engineAudio.volume,
            targetVolume,
            volumeSmoothSpeed * Time.deltaTime
        );

        engineAudio.pitch = Mathf.MoveTowards(
            engineAudio.pitch,
            targetPitch,
            pitchSmoothSpeed * Time.deltaTime
        );
    }
    }
}