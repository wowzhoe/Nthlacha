using UnityEngine;

namespace CodeFirst
{
    public class Coroutiner : MonoBehaviour
    {
        public static Coroutiner instance;

        void Awake()
        {
            instance = this;
        }
    }
}