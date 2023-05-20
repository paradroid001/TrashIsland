using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{
    public class MaterialSwapper : MonoBehaviour
    {
        [System.Serializable]
        public class MaterialInfo : NamedData<Material>
        {
        }
        public Renderer[] renderers;
        public MaterialInfo[] materials;

        [YarnCommand("material")]
        public void SwapMaterial(string materialname)
        {
            Material m;
            if (MaterialInfo.FindByName(materials, materialname, out m) )
            {
                foreach(Renderer renderer in renderers)
                {
                    renderer.material = m;
                }
            }
        }
    }
}
