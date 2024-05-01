using System;

namespace CodeFirst.Gameplay
{
    [Serializable]
    public class Board
    {
        public BoardMapping mapping;

        public Board(int x, int y)
        {
            mapping = new BoardMapping();
            mapping.x = x;
            mapping.y = y;
        }
    }
}