using UnityEngine;

namespace CodeFirst
{
  public class Loader
  {
        public static T Load<T>(string name) where T : Object
        {
            return Resources.Load<T>(name);
        }

        public static T Instantiate<T>(string name) where T : Object
        {
            return MonoBehaviour.Instantiate(Load<T>(name));
        }

        public static T Instantiate<T>(string name, Transform parent) where T : Object
        {
            return MonoBehaviour.Instantiate(Load<T>(name), parent);
        }
    }
}
