using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerr : MonoBehaviour {

    [SerializeField]
    private AudioClip[] audioClipss;

    [SerializeField]
    private int audioSourceNumberr = 2;

    private AudioSource[] audioSourcess;

	// Use this for initialization
	void Start () {
        //initialize audio sources
        audioSourcess = new AudioSource[audioSourceNumberr];
		for (int i = 0; i < audioSourceNumberr; i++)
        {
            audioSourcess[i] = gameObject.AddComponent<AudioSource>();
        }
	}

    void playAudioClip(int clipNumber)
    {
        if (audioClipss.Length > 0 && //audioClips isnt empty
            clipNumber < audioClipss.Length && clipNumber >= 0)//clipNumber is within bounds
        {
            for (int i = 0; i < audioSourceNumberr; i++)
            {
                if (!audioSourcess[i].isPlaying)
                {
                    audioSourcess[i].clip = audioClipss[clipNumber];
                    audioSourcess[i].Play();
                    break;
                }
            }
        }
    }

    string getAudioClipName(int clipNumber)
    {
        return audioClipss[clipNumber].name;
    }
}
