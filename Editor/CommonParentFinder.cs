/*
// Copyright (c) 2019 Nementic Games GmbH.
// This file is subject to the MIT License. 
// See the LICENSE file in the package root folder for more information.
// Author: Anja Haumann
* {^._.^}/
*/

using System.Collections.Generic;
using System.Linq;
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
        /// Find the deepest common parent of the given GameObjects. 
        /// -> the first parent that all GameObjects share.
        /// </summary>
        public static Transform FindDeepest(GameObject[] gameObjects)
        {
            if (gameObjects == null || gameObjects.Length == 0)
                return null;

            if (gameObjects.Length == 1)
                return gameObjects[0].transform.parent;

            List<Transform> roots = GetRoots(gameObjects);

            // There can't be a common parent if the given 
            // GameObjects have no root/parent transforms.
            if (roots.Count != 1)
                return null;

            else // Find the most deep common parent. (The first parent that the objects share.)
            {
                Transform currentGameObject;
                Transform currentParent = gameObjects[0].transform.parent;

				while (currentParent != null)
                {
					// Check if all objects are children of the parent.
					bool parentFound = true;
					for (int i = 0; i < gameObjects.Length; i++)
                    {
                        currentGameObject = gameObjects[i].transform;
                        if (currentGameObject == currentParent || currentGameObject.IsChildOf(currentParent) == false)
                        {
                            parentFound = false;
                            break;
                        }
                    }

                    if (parentFound)
                        return currentParent;

                    currentParent = currentParent.parent;
                }
            }

            // This point should never be reached, 
            // because the GameObjects share at least there root.
            return null;
        }

        /// <summary>
        /// Returns the sibling index in the hierarchy which the group parent should have. 
        /// Beginning at the future children the hierarchy is searched upwards till the transforms
        /// right below the group parent is found. The smallest sibling  index of these transforms is returned.
        /// </summary>
        public static int GetGroupSiblingIndex(GameObject[] futureChilds, Transform groupParent)
        {
            int smallestChildIndex = int.MaxValue;

            for (int i = 0; i < futureChilds.Length; i++)
            {
                var current = futureChilds[i];

                // The parent of the current child is the group parent? Just take its sibling index.
                if (current.transform.parent == groupParent)
                {
                    int index = current.transform.GetSiblingIndex();
                    if (index < smallestChildIndex)
                        smallestChildIndex = index;
                }
                else
                {
                    // Move up till the gameObject right below the group parent is found.
                    Transform nextParentTransform = current.transform.parent;
                    while (nextParentTransform.parent != groupParent && nextParentTransform.parent != null)
                    {
                        nextParentTransform = nextParentTransform.parent;
                    }

                    // If the correct transform is found, check it's sibling index.
                    if (nextParentTransform.parent == groupParent)
                    {
                        int index = nextParentTransform.GetSiblingIndex();
                        if (index < smallestChildIndex)
                            smallestChildIndex = index;
                    }
                }
            }
            return smallestChildIndex;
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
                if (roots.Contains(root) == false && gameObjects.Contains(root.gameObject) == false)
                    roots.Add(root);
            }
            return roots;
        }
    }
}