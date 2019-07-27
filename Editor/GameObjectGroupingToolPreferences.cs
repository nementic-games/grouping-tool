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
    /// Handles the preferences of the grouping tool in the player prefs.
    /// </summary>
    public static class GameObjectGroupingToolPreferences
    {
        private static bool preferencesLoaded = false;

        // Keys for loading the preferences from the player prefs.
        private static string showPopUpString = "Show Popup";

        private static string useDefaultLableKey = "Use Default Label";
        private static string defaultLableTypeKey = "Default Label Type";
        private static string defaultLableColorKey = "Default Label Color";

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider("Nementic/Grouping Tool", SettingsScope.User)
            {
                guiHandler = (s) =>
                {
                    PreferencesGUI();
                }
            };
        }

        public static void PreferencesGUI()
        {
            LoadPreferences();

            // Draw GUI
            GameObjectGroupingTool.showPopUp = EditorGUILayout.Toggle(
                "Show Naming Popup",
                GameObjectGroupingTool.showPopUp
                );

            EditorGUILayout.Space();
            GameObjectGroupingTool.useDefaultLable = EditorGUILayout.Toggle(
                useDefaultLableKey,
                GameObjectGroupingTool.useDefaultLable
                );
            GameObjectGroupingTool.defaultIconType = (GameObjectLabler.Type)EditorGUILayout.EnumPopup(
                defaultLableTypeKey,
                GameObjectGroupingTool.defaultIconType
                );
            GameObjectGroupingTool.defaultIconColor = (GameObjectLabler.Color)EditorGUILayout.EnumPopup(
                defaultLableColorKey,
                GameObjectGroupingTool.defaultIconColor
                );

            // Save new preferences.
            if (GUI.changed)
            {
                EditorPrefs.SetBool(showPopUpString, GameObjectGroupingTool.showPopUp);
                EditorPrefs.SetBool(useDefaultLableKey, GameObjectGroupingTool.useDefaultLable);
                EditorPrefs.SetInt(defaultLableTypeKey, (int)GameObjectGroupingTool.defaultIconType);
                EditorPrefs.SetInt(defaultLableColorKey, (int)GameObjectGroupingTool.defaultIconColor);
            }
        }

        [InitializeOnLoadMethod]
        private static void LoadPreferences()
        {
            if (preferencesLoaded == false)
            {
                GameObjectGroupingTool.showPopUp = EditorPrefs.GetBool(showPopUpString, true);

                GameObjectGroupingTool.useDefaultLable = EditorPrefs.GetBool(useDefaultLableKey, true);
                GameObjectGroupingTool.defaultIconType =
                    (GameObjectLabler.Type)
                    EditorPrefs.GetInt(defaultLableTypeKey, (int)GameObjectLabler.Type.Label);
                GameObjectGroupingTool.defaultIconColor =
                    (GameObjectLabler.Color)
                    EditorPrefs.GetInt(defaultLableColorKey, (int)GameObjectLabler.Color.Gray);

                preferencesLoaded = true;
            }
        }
    }
}
#endif