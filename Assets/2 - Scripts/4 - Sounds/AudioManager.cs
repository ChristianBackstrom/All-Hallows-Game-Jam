using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	[SerializeField] private Sound[] _sounds; // The array of all the sounds

	private Sound _introSound; // The intro sound

	private bool _isPlayingIntro = true; // Is the intro playing

	void Awake()
	{
		// If there is no instance of the AudioManager, set it to this
		#region Singleton
		if (Instance == null)
			Instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		#endregion

		// setups all the Sounds
		#region  Audio Setup
		foreach (Sound s in _sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
		#endregion

	}

	private void Start()
	{
		Play("BossIntro");
	}

	private void Update()
	{
		if (!_introSound.source.isPlaying && _isPlayingIntro)
		{
			Play("BossLoop");
			_isPlayingIntro = false;
		}
	}

	// Plays a sound using a string
	#region PlaySound
	public void Play(string title)
	{
		Sound s = System.Array.Find(_sounds, sound => sound.title == title);

		if (s == null)
		{
			Debug.LogWarning("Sound: " + title + " not found!");
			return;
		}

		if (s.title == "BossIntro")
			_introSound = s;

		if (s.randPitch)
			s.source.pitch = Random.Range(s.randPitchMin, s.randPitchMax);

		s.source.Play();
	}
	#endregion

	// Stops a playing sound using a string
	#region StopSound
	public void Stop(string title)
	{
		Sound s = System.Array.Find(_sounds, sound => sound.title == title);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + title + " not found!");
			return;
		}

		if (!s.source.isPlaying)
			return;
		s.source.Stop();
	}
	#endregion

	// Stops all sounds playing
	#region StopAllSounds
	public void StopAll()
	{
		foreach (Sound s in _sounds)
		{
			if (!s.source.isPlaying)
				continue;
			s.source.Stop();
		}
	}

	public void StopAll(string title)
	{
		foreach (Sound s in _sounds)
		{
			if (s.title == title)
				continue;
			if (!s.source.isPlaying)
				continue;
			s.source.Stop();
		}
	}
	#endregion
}
