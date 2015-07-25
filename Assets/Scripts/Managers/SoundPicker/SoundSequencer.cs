using UnityEngine;
using System.Collections;

public class SoundSequencer : AbstractSoundPicker
{
	private int currentIndex;
	public SoundSequencer(AudioSource source, AudioClip[] samples) : base(source, samples) {
		currentIndex = 0;
	}
	
	public override void PlayNext ()
	{

		source.PlayOneShot (sampleset[currentIndex], volume);
		currentIndex++;
		if (currentIndex >= sampleset.Length) {
			currentIndex = 0;
		}
		
	}
}
