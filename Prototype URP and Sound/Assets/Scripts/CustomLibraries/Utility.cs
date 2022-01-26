using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utility
{
    public static class PrefabUtil
    {
        public static void SaveAsPrefab(GameObject obj, string path)
        {

            //Stores original object name
            string name = obj.name;

            //Path correction
            if (path[path.Length - 1] != '/')
            {
                path += '/';
            }

            //Generate new name
            string new_path = path + name + ".prefab";

            //Check for duplicate files
            int id = 1;
            while (System.IO.File.Exists(new_path))
            {
                new_path = path + name + "_" + id + ".prefab";
                id++;
            }

            //Save object as prefab
            PrefabUtility.SaveAsPrefabAsset(obj, new_path);
        }
    }
}
