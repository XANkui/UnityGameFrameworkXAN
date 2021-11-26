using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityGameFrameworkXAN.UI { 

	public class UIUtil : MonoBehaviour
	{
		private Dictionary<string, UIUtilData> mDataDict;

        public void Init()
        {
			mDataDict = new Dictionary<string, UIUtilData>();
			RectTransform rect = transform.GetComponent<RectTransform>();
            foreach (RectTransform rectTrans in rect)
            {
				mDataDict.Add(rectTrans.name,new UIUtilData(rectTrans));
            }
		}

		public UIUtilData Get(string name) {
			if (mDataDict.ContainsKey(name))
			{
				return mDataDict[name];
			}
			else {
				Transform temp = transform.Find(name);
				if (temp == null)
				{
					Debug.LogError("/Get()/ The component does not exist, name = " + name);
					return null;
				}
				else {
					mDataDict.Add(name, new UIUtilData(temp.GetComponent<RectTransform>()));
					return mDataDict[name];
				}
			}
		}
    }

	public class UIUtilData { 
		public GameObject Go { get; private set; }
		public RectTransform RectTrans { get; private set; }
		public Button Btn { get; private set; }
		public Image Img { get; private set; }
		public Text Text { get; private set; }

		public UIUtilData(RectTransform rectTrans) {
			RectTrans = rectTrans;
			Go = rectTrans.gameObject;
			Btn = rectTrans.GetComponent<Button>();
			Img = rectTrans.GetComponent<Image>();
			Text = rectTrans.GetComponent<Text>();
			
		}

		public void AddListener(Action action) {
			if (Btn != null)
			{
				Btn.onClick.AddListener(() => action());
			}
			else {
				Debug.LogError("/AddListener()/ Button does not exist, GO.name = " +Go.name);
			}
		}

		public void SetSPrite(Sprite sprite)
		{
			if (Img != null)
			{
				Img.sprite = sprite;
			}
			else
			{
				Debug.LogError("/SetSPrite()/ Image does not exist, GO.name = " + Go.name);
			}
		}

		public void SetTextContent(string content)
		{
			if (Text != null)
			{
				Text.text = content;
			}
			else
			{
				Debug.LogError("/SetTextContent()/ Text does not exist, GO.name = " + Go.name);
			}
		}
	}
}
