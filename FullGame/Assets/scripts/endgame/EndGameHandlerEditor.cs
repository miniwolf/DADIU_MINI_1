using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HighScoreEndGame))]
public class EndGameHandlerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		EndGame myScript = (EndGame)target;

		if ( GUILayout.Button("Show Endgame") ) {
			myScript.CreateHighScore();
		}
	}
}
