using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField]Vector3 MovementVector;
    float MovementFactor;
    [SerializeField]float period =10f;


    // Start is called before the first frame update
    void Start()
    {
        StartingPosition= transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period<=Mathf.Epsilon){return;}
        float cycles=Time.time/period;
        const float tau = Mathf.PI*2;
        float sineWave=Mathf.Sin(cycles*tau);
        MovementFactor=(sineWave+1f)/2f;
        
        Vector3 offset=MovementVector*MovementFactor;
        transform.position= StartingPosition+offset;
    }
}
