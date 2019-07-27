/*
// Copyright (c) 2019 Nementic Games GmbH.
// This file is subject to the MIT License. 
// See the LICENSE file in the package root folder for more information.
// Author: Anja Haumann
* {^._.^}/
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nementic.GroupingTool
{
    /// <summary>
    /// Pop up that changes the properties of the group.
    /// </summary>
    public class OptionsPopUp : PopupWindowContent
    {
        public readonly static float windowWidth = 250;
        public readonly static float windowHeight = 90;
        private readonly static int windowPaddingInPixel = 10;
        private readonly static int rowPaddingInPixel = 5;

        private static string defaultGroupName = "New Group";
        private static string currentGroupName;

        bool focused = false;

        public override Vector2 GetWindowSize()
        {
            return new Vector2(windowWidth, windowHeight);
        }

        public override void OnGUI(Rect rect)
        {
            // Add window padding
            GUILayout.BeginHorizontal();
            GUILayout.Space(windowPaddingInPixel);
            GUILayout.BeginVertical();
            GUILayout.Space(windowPaddingInPixel);

            // Draw header
            GUILayout.Label("GameObject Group Options", EditorStyles.boldLabel);
            GUILayout.Space(rowPaddingInPixel);

            // Draw name field
            GUILayout.BeginHorizontal();
            GUILayout.Label("Group name");
            GUI.SetNextControlName("Group name");
            currentGroupName = EditorGUILayout.TextField(currentGroupName);
            GUILayout.EndHorizontal();
            GUILayout.Space(rowPaddingInPixel);

            // Draw group button and auto group on return.
            if (GUILayout.Button("Group") || (Event.current.isKey && Event.current.keyCode == KeyCode.Return))
            {
                GameObjectGroupingTool.GroupSelection(currentGroupName);
                editorWindow.Close();
            }

            // End window padding
            GUILayout.Space(windowPaddingInPixel);
            GUILayout.EndVertical();
            GUILayout.Space(windowPaddingInPixel);
            GUILayout.EndHorizontal();

            if (focused == false)
            {
                GUI.FocusControl("Group name");
                focused = true;
            }
        }

        public override void OnOpen()
        {
            focused = false;
            currentGroupName = defaultGroupName;
            base.OnOpen();
        }
    }
}
#endif