
using System.Collections.Generic;
using UnityEngine;
using UnityGameFrameworkXAN.Common;

namespace UnityGameFrameworkXAN {

    public class SimpleObjectPoolManager : MonoSingleton<SimpleObjectPoolManager>
    {
        // 是否打印对象池对象使用情况（默认不打印）
        private bool isLogUsedPoolObject = false;

        // 预制体（对象池对象）生成对象池的字典
        private Dictionary<GameObject, ObjectPool<GameObject>> prefabPoolDictinary;

        // 正在使用的对象的字典
        private Dictionary<GameObject, ObjectPool<GameObject>> usedPoolObjectbDictinary;

        // 对象池是否更新使用的标志
        private bool dirty = false;

        protected override void Awake()
        {
            base.Awake();

            // 初始化字典
            prefabPoolDictinary = new Dictionary<GameObject, ObjectPool<GameObject>>();
            usedPoolObjectbDictinary = new Dictionary<GameObject, ObjectPool<GameObject>>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isLogUsedPoolObject == true && dirty == true)
            {
                PrintUsedPoolObjectStatue();
                dirty = false;
            }
        }

        /// <summary>
        /// 是否打印对象池对象使用情况（默认不打印）
        /// </summary>
        /// <param name="isLogUsedPoolObject"></param>
        public void SetIsLogUsedPoolObject(bool isLogUsedPoolObject)
        {
            this.isLogUsedPoolObject = isLogUsedPoolObject;
        }

        /// <summary>
        /// 孵化器孵化指定个数对象池对象
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <param name="count">要预生成对象池对象</param>
        public void WarmPool(GameObject prefab, int count)
        {
            if (prefabPoolDictinary.ContainsKey(prefab))
            {
                Debug.Log("Pool for prefab " + prefab.name + " has already been created");
            }

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(() => {
                return InstantiatePrefab(prefab);

            }, count);

            // 添加到字典中
            prefabPoolDictinary[prefab] = pool;

            // 更新使用数据标志
            dirty = true;

        }

        /// <summary>
        /// 从对象池拿出指定对象使用
        /// </summary>
        /// <param name="prefab">要使用的对象</param>
        /// <returns>对象池返回的可用对象</returns>
        public GameObject SpawnObject(GameObject prefab)
        {
            return SpawnObject(prefab, Vector3.zero, Quaternion.identity);
        }


        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            // 如果该预制体没有孵化，则先进行孵化 1 个
            if (prefabPoolDictinary.ContainsKey(prefab) == false)
            {
                WarmPool(prefab, 1);
            }

            // 从对象池中获取对象
            ObjectPool<GameObject> pool = prefabPoolDictinary[prefab];
            GameObject clone = pool.GetObjectPoolContainerItem();

            // 设置对象的位置旋转，显示物体
            clone.transform.position = position;
            clone.transform.rotation = rotation;
            clone.SetActive(true);

            // 把拿出来的对象添加到已使用的字典中
            usedPoolObjectbDictinary.Add(clone, pool);

            // 更新使用数据标志
            dirty = true;

            return clone;
        }

        /// <summary>
        /// 释放使用的对象池对象
        /// </summary>
        /// <param name="clone">对象</param>
        public void ReleaseObject(GameObject clone)
        {
            clone.SetActive(false);

            // 已使用的字典中
            if (usedPoolObjectbDictinary.ContainsKey(clone))
            {
                usedPoolObjectbDictinary[clone].ReleaseItem(clone);
                usedPoolObjectbDictinary.Remove(clone);

                // 更新使用数据标志
                dirty = true;
            }
            else
            {

                Debug.Log("No pool contains the object: " + clone.name);

            }
        }

        /// <summary>
        /// 打印吃对象使用情况
        /// </summary>
        private void PrintUsedPoolObjectStatue()
        {

            foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in prefabPoolDictinary)
            {
                Debug.Log(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
            }
        }

        /// <summary>
        /// 生成函数,父物体为被物体
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <returns></returns>
        private GameObject InstantiatePrefab(GameObject prefab)
        {
            var go = Instantiate(prefab) as GameObject;
            go.transform.parent = this.transform;
            go.SetActive(false);
            return go;
        }
    }
}
