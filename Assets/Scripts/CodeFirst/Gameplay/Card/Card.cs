using System;

namespace CodeFirst.Gameplay
{
    [Serializable]
    public class Card
    {
        public CardMapping mapping;
        public CardView view;

        public Card(CardView _view)
        {
            view = _view;
            mapping = new CardMapping();
        }
    }
}