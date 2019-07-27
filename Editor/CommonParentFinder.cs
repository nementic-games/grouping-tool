/*
// Copyright (c) 2019 Nementic Games GmbH.
// This file is subject to the MIT License. 
// See the LICENSE file in the package root folder for more information.
// Author: Anja Haumann
* {^._.^}/
*/

using System.Collections.Generic;
using UnityEngine;

namespace Nementic.GroupingTool
{
    /// <summary>
    /// Finds a common parent of the given gameObjects.
    /// Can only be called in the unity editor.
    /// </summary>
    public static class CommonParentFinder
    {
        /// <summary>
        /// Findest the deepest common parent of the given gameObjects. 
        /// -> the first parent that all gameObjects share.
        /// </summary>
        public static Transform FindDeepest(GameObject[] gameObjects)
        {
            if (gameObjects.Length == 0)
                return null;
            if (gameObjects.Length == 1)
                return gameObjects[0].transform.parent;

            List<Transform> roots = GetRoots(gameObjects);
            // They can't have common parents if the root is different.
            if (roots.Count != 1)
                return null;
            else // Find the most deep common parent. (The first parent that the objects share.)
            {
                Transform currentParent = gameObjects[0].transform.parent;
                Transform currentChild;
                bool parentFound = true;

                while (currentParent != null)
                {
                    // Check if all objects are childs of the current parent.
                    parentFound = true;
                    for (int i = 0; i < gameObjects.Length; i++)
                    {
                        currentChild = gameObjects[i].transform;
                        if (currentChild == currentParent || currentChild.IsChildOf(currentParent) == false)
                        {
                            parentFound = false;
                            break;
                        }
                    }

                    // Did we find a common parent?
                    if (parentFound)
                        return currentParent;

                    currentParent = currentParent.parent;
                }
            }

            // We should never reach this point, because we are sure, 
            // that the gameObjects at least share there roots.
            return null;
        }

        /// <summary>
        /// Returns a list of the root transforms of the given gameObjects. 
        /// Each root transform will only be added once to the list. 
        /// </summary>
        private static List<Transform> GetRoots(GameObject[] gameObjects)
        {
            List<Transform> roots = new List<Transform>();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                Transform root = gameObjects[i].transform.root;
                if (roots.Contains(root) == false &&
                    ContainsGameObject(gameObjects, root.gameObject) == false)
                    roots.Add(root);
            }
            return roots;
        }

        /// <summary>
        /// Returns wether the given gameObject is contained in the gameObject array.
        /// </summary>
        private static bool ContainsGameObject(GameObject[] gameObjects, GameObject gameObject)
        {
            bool contains = false;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                var current = gameObjects[i];
                if (current == gameObject)
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
    }
}