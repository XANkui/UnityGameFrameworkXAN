using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFrameworkXAN {

    /// <summary>
    /// 对象池
    /// </summary>
    public class ObjectPool<T>
    {
        // 对象池中的对象列表
        private List<ObjectPoolContainer<T>> listObjects;

        // 对象池中使用了的对象字典
        private Dictionary<T, ObjectPoolContainer<T>> usedObjectDictionary;

        // 回调事件
        private Func<T> factoryFuc;

        // 对象列表索引
        private int lastObjectIndex = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factoryFunc">工厂回调函数</param>
        /// <param name="initialSize">初始化个数</param>
        public ObjectPool(Func<T> factoryFunc, int initialSize)
        {

            this.factoryFuc = factoryFunc;

            listObjects = new List<ObjectPoolContainer<T>>();
            usedObjectDictionary = new Dictionary<T, ObjectPoolContainer<T>>();

            Warm(initialSize);
        }

        /// <summary>
        /// 孵化器，生成对象池实例
        /// </summary>
        /// <param name="capacity">实例个数</param>
        private void Warm(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                CreateContainer();
            }
        }

        /// <summary>
        /// 生成对象池实例
        /// </summary>
        /// <returns></returns>
        private ObjectPoolContainer<T> CreateContainer()
        {
            ObjectPoolContainer<T> container = new ObjectPoolContainer<T>();

            // 生成实例
            container.Item = factoryFuc.Invoke();

            // 实例添加到对象池列表中
            listObjects.Add(container);

            return container;
        }

        /// <summary>
        /// 从对象列中获取可用的对象
        /// </summary>
        /// <returns>返回可用的对象</returns>
        public T GetObjectPoolContainerItem()
        {
            ObjectPoolContainer<T> container = null;

            for (int i = 0; i < listObjects.Count; i++)
            {
                // 对象列表索引递增，并且防止越界
                lastObjectIndex++;
                lastObjectIndex %= listObjects.Count;

                // 如果列表中的对象正在使用，则进行下一循环,否则返回该对象,并退出循环
                if (listObjects[lastObjectIndex].Used)
                {
                    continue;
                }
                else
                {

                    container = listObjects[lastObjectIndex];
                    break;
                }
            }

            // 如果没有可用的对象，重新生成一个对象
            if (container == null)
            {
                container = CreateContainer();
            }

            // 标记当前对象已经被使用，并添加到使用列表中 
            container.Consume();
            usedObjectDictionary.Add(container.Item, container);
            return container.Item;
        }

        /// <summary>
        /// 释放正在使用的对象
        /// </summary>
        /// <param name="item">要释放的对象</param>
        /// <returns>true：释放成功/false: 释放失败</returns>
        public bool ReleaseItem(object item)
        {
            return ReleaseItem((T)item);
        }

        /// <summary>
        /// 从已使用字典中，释放正在使用的对象
        /// </summary>
        /// <param name="item">释放对象</param>
        /// <returns>true：释放成功/false: 释放失败</returns>
        public bool ReleaseItem(T item)
        {

            // 判断是否存在已使用对象字典中
            if (usedObjectDictionary.ContainsKey(item))
            {

                // 存在，即释放该对象，并从已使用字典中移除
                ObjectPoolContainer<T> container = usedObjectDictionary[item];
                container.Release();
                usedObjectDictionary.Remove(item);

                return true;
            }
            else
            {
                Debug.Log("This object pool does not contain the item provided: " + item);

                return false;
            }
        }

        /// <summary>
        /// 获取对象池对象列表中的个数
        /// </summary>
        public int Count
        {
            get
            {
                return listObjects.Count;
            }
        }

        /// <summary>
        /// 获取对象池已经使用的对象个数
        /// </summary>
        public int CountUsedItems
        {
            get
            {
                return usedObjectDictionary.Count;
            }
        }
    }
}
