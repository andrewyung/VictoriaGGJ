using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private int audioSourceNumber = 2;

    private AudioSource[] audioSources;

	// Use this for initialization
	void Start () {
        //initialize audio sources
        audioSources = new AudioSource[audioSourceNumber];
		for (int i = 0; i < audioSourceNumber; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
	}

    void playAudioClip(int clipNumber)
    {
        if (audioClips.Length > 0 && //audioClips isnt empty
            clipNumber < audioClips.Length && clipNumber >= 0)//clipNumber is within bounds
        {
            for (int i = 0; i < audioSourceNumber; i++)
            {
                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].clip = audioClips[clipNumber];
                    audioSources[i].Play();
                    break;
                }
            }
        }
    }

    string getAudioClipName(int clipNumber)
    {
        return audioClips[clipNumber].name;
    }
}
