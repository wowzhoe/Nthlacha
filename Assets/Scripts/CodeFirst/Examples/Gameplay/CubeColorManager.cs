using UnityEngine;
using CodeFirst;

public class CubeColorManager : MonoBehaviour
{
    [SerializeField] Renderer renderer;

    void Start()
    {
        Main.Store.color.Bind(color => renderer.material.color = color);
    }
}
