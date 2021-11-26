using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityGameFrameworkXAN.UI.Utils;

namespace UnityGameFrameworkXAN.UI { 

	public class InitBindAttribute 
	{
		public void Init() {
			Assembly assembly = Assembly.GetAssembly(typeof(UIPanelBindPrefab));
			Type[] types = assembly.GetExportedTypes();

            foreach (Type type in types)
            {
                foreach (Attribute attribute in Attribute.GetCustomAttributes(type,true))
                {
                    if (attribute is UIPanelBindPrefab)
                    {
                        UIPanelBindPrefab data = attribute as UIPanelBindPrefab;
                        BindAttributeUtil.Bind(data.Path,type);
                    }
                }
            }
		}
	}
}
