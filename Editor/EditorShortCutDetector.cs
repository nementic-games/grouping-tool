/*
// Copyright (c) 2019 Nementic Games GmbH.
// This file is subject to the MIT License. 
// See the LICENSE file in the package root folder for more information.
// Author: Anja Haumann
* {^._.^}/
*/

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nementic.GroupingTool
{
    /// <summary>
    /// Detects shortcuts in the unity editor.
    /// ShortCut and Callback can be changed after the creation.
    /// Can only be used in the editor.
    /// </summary>
    public class EditorShortCutDetector
    {
        public KeyCode shortCut;
        public AdditionalKeyCode additionalShortCut;
        public System.Action actionToCall;

        /// <summary>
        /// Creates a shortCutDetector, witch listens for the given shortCut in the editor.
        /// </summary>
        /// <param name="actionToCall">Action that will be called, when the shortCut is detected.</param>
        /// <param name="shortCut">KeyCode to listen for.</param>
        /// <param name="additionalShortCut">Additional keyCodes to listen for, for example 'Control'.</param>
        public EditorShortCutDetector(System.Action actionToCall, KeyCode shortCut, AdditionalKeyCode additionalShortCut)
        {
            this.actionToCall = actionToCall;
            this.shortCut = shortCut;
            this.additionalShortCut = additionalShortCut;
        }

        /// <summary>
        /// Starts the detection.
        /// </summary>
        public void Init()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= DetectHierarchyShortCut;
            EditorApplication.hierarchyWindowItemOnGUI += DetectHierarchyShortCut;


            SceneView.onSceneGUIDelegate -= DetectSceneViewShortCut;
            SceneView.onSceneGUIDelegate += DetectSceneViewShortCut;
        }

        /// <summary>
        /// Detects key presses in the scene view.
        /// Must listen to the 'SceneView.onSceneGUIDelegate' callback.
        /// </summary>
        private void DetectSceneViewShortCut(SceneView sceneView)
        {
            bool shortCutDetected = DetectShortCut();
            if (shortCutDetected)
                this.actionToCall.Invoke();
        }

        /// <summary>
        /// Detects key presses in the hierarchy window.
        /// Must listen to the 'EditorApplication.hierarchyWindowItemOnGUI' callback.
        /// </summary>
        private void DetectHierarchyShortCut(int instanceID, Rect selectionRect)
        {
            if (instanceID == Selection.activeInstanceID)
            {
                bool shortCutDetected = DetectShortCut();
                if (shortCutDetected)
                    this.actionToCall.Invoke();
            }
        }

        /// <summary>
        /// Detects if the shortCut was pressed.
        /// </summary>
        private bool DetectShortCut()
        {
            bool shortCutPressed = false;
            var currentEvent = Event.current;

            // Is a key pressed?
            if (currentEvent.type == EventType.KeyDown)
            {
                // Is it our key?
                if (currentEvent.keyCode == shortCut)
                {
                    // Do we have an additional shortCut and if yes, is it also pressed?
                    if (additionalShortCut == AdditionalKeyCode.None)
                        shortCutPressed = true;
                    else if (additionalShortCut == AdditionalKeyCode.Control && currentEvent.control == true)
                        shortCutPressed = true;
                    else if (additionalShortCut == AdditionalKeyCode.Alt && currentEvent.alt == true)
                        shortCutPressed = true;
                }
            }

            return shortCutPressed;
        }
    } 
}
#endif