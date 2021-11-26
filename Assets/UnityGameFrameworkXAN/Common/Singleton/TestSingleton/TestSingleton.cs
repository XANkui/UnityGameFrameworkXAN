using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFrameworkXAN.Common;

namespace UnityGameFrameworkXAN.Test
{ 

	public class TestSingleton : MonoSingleton<TestSingleton>
	{
		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			
		}
	}

	public class TestClass : Singleton<TestClass> {
		// 注意：添加非公有无参构造函数，不然会报错
		protected TestClass() { }
	}
}
