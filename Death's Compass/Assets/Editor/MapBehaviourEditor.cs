using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MapBehaviour))]
public class MapBehaviourEditor : Editor {
	private int collapseAreaSize = 6;

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		MapBehaviour mapBehaviour = (MapBehaviour)target;

		if (GUILayout.Button("Create module data")) {
			mapBehaviour.CreateModules();
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		if (GUILayout.Button("Simplify module data")) {
			mapBehaviour.CreateModules();
			mapBehaviour.SimplifyNeighborData();
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		GUILayout.Space(20f);

		if (GUILayout.Button("Clear")) {
			mapBehaviour.Clear();
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		GUILayout.BeginHorizontal();
		int.TryParse(GUILayout.TextField(this.collapseAreaSize.ToString()), out this.collapseAreaSize);

		if (GUILayout.Button("Initialize " + this.collapseAreaSize + "x" + this.collapseAreaSize + " area")) {
			mapBehaviour.Initialize();
			mapBehaviour.Map.Collapse(Vector3i.zero, new Vector3i(this.collapseAreaSize, mapBehaviour.Map.Height, this.collapseAreaSize), true);
			mapBehaviour.BuildAllSlots();
		}
		GUILayout.EndHorizontal();
	}
}
