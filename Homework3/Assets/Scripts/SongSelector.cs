using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelector : MonoBehaviour
{
    public AudioSource audioS;

    public AudioClip ac1;
    public AudioClip ac2;
    public AudioClip ac3;

    // Start is called before the first frame update
    void Start()
    {
        audioS.clip = ac1;
        audioS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && audioS.clip != ac1)
        {
            audioS.clip = ac1;
            audioS.Play();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && audioS.clip != ac2)
        {
            audioS.clip = ac2;
            audioS.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && audioS.clip != ac3)
        {
            audioS.clip = ac3;
            audioS.Play();
        }
    }
}
