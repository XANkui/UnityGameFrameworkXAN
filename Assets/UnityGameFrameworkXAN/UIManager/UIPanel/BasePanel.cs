using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameworkXAN.UI {

    public class BasePanel : MonoBehaviour, IPanel
	{
        private UIUtil mUtil;

        protected UIUtil Util {
            get {
                if (mUtil==null)
                {
                    mUtil = gameObject.AddComponent<UIUtil>();
                    mUtil.Init();
                }

                return mUtil;
            }
        }

        public void Init()
        {
            
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
