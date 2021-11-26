using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameworkXAN.UI { 

	[AttributeUsage(AttributeTargets.Class)] //属性目标为 class 类
	public class UIPanelBindPrefab : Attribute
	{
		// prefab 路径
		public string Path { get; private set; }

		public UIPanelBindPrefab(string path) {
			Path = path;
		}
	}
}
