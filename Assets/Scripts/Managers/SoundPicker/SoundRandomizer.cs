using UnityEngine;
using System.Collections;

public class SoundRandomizer : AbstractSoundPicker
{
	public SoundRandomizer(AudioSource source, AudioClip[] samples) : base(source, samples) {}

	public override void PlayNext ()
	{
		int index = (int)(Random.Range (0, sampleset.Length));
		if (index >= 0 && index < sampleset.Length) {
			source.PlayOneShot (sampleset[index], volume);
		}
	}
}

