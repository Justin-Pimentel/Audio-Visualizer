using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticleSeedGenerator : MonoBehaviour
{

    public ParticleSystem[] setSeedParticles;
    public int ParticleSeed;
    public bool forceNewSeed;

    /*private void Awake()
    {
        if (setSeedParticles.Length > 0)
        {
            for (int i = 0; i < setSeedParticles.Length; i++)
            {
                setSeedParticles[i].randomSeed = (uint)ParticleSeed;
            }
        }
    }*/

    void Update()
    {
        if (forceNewSeed)
        {
            ParticleSeed = Random.Range(0, int.MaxValue);
            Debug.Log("Particle seed: " + ParticleSeed);

            if (setSeedParticles.Length > 0)
            {
                for (int i = 0; i < setSeedParticles.Length; i++)
                {
                    setSeedParticles[i].randomSeed = (uint)ParticleSeed;
                }
            }

            forceNewSeed = false;
        }
    }
}
