/*
// Copyright (c) 2019 Nementic Games GmbH.
// This file is subject to the MIT License. 
// See the LICENSE file in the package root folder for more information.
// Author: Anja Haumann
* {^._.^}/
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nementic.GroupingTool
{
	[InitializeOnLoad]
	public static class GameObjectGroupingTool
	{
		public static bool showPopUp = true;
		public static bool useDefaultLable = true;

		public static GameObjectLabler.Type defaultIconType = GameObjectLabler.Type.Label;
		public static GameObjectLabler.Color defaultIconColor = GameObjectLabler.Color.Gray;

		public static GroupPosition lastGroupPosition = GroupPosition.Centered;

		private static GUICallbackHook callbackHook;

		/// <summary>
		/// Will be created by the editor. (InitializeOnLoad)
		/// </summary>
		static GameObjectGroupingTool()
		{
			GameObjectGroupingToolMenuItem.Init();

			callbackHook = new GUICallbackHook
			{
				actionToCall = RequestGroupSelection
			};
		}

		[Shortcut("Nementic/Grouping Tool/Group", KeyCode.G, ShortcutModifiers.Action)]
		private static void UnityShortcutManagerTriggered()
		{
			// The callback hook is needed because the naming popup can only be triggered in a GUI callback.
			callbackHook.ExecuteInNextGUI();
		}

		/// <summary>
		/// Displays a naming popUp before grouping the selection. 
		/// The popUp can be disabled in the preferences.
		/// This function MUST be called from a GUI callback!
		/// </summary>
		public static void RequestGroupSelection()
		{
			if (showPopUp)
				ShowPopUp();
			else
				GroupSelection(lastGroupPosition, "New Group");
		}

		/// <summary>
		/// Groups the currently selected gameObjects under a new parent gameObject with the given name.
		/// </summary>
		/// <param name="groupName">Name of the new parent.</param>
		public static void GroupSelection(GroupPosition groupPosition, string groupName)
		{
			GameObject[] selectedObjects = Selection.gameObjects;

			if (selectedObjects.Length == 0)
				return;

			// Create Parent.
			GameObject parentObject = CreateParentObject(selectedObjects, groupPosition, groupName);
			string parentingUndoDisplayMessage = selectedObjects.Length > 1 ?
				"Parent GameObjects" : "Parent GameObject";

			// Parent selection.
			for (int i = 0; i < selectedObjects.Length; i++)
			{
				GameObject current = selectedObjects[i];
				Undo.SetTransformParent(current.transform, parentObject.transform, parentingUndoDisplayMessage);
			}

			Selection.activeGameObject = parentObject;

			// Save the changed group position.
			lastGroupPosition = groupPosition;
			GameObjectGroupingToolPreferences.SaveGroupingToolSettings();
		}

		/// <summary>
		/// Shows a pop up to change the properties of the group.
		/// This function can only be called from a GUI callback, because Event.current
		/// is only available in GUI callbacks. 
		/// </summary>
		private static void ShowPopUp()
		{
			if (Event.current != null)
			{
				Vector2 screenPos = Event.current.mousePosition;
				PopupWindow.Show(new Rect(screenPos.x, screenPos.y, 0, 0), new OptionsPopUp());
			}
			else
				Debug.LogError("ShowPopUp() must be called from a GUI callback!");
		}

		/// <summary>
		/// Creates a parent gameObject for the given gameObjects. The future childs will not be parented.
		/// </summary>
		private static GameObject CreateParentObject(GameObject[] futureChilds, GroupPosition groupPosition, string name = "NewGroup")
		{
			GameObject parentObject = new GameObject(name);
			Transform groupParent = CommonParentFinder.FindDeepest(futureChilds);

			if (groupParent != null)
				parentObject.transform.SetParent(groupParent);
			else
			{
				// If it's a root object make sure it's spawned in the correct scene.
				Scene targetScene = futureChilds[0].scene;
				if (parentObject.scene != targetScene)
					SceneManager.MoveGameObjectToScene(parentObject, targetScene);
			}

			int groupSiblingIndex = CommonParentFinder.GetGroupSiblingIndex(futureChilds, groupParent);
			parentObject.transform.SetSiblingIndex(groupSiblingIndex);

			Vector3 loalPos = groupPosition == GroupPosition.LocalOrigin ? Vector3.zero : GetCenterPosition(futureChilds);
			parentObject.transform.localPosition = loalPos;

			if (useDefaultLable)
			{
				GameObjectLabler.AddLable(
					parentObject,
					defaultIconType,
					defaultIconColor
					);
			}

			Undo.RegisterCreatedObjectUndo(parentObject, "Create Group GameObject");
			return parentObject;
		}

		/// <summary>
		/// Gets the position which lies in the center of the given gameObjects.
		/// </summary>
		private static Vector3 GetCenterPosition(GameObject[] gameObjects)
		{
			if (gameObjects.Length == 0)
				return Vector3.zero;
			if (gameObjects.Length == 1)
				return gameObjects[0].transform.position;

			var bounds = new Bounds(gameObjects[0].transform.position, Vector3.zero);
			for (var i = 1; i < gameObjects.Length; i++)
				bounds.Encapsulate(gameObjects[i].transform.position);
			return bounds.center;
		}
	}

	public enum GroupPosition
	{
		Centered,
		LocalOrigin
	}
}
#endif
