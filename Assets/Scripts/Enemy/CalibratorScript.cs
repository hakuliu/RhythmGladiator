using UnityEngine;
using System.Collections;

public class CalibratorScript : MonoBehaviour
{
	public Color colorSwitch0;
	public Color colorSwitch1;

	public AudioClip minorSound;
	public AudioClip majorSound;

	private AudioSource asource;
	private Light colswap;
	private Color currentColor;

	// Use this for initialization
	void Start ()
	{
		asource = GetComponent<AudioSource> ();
		colswap = GetComponent<Light> ();
		currentColor = colorSwitch0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void playCalibrationSoundMinor() {
		asource.PlayOneShot (minorSound);
	}
	public void playCalibrationSoundMajor() {
		asource.PlayOneShot (majorSound);
	}
	public void toggleColor() {
		if (currentColor == colorSwitch0) {
			currentColor = colorSwitch1;
			colswap.color = colorSwitch1;
		} else {
			currentColor = colorSwitch0;
			colswap.color = colorSwitch0;
		}
	}
}

