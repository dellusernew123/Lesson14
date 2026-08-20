using UnityEngine;

namespace _Project.Scripts.Combined.CoreModules.AssetsManagement
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcePath) where T : Object
            => Resources.Load<T>(resourcePath);
    }
}