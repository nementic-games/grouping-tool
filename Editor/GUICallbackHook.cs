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
    /// Hooks into the gui callback of the hierarchy and scene view.
    /// Can only be used in the editor.
    /// </summary>
    public class GUICallbackHook
    {
        public System.Action actionToCall;
        private bool execute;

        /// <summary>
        /// Starts the detection.
        /// </summary>
        public GUICallbackHook()
        {
            // Using both callbacks ensures that the tool works even when the scene view or the hierarchy is not open.
            EditorApplication.hierarchyWindowItemOnGUI -= ExecuteInHierarchyGUI;
            EditorApplication.hierarchyWindowItemOnGUI += ExecuteInHierarchyGUI;

            SceneView.beforeSceneGui -= ExecuteInSceneViewGUI;
            SceneView.beforeSceneGui += ExecuteInSceneViewGUI;
        }

        public void ExecuteInNextGUI()
        {
            execute = true;
        }

        /// <summary>
        /// Executes the given action in the scene view gui callback.
        /// </summary>
        private void ExecuteInSceneViewGUI(SceneView sceneView)
        {
            if (execute)
            {
                execute = false;

                // Popup window throws an exit gui exception which breaks the scene view gui phase.
                try
                {
                    actionToCall.Invoke();
                }
                catch (ExitGUIException)
                { }
            }
        }

        /// <summary>
        /// Executes the given action in the scene view gui callback.
        /// </summary>
        private void ExecuteInHierarchyGUI(int instanceID, Rect selectionRect)
        {
            if (instanceID == Selection.activeInstanceID)
            {
                if (execute)
                {
                    execute = false;
                    actionToCall.Invoke();
                }
            }
        }
    }
}
#endif