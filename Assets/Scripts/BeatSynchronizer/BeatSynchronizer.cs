﻿using UnityEngine;
using System.Collections;

/// <summary>
/// This class should be attached to the audio source for which synchronization should occur, and is 
/// responsible for synching up the beginning of the audio clip with all active beat counters and pattern counters.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BeatSynchronizer : MonoBehaviour {

	public float bpm = 120f;		// Tempo in beats per minute of the audio clip.
	public float startDelay = 1f;	// Number of seconds to delay the start of audio playback.
	public delegate void AudioStartAction(double syncTime);
	public static event AudioStartAction OnAudioStart;

	public float dspDelay;
	public AudioSource audioSource;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void StartMusic()
	{
		double initTime = AudioSettings.dspTime;
		audioSource.PlayScheduled(initTime + startDelay);
		if (OnAudioStart != null) {
			OnAudioStart(initTime + startDelay);
		}
	}

	public double pauseTime;

	public void Pause()
	{
		audioSource.Pause();
		pauseTime = AudioSettings.dspTime;
	}
	
	public void Resume()
	{
		audioSource.UnPause();
	}
}
