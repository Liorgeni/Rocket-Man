using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelayTime = 2f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;


    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDisabled = false;
void Start ()
{
    audioSource = GetComponent<AudioSource>();
}

void Update ()
{
    CheatCode();

}

void OnCollisionEnter (Collision other)
{
    if (isTransitioning || isCollisionDisabled)
    return;

    switch (other.gameObject.tag)
    {
        
        case  "Friendly":
        Debug.Log("This think is friendly!");
        break;
   
        case  "Finish":
        StartNextLevelSequence();
        break;

        default:
        StartCrashSequence();
        break;
    }
}


void CheatCode()
{
    if ( Input.GetKey(KeyCode.L))
        LoadNextLevel();
    if ( Input.GetKey(KeyCode.C))
    {
    isCollisionDisabled = !isCollisionDisabled;
        Debug.Log("Collision disablled");
    }
    
    }
        



void StartCrashSequence()
{

    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(deathSound);
    crashParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelDelayTime);    
}

void StartNextLevelSequence()
{
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(winSound);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelDelayTime);    
}

void ReloadLevel()
{
    int currentSceneIdx  = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIdx);

}

void LoadNextLevel()

{
         int currentSceneIdx  = SceneManager.GetActiveScene().buildIndex;
         int nextSceneIdx = currentSceneIdx + 1;
         if (nextSceneIdx == SceneManager.sceneCountInBuildSettings)
         {
            nextSceneIdx = 0;
         }
          SceneManager.LoadScene(nextSceneIdx);
}
}
