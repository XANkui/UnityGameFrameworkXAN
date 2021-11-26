using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFrameworkXAN.UI.Utils;

namespace UnityGameFrameworkXAN.UI { 

	public class UIManger : MonoBehaviour
	{
		private Stack<string> mUIStack = new Stack<string>();
		private Dictionary<string, IPanel> mPanelDict = new Dictionary<string, IPanel>();

		private Canvas mCanvas;
		public Canvas Canvas {
			get {
                if (mCanvas==null)
                {
					mCanvas = UnityEngine.Object.FindObjectOfType<Canvas>();

                }

				if (mCanvas == null)
				{
					Debug.LogError(GetType()+ "/Canvas()/ There is no canvas in the scene");

				}

				return mCanvas;
			}
		}

		public IPanel Show(string path) {
            if (mUIStack.Count > 0)
            {
				string name = mUIStack.Peek();
				mPanelDict[name].Hide(); // 隐藏之前的
            }
			// 显示新的
			IPanel panel = InitPanel(path);
			panel.Show();

			mUIStack.Push(path);
			mPanelDict[path] = panel;

			return panel;
		}

		public void Back() {
            if (mUIStack.Count<=1)
            {
				return;
            }

			// 隐藏当前
			string name = mUIStack.Pop();
			mPanelDict[name].Hide();

			// 显示之前的
			name = mUIStack.Peek();
			mPanelDict[name].Show();


		}

		private IPanel InitPanel(string path) {
			if (mPanelDict.ContainsKey(path))
			{
				return mPanelDict[path];
			}
			else {
				// TODO loadMGr
				GameObject panelGo = new GameObject();
				Type type = BindAttributeUtil.GetType(path);
				Component component = panelGo.AddComponent(type);
				IPanel panel = null;
				if (component is IPanel)
                {
					panel = component as IPanel;
					panel.Init();
					mPanelDict.Add(path, panel);
				}
				return panel;
			}
		}
	}
}
