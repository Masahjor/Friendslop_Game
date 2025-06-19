using UnityEngine;

public class GimpelSpeak : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    public MicDetection detector;
    public AudioSource audioSource;

    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness;
    private float[] clipSampleData;

    public float threshhold = 0.1f;


    // Use this for initialization
    void Awake()
    {

        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }


    // Update is called once per frame
    void Update()
    {
        float Loudness = detector.GetLoudnessFromMicrophone() * clipLoudness * 2;
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for


        }

        if(Loudness < threshhold)
            Loudness = 0;



        //Smoothen on opening
        clipLoudness = Mathf.SmoothDamp(clipLoudness, 1f, ref Loudness, 3f);

        animator.SetFloat("Loudness", Loudness);

        print(Loudness);

    }
}
