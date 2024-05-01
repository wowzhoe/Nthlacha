using System;

namespace CodeFirst.Gameplay
{
    [Serializable]
    public class BoardMapping
    {
        public Card[] cards;
        public int x, y, index;
    }
}