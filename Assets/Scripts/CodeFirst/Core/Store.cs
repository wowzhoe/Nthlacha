namespace CodeFirst
{
    public class Store
    {
        public Box<float> sliderValue = new Box<float>(0);
        public Box<UnityEngine.Color> color = new Box<UnityEngine.Color>(Constants.ColorStart);
        public Box<UIState> uiState = new Box<UIState>(UIState.Main);
    }
}
