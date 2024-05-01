using UnityEngine;

namespace CodeFirst.Services
{
    public class TestService
    {
        public void Start()
        {
            var gradient = MakeGradient();
            Main.Store.sliderValue.Bind(s => Main.Store.color.Value = gradient.Evaluate(s));
        }


        private Gradient MakeGradient()
        {
            var gradient = new Gradient();

            var colorKey = new GradientColorKey[2];
            colorKey[0].color = Constants.ColorStart;
            colorKey[0].time = 0.0f;
            colorKey[1].color = Constants.ColorEnd;
            colorKey[1].time = 1.0f;

            var alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.0f;
            alphaKey[1].time = 1.0f;

            gradient.SetKeys(colorKey, alphaKey);

            return gradient;
        }
    }
}
