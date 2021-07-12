using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float LoadLevelDelay=2f;
    [SerializeField]AudioClip success;
    [SerializeField]AudioClip crash;
    [SerializeField]ParticleSystem successParticle;
    [SerializeField]ParticleSystem crashParticle;
    bool CollisionDisable=false;
    bool isTransitioning;
    AudioSource audioSource;
     void Start(){
        audioSource=GetComponent<AudioSource>();
    }
    void Update(){
        respondToDebugKeys();
    }
    void respondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            nextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            CollisionDisable=!CollisionDisable;
        }
    }
     void OnCollisionEnter(Collision other) {
        if(isTransitioning||CollisionDisable){
            return;
        }
        switch(other.gameObject.tag){
            case "Friendly":
            Debug.Log("this is Friendly");
            break;
            case "Finish":
            startSuccessSequence();
            break;
            default:
            startCrashSequence();
            break;
        }
    }
    void startSuccessSequence(){
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("nextLevel",LoadLevelDelay);

    }
    void startCrashSequence(){
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel",LoadLevelDelay);
    }
    void nextLevel(){
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex= currentSceneIndex+1;
        
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings){
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        

    }
    void ReloadLevel(){
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
     