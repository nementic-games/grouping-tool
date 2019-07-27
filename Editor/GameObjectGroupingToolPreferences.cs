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
        private static string shortCutKey = "Shortcut";
        private static string additionalShortCutKey = "Additional Shortcut";
        private static string showPopUpString = "Show Popup";

        private static string useDefaultLableKey = "Use Default Label";
        private static string defaultLableTypeKey = "Default Label Type";
        private static string defaultLableColorKey = "Default Label Color";

        [PreferenceItem("Nementic/Grouping Tool")]
        public static void PreferencesGUI()
        {
            LoadPreferences();

            // Draw GUI
            GameObjectGroupingTool.showPopUp = EditorGUILayout.Toggle(
                "Show Naming Popup",
                GameObjectGroupingTool.showPopUp
                );

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Editor Shortcut");
            GameObjectGroupingTool.AdditionalShortCut = (AdditionalKeyCode)EditorGUILayout.EnumPopup(GameObjectGroupingTool.AdditionalShortCut);
            GameObjectGroupingTool.ShortCut = (KeyCode)EditorGUILayout.EnumPopup(GameObjectGroupingTool.ShortCut);
            EditorGUILayout.EndHorizontal();

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
                EditorPrefs.SetInt(shortCutKey, (int)GameObjectGroupingTool.ShortCut);
                EditorPrefs.SetInt(additionalShortCutKey, (int)GameObjectGroupingTool.AdditionalShortCut);
                EditorPrefs.SetBool(showPopUpString, GameObjectGroupingTool.showPopUp);
                EditorPrefs.SetBool(useDefaultLableKey, GameObjectGroupingTool.useDefaultLable);
                EditorPrefs.SetInt(defaultLableTypeKey, (int)GameObjectGroupingTool.defaultIconType);
                EditorPrefs.SetInt(defaultLableColorKey, (int)GameObjectGroupingTool.defaultIconColor);
            }
        }

        [InitializeOnLoadMethod]
        private static void LoadPreferences()
        {
            // Load preferences
            if (preferencesLoaded == false)
            {
                GameObjectGroupingTool.ShortCut = (KeyCode)EditorPrefs.GetInt(shortCutKey, (int)KeyCode.G);
                GameObjectGroupingTool.AdditionalShortCut = (AdditionalKeyCode)EditorPrefs.GetInt(additionalShortCutKey, (int)AdditionalKeyCode.Control);

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