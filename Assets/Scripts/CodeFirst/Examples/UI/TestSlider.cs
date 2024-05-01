using UnityEngine;
using UnityEngine.UI;
using CodeFirst;

public class TestSlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void OnSliderValueChange()
    {
        Main.Store.sliderValue.Value = slider.value;
    }
}
