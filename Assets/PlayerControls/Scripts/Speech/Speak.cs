using UnityEngine;
using UnityEngine.Audio;

public class Speak : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header("Microphone detection")]
    public MicDetection detector;
    Vector3 minScale;
    Vector3 maxScale;

    public AudioSource source;

    public float loudnesSensibility = 100;
    public float threshhold = 0.1f;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {

            float loudness = detector.GetLoudnessFromMicrophone() * loudnesSensibility;

        if (loudness < threshhold)
            loudness = 0;
        
       //transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);

        animator.SetFloat("Loudness", loudness);

        print(loudness);
    }
}
