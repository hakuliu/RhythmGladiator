using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// This class allows anything that holds a reference to it to allow you to schedule a BeatEvent on the next Beat that happens
/// Primarily should be used to manage global events that happens, and needs to be timed with the BGM.
/// </summary>
public class BeatScheduler : MonoBehaviour
{
	/// <summary>
	/// The level BGM offset.
	/// the offset from when the bgm audio source starts and its first actual beat.
	/// </summary>
	public float startOffset;
	public Image heartImg;
	public float beatTime = .1f;
	public Color HeartBeatColor = Color.cyan;
	public Color HeartBeatMinorColor = new Color (169, 255, 212);
	public Color HeartBeatMajorColor = Color.red;
	public int beatsPerMeasure = 4;//how many beats per measure
	public int beatsTilFirstMeasure = 0;//depends on music load time
	public int bpm = 120;

	public CalibratorScript calibrator;//can be null.  if it's not null, we use it to do some debugging

	float timer;
	float lastBeat;
	bool beating;
	float lerptimer;
	int beatCounter;
	AudioSource bgmaudio;

	//I wanted to let anyone schedule next beat events so this is static
	private static ArrayList scheduledMeasureEvents = new ArrayList();

	// Use this for initialization
	public void Start ()
	{
		BeatManager.BPM = bpm;//sets the global from here, since bgm controls everything
		timer = 0;
		lastBeat = startOffset;
		beatCounter = -1 * beatsTilFirstMeasure;
		bgmaudio = GetComponent<AudioSource> ();
		bgmaudio.Play ();//this ensures that the script is sync'd with audio....maybe? :P
	}
	
	// Update is called once per frame
	public void FixedUpdate ()
	{
		float lastTime = timer;
		timer += Time.fixedDeltaTime;

		float nextBeat = lastBeat + BeatManager.TickTime;

		if(BeatManager.eventHappened(lastTime, timer, nextBeat)) {
			lastBeat = nextBeat;
			measureUpdate();
		}
	}

	void measureUpdate() {
		if (beatCounter == 0) {
			//do big beat
			BeatHeartMajor();
			triggerMeasureEvents();
		} else {
			BeatHeartMinor();
		}
		beatCounter++;
		if (beatCounter >= beatsPerMeasure) {
			beatCounter = 0;
		}
	}

	void Update() {
		if (beating && lerptimer <= 1f) {
			heartImg.color = Color.Lerp (HeartBeatColor, Color.white, lerptimer);
			lerptimer += .2f;
		} else {
			lerptimer = 0;
			beating = false;
		}
	}

	public static void ScheduleNextMeasure(BeatEvent e)
	{
		scheduledMeasureEvents.Add (e);
	}

	void BeatHeartMinor() {
		if (heartImg != null) {
			lerptimer = 0;
			beating = true;
			heartImg.color = HeartBeatMinorColor;
			HeartBeatColor = HeartBeatMinorColor;
		}
		if (calibrator != null) {
			calibrator.toggleColor();
			calibrator.playCalibrationSoundMinor();
		}
	}
	void BeatHeartMajor() {
		if (heartImg != null) {
			lerptimer = 0;
			beating = true;
			heartImg.color = HeartBeatMajorColor;
			HeartBeatColor = HeartBeatMajorColor;
		}
		if (calibrator != null) {
			calibrator.toggleColor();
			calibrator.playCalibrationSoundMajor();
		}
	}
	

	void triggerMeasureEvents()
	{
		//we make a copy because ArrayList (and foreach) won't handle concurrency correctly 
		//if we decided to change the ArrayList in the middle of the loop for example
		ArrayList copy = new ArrayList (scheduledMeasureEvents);
		//we made a copy already so we can goahead and flush it now.
		scheduledMeasureEvents.Clear ();
		foreach(BeatEvent ev in copy) {
			ev.doAction();
		}
	}
}

