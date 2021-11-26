using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameworkXAN.UI.Utils { 

	public class BindAttributeUtil 
	{

		private static Dictionary<string, Type> mPrefabAndScriptDict = new Dictionary<string, Type>();

		public static void Bind(string path, Type type) {
			if (mPrefabAndScriptDict.ContainsKey(path) == false)
			{
				mPrefabAndScriptDict.Add(path, type);
			}
			else {
				Debug.LogError("/Bind()/ Path already exists, Path = "+path);
			}
		}

		public static Type GetType(string path) {
			if (mPrefabAndScriptDict.ContainsKey(path))
			{
				return mPrefabAndScriptDict[path];
			}
			else {
				Debug.LogError("/GetType()/ The path Type is not included, Path = " + path);
				return null;
			}
		}
		
	}
}
