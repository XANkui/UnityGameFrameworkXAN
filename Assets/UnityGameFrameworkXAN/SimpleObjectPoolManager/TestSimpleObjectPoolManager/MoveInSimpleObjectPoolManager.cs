
using UnityEngine;

namespace UnityGameFrameworkXAN.Test { 

	public class MoveInSimpleObjectPoolManager : MonoBehaviour
	{
        // Start is called before the first frame update
        void OnEnable()
        {
            Invoke("Release", 3);
        }

        // Update is called once per frame
        void Update()
        {

            this.transform.Translate(transform.right * Time.deltaTime * 10);
        }
        void Release()
        {
            SimpleObjectPoolManager.Instance.ReleaseObject(this.gameObject);
        }

	}
}
