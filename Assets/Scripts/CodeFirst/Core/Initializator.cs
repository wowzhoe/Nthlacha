using UnityEngine;

namespace CodeFirst
{
    public class Initializator
    {
        public void InitScene()
        {
            InitCameraRig();
            InitEnvironment();
            InitUI();
        }

        private void InitCameraRig()
        {
            Loader.Instantiate<GameObject>(AddressableNames.CameraRig);
        }

        private void InitEnvironment()
        {
            Loader.Instantiate<GameObject>(AddressableNames.Environment);
        }

        private void InitUI()
        {
            Loader.Instantiate<GameObject>(AddressableNames.UI);
        }
    }
}
