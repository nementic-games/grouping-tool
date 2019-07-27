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
    /// Creates a right-click-menu-item to gameObjects to use the grouping tool.
    /// </summary>
    public static class GameObjectGroupingToolMenuItem
    {
        private static bool menuItemClicked = false;

        public static void Init()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        [MenuItem("GameObject/Group", false, -1)]
        static void RequestShowPopUpFromHierarchy()
        {
            menuItemClicked = true;
        }

        static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {

            if (instanceID == Selection.activeInstanceID)
            {
                if (menuItemClicked)
                {
                    menuItemClicked = false;
                    GameObjectGroupingTool.RequestGroupSelection();
                }
            }
        }
    } 
}
#endif