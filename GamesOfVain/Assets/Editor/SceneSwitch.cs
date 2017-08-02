using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneSwitch : EditorWindow
{
	static SceneSwitch window;
	static List<EditorBuildSettingsScene> allScenes;
	static int selectedSceneIndex = 0;
	static bool sceneSwiched = false;


	[MenuItem ("Utility/SceneSwitch %`")]
	static void Init ()
	{
		allScenes = new List<EditorBuildSettingsScene> ();
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
			if (EditorBuildSettings.scenes [i].enabled) {
				allScenes.Add (EditorBuildSettings.scenes [i]);
			}
		}
		window = (SceneSwitch)GetWindowWithRect (typeof (SceneSwitch), new Rect (Screen.width / 2 - 150, (Screen.height - (allScenes.Count * 25 + 35)) / 2, 300, allScenes.Count * 25 + 35), true);
		sceneSwiched = false;
		selectedSceneIndex = (selectedSceneIndex + 1) % allScenes.Count;
		window.ShowPopup ();
	}



	void GUIUpdate ()
	{
		GUILayout.Space (20);
		GUIStyle style = new GUIStyle ();
		style.fontSize = 20;
		style.alignment = TextAnchor.MiddleCenter;

		for (int i = 0; i < allScenes.Count; i++) {
			string sceneName = Path.GetFileNameWithoutExtension (allScenes [i].path);
			sceneName += sceneName == SceneManager.GetActiveScene ().name ? "(Active)" : "";
			if (i == selectedSceneIndex)
				style.fontStyle = FontStyle.Bold;
			else
				style.fontStyle = FontStyle.Normal;
			EditorGUILayout.LabelField (sceneName, style);
			GUILayout.Space (10);

		}
	}

	void OnGUI ()
	{
		GUIUpdate ();
		Event e = Event.current;

#if UNITY_EDITOR_OSX
		if (!e.command && !sceneSwiched) {
#else
		if (!e.control && !sceneSwiched) {
#endif
			sceneSwiched = true;
			EditorSceneManager.SaveScene (SceneManager.GetActiveScene ());
			EditorSceneManager.OpenScene (allScenes [selectedSceneIndex].path, OpenSceneMode.Single);
		}
	}

	void OnInspectorUpdate ()
	{
		Repaint ();

		if (sceneSwiched)
			this.Close ();
	}
}