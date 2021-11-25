using UnityEngine;

namespace UnityGameFrameworkXAN {

   
    /// <summary>
    /// Mono 单例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        private static readonly object locker = new object();

        private static bool mIsAppQuitting;

        public static T Instance
        {
            get
            {
                // 脚本销毁则单例置空
                if (mIsAppQuitting)
                {
                    instance = null;
                    return instance;
                }

                // 线程安全处理
                lock (locker)
                {
                    if (instance == null)
                    {
                        // 保证场景中只有一个 单例
                        T[] managers = Object.FindObjectsOfType(typeof(T)) as T[];
                        if (managers.Length != 0)
                        {
                            if (managers.Length == 1)
                            {
                                instance = managers[0];
                                instance.gameObject.name = typeof(T).Name;
                                return instance;
                            }
                            else
                            {
                                Debug.LogError("Class " + typeof(T).Name + " exists multiple times in violation of singleton pattern. Destroying all copies");
                                foreach (T manager in managers)
                                {
                                    Destroy(manager.gameObject);
                                }
                            }
                        }


                        var singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T);
                        singleton.hideFlags = HideFlags.None;
                        DontDestroyOnLoad(singleton);

                    }
                    instance.hideFlags = HideFlags.None;
                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            mIsAppQuitting = false;
        }

        protected virtual void OnDestroy()
        {
            mIsAppQuitting = true;
        }
    }
}
