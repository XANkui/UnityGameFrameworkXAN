using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameworkXAN.Test { 

	public class TestSimpleObjectPoolManager : MonoBehaviour
	{
        private GameObject cube;
        private GameObject sphere;

        // Start is called before the first frame update
        void Start()
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<MoveInSimpleObjectPoolManager>();
            cube.SetActive(false);
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.AddComponent<MoveInSimpleObjectPoolManager>();
            sphere.SetActive(false);
            SimpleObjectPoolManager.Instance.WarmPool(cube, 5);
            SimpleObjectPoolManager.Instance.WarmPool(sphere, 5);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SimpleObjectPoolManager.Instance.SpawnObject(cube);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SimpleObjectPoolManager.Instance.SpawnObject(sphere);
            }
        }


	}
}
