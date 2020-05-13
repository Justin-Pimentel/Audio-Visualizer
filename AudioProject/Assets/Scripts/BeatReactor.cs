using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatReactor : MonoBehaviour
{
    public GameObject cubePrefab;
    public float distance;

    // Use this for initialization
    void Start()
    {
        float spacedAngle = 360f / (float)(AudioPeer.numBins / 2);
        Debug.Log(spacedAngle);

        for (int i = 0; i < (AudioPeer.numBins / 2); i++)
        {
            GameObject inCube = (GameObject)Instantiate(cubePrefab);
            inCube.transform.position = this.transform.position;
            inCube.transform.parent = this.transform;
            inCube.name = "Cube" + i;
            transform.eulerAngles = new Vector3(0, (-spacedAngle * i), 0);
            inCube.transform.position = Vector3.forward * distance;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // this rotates each cube.
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

        // --------------------------------------------------------
        // ------- animate the cube size based on spectrum data.

        // consolidate spectral data to 8 partitions (1 partition for each rotating cube)
        int numPartitions = 128;
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
            aveMag[i] = 0.5f + aveMag[i] * 500;
            if (aveMag[i] > 50)
            {
                aveMag[i] = 50;
            }
        }

        float mag = aveMag[0];

        // Map the magnitude to the cubes based on the cube name
        for (int i = 0; i < numPartitions; i++)
        {
            GameObject currChild = this.transform.GetChild(i).gameObject;
            //int index = i + 1;
            if (currChild.name == "Cube" + i)
            {
                currChild.transform.localScale = new Vector3(1, 1, aveMag[i]);
            }
        }

        // --------- End animating cube via spectral data
        // --------------------------------------------------------



    }


}
