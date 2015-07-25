using UnityEngine;
using System.Collections;

public abstract class AbstractSoundPicker
{
	protected AudioSource source;
	protected AudioClip[] sampleset;
	public float volume = 1f;
	public AbstractSoundPicker(AudioSource source, AudioClip[] sampleset) {
		this.source = source;
		this.sampleset = sampleset;
	}
	public abstract void PlayNext();
}