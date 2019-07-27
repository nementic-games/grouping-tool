/*
// Copyright (c) 2019 Nementic Games GmbH.
// This file is subject to the MIT License. 
// See the LICENSE file in the package root folder for more information.
// Author: Anja Haumann
* {^._.^}/
*/

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Nementic.GroupingTool
{
    /// <summary>
    /// Lables a gameObject with the build in editor-gameObject-icons.
    /// </summary>
    public static class GameObjectLabler
    {
        /// <summary>
        /// Color of the lable.
        /// </summary>
        public enum Color
        {
            Gray = 0,
            Blue,
            Cyan,
            Green,
            Yellow,
            Orange,
            Red,
            Magenta
        }

        /// <summary>
        /// Type of the lable.
        /// </summary>
        public enum Type
        {
            Label = 0,
            Dot = 1,
            Diamond = 2
        }

        /// <summary>
        /// Texture names of the build in editor-gameObject-icons. Index with the Type-enum.
        /// </summary>
        private static readonly string[] textureNames = {
        "sv_label_{0}", // Lable
        "sv_icon_dot{0}_pix16_gizmo", // Dot
        "sv_icon_dot{0}_pix16_gizmo" // Diamond
        };

        /// <summary>
        /// The diamond textures got the same file name as the dot textures.
        /// ->  there indices start after the dot textures.
        /// So we have to offset there color indices by the count of available dot textures.
        /// </summary>
        private static readonly int diamondIndexOffset = 8;


        /// <summary>
        /// Adds a lable of the given type and color to the gameObject. (Reflection: might break with a Unity upgrade)
        /// </summary>
        /// <param name="go"> GameObject to lable.</param>
        /// <param name="lableType"> Type of the lable.</param>
        /// <param name="lableColor">Color of the lable.</param>
        public static void AddLable(GameObject go, Type lableType, Color lableColor)
        {
            // Choose the correct texture.
            string textureName = textureNames[(int)lableType];
            int textureIndex = (int)lableColor;
            if (lableType == Type.Diamond) textureIndex += diamondIndexOffset;
            textureName = textureName.Replace("{0}", textureIndex.ToString());
            GUIContent iconTexture = EditorGUIUtility.IconContent(textureName);

            // Get 'SetIconMethod' via reflection.
            System.Type editorGuiUtilityType = typeof(EditorGUIUtility);
            BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
            object[] args = new object[] { go, iconTexture.image };
            System.Type[] argTypes = { typeof(UnityEngine.Object), typeof(Texture2D) };

            // Add lable.
            var setIconMethod = editorGuiUtilityType.GetMethod("SetIconForObject", bindingFlags, null, argTypes, null);
            setIconMethod.Invoke(null, args);
        }
    }
}
#endif
