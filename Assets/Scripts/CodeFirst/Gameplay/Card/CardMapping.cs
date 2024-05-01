using System;
using UnityEngine;

namespace CodeFirst.Gameplay
{
    [Serializable]
    public class CardMapping
    {
        public int spriteID;
        public int id;
        public float x, y;
        public bool flipped;
        public bool turning;
        public bool played;
        public GameObject go;
    }
}