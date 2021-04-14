using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeLoadingScene = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successLandingSFX;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successLandingParticles;

    Cheats cheats;
    AudioSource myAudioSource;

    bool isTransitioning = false;

    void Start()
    {
        cheats = FindObjectOfType<Cheats>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || !cheats.GetCollisionState()) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You touched friendly object!");
                break;
            case "Finish":
                StartLoadNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), delayBeforeLoadingScene);
    }

    void StartLoadNextLevelSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(successLandingSFX);
        successLandingParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), delayBeforeLoadingScene);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
