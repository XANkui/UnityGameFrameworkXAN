using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameworkXAN
{ 
    /// <summary>
    /// 场景或者游戏作为入口管理，控制不同版本数据和流程加载
    /// </summary>
	public abstract class BaseMainVersionControllerManager : MonoBehaviour
	{
		public MainVersionModeEnum Mode;

		private static MainVersionModeEnum mSharedMode;
		private static bool mModeSetted = false;

		// Start is called before the first frame update
		void Start()
		{
            // 为了同步更新其他场景的 MainVersionModeEnum，以保持模式一致性
            if (mModeSetted == false) 
            {
                mSharedMode = Mode;
                mModeSetted = true;

                Debug.Log(GetType() + "/Start()/ The Current MainVersionModeEnum = " + Mode);
            }
            else {
                Mode = mSharedMode;
            }

            switch (mSharedMode)
            {
                case MainVersionModeEnum.Developing:
                    LaunchInDevelopingMode();
                    break;
                case MainVersionModeEnum.Test:
                    LaunchInTestMode();
                    break;
                case MainVersionModeEnum.Release:
                    LaunchInReleaseMode();
                    break;
                default:
                    Debug.Log(GetType()+ "/Start()/ No corresponding processing , MainVersionModeEnum = " + mSharedMode);
                    break;
            }
        }

        protected abstract void LaunchInDevelopingMode();
        protected abstract void LaunchInTestMode();
        protected abstract void LaunchInReleaseMode();
		
	}
}
