using UnityEngine;

namespace CodeFirst
{
    public class EntryPoint : MonoBehaviour
    {
        void Awake()
        {
            Main.Store = new Store();
            Main.Services = new Servicer();
            new Initializator().InitScene();
            Main.Services.Start();
        }
    }
}
