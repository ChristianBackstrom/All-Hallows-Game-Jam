using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
public class Sound : ScriptableObject
{
	public string title;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;

	public bool randPitch;
	[Range(.1f, 3f)]
	public float randPitchMin;
	[Range(.1f, 3f)]
	public float randPitchMax;

	[Space(10)]
	public bool loop;


	[HideInInspector]
	public AudioSource source;

	private void OnValidate()
	{
		if (randPitchMax < randPitchMin)
			randPitchMax = randPitchMin;

		title = name;
	}
}

#region Editor
#if UNITY_EDITOR

[CustomEditor(typeof(Sound))]
public class SoundEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.CreateInspectorGUI();

		Sound sound = (Sound)target;

		DrawField(sound);

	}

	private void DrawField(Sound sound)
	{
		EditorGUILayout.Space(10);
		EditorGUILayout.LabelField(sound.name, EditorStyles.boldLabel);
		EditorGUILayout.Space(10);

		EditorGUILayout.LabelField("Audio Clip", EditorStyles.boldLabel);
		sound.clip = (AudioClip)EditorGUILayout.ObjectField(sound.clip, typeof(AudioClip), false);

		EditorGUILayout.Space(10);

		EditorGUILayout.LabelField("Volume Settings", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.LabelField("Volume", GUILayout.Width(50));
		sound.volume = EditorGUILayout.Slider(sound.volume, 0f, 1f);

		EditorGUILayout.LabelField("Pitch", GUILayout.Width(50));
		sound.pitch = EditorGUILayout.Slider(sound.pitch, .1f, 3f);

		EditorGUILayout.EndHorizontal();


		EditorGUILayout.Space(10);

		EditorGUILayout.LabelField("Random Pitch Settings", EditorStyles.boldLabel);
		sound.randPitch = EditorGUILayout.Toggle("Random Pitch", sound.randPitch);

		if (sound.randPitch)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Min", GUILayout.Width(50));
			sound.randPitchMin = EditorGUILayout.Slider(sound.randPitchMin, .1f, 3f);

			EditorGUILayout.LabelField("Max", GUILayout.Width(50));
			sound.randPitchMax = EditorGUILayout.Slider(sound.randPitchMax, .1f, 3f);

			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.Space(10);

		EditorGUILayout.LabelField("Loop Settings", EditorStyles.boldLabel);
		sound.loop = EditorGUILayout.Toggle("Loop", sound.loop);
	}
}

#endif
#endregion