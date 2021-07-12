using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float mainThrust=100f;
    [SerializeField]float rotateSpeed=100f;
    [SerializeField]AudioClip mainEngine;
    [SerializeField]ParticleSystem MainBooster;
    [SerializeField]ParticleSystem LeftBooster;
    [SerializeField]ParticleSystem RightBooster;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processRotation();
    }
    void processThrust(){
        if (Input.GetKey(KeyCode.Space)){ 
            StartThrust();
        }
        else
        {
            StopThrusting();
        }
    }
    void processRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }
    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!MainBooster.isPlaying)
        {
            MainBooster.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        MainBooster.Stop();
    }

     void StopRotation()
    {
        RightBooster.Stop();
        LeftBooster.Stop();
    }

     void RotateRight()
    {
        applyRotation(-rotateSpeed);
        if (!LeftBooster.isPlaying)
        {
            LeftBooster.Play();
        }
    }

    void RotateLeft()
    {
        applyRotation(rotateSpeed);
        if (!RightBooster.isPlaying)
        {
            RightBooster.Play();
        }
    }

    void applyRotation(float rotationvalue){
        transform.Rotate(Vector3.forward*rotationvalue*Time.deltaTime);
    }
}
    
