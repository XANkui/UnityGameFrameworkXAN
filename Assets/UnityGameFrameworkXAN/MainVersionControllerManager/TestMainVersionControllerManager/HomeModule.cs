﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityGameFrameworkXAN.Test { 

	public class HomeModule : BaseMainVersionControllerManager
	{


        protected override void LaunchInDevelopingMode()
        {
            Debug.Log(GetType() + "/LaunchInDevelopingMode()/ ");
            FindButtonSetListener();
        }

        protected override void LaunchInTestMode()
        {
            Debug.Log(GetType() + "/LaunchInTestMode()/ ");
            FindButtonSetListener();
        }

        protected override void LaunchInReleaseMode()
        {
            Debug.Log(GetType() + "/LaunchInReleaseMode()/ ");
            FindButtonSetListener();
        }

        void FindButtonSetListener() {
            GameObject.Find("Canvas").GetComponentInChildren<Button>()
                .onClick.AddListener(()=> {
                    SceneManager.LoadScene("GameModuleScene");
            });
        }
    }
}
