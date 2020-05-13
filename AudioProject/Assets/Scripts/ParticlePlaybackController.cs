using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlaybackController : MonoBehaviour
{
    private ParticleSystem ps;

    [Range(1f, 20f)]
    public float multiplier;

    [Range(0f, 1f)]
    public float dampValue;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;

        int numPartitions = 1;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = AudioPeer.numBins / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

        for (int i = 0; i < numDisplayedBins; i++)
        {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData[i] / (AudioPeer.numBins / numPartitions);
            }
            else
            {
                partitionIndx++;
                i--;
            }
        }

        // scale and bound the average magnitude.
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = 0.5f + aveMag[i] * 100;
            if (aveMag[i] > 100)
            {
                aveMag[i] = 100;
            }
        }

        float mag = aveMag[0];

        if (mag >= 1.15f)
        {
            main.simulationSpeed += (mag * multiplier);
            //Debug.Log("Changing Playback Speed: " + mag * multiplier);
            if(ColorChanger.waveHeight < 10f)
            {
                ColorChanger.waveHeight += 0.2f;
            }
            ColorChanger.perlinScale += 0.15f;
        }

        if (main.simulationSpeed > 1f)
        {
            main.simulationSpeed -= dampValue;
        }

        if(ColorChanger.waveHeight >= 2f)
        {
            ColorChanger.waveHeight -= 0.05f;
        }

        if (ColorChanger.perlinScale >= 4.56f)
        {
            ColorChanger.perlinScale -= 0.01f;
        }
    }
}
