using UnityEngine;
using UnityEngine.UI;

namespace CodeFirst.Gameplay
{
    public class CardView : CardAction
    {
        public Card card;
        [SerializeField] private Image img;

        public int SpriteID
        {
            set
            {
                card.mapping.spriteID = value;
                card.mapping.flipped = true;
                Change();
            }
            get { return card.mapping.spriteID; }
        }

        public int ID
        {
            set { card.mapping.id = value; }
            get { return card.mapping.id; }
        }

        public void Initialize() => card = new Card(this);

        public void Change() => base.Change(card, img);

        public void Inactive() => base.Deactivate(card, img);

        public void Active() => base.Activate(img);

        public void ResetRotation() => base.Reset(card);

        public void Flip() => base.Flip(this, card);

        public async void OnClick()
        {
            if (card.mapping.flipped || card.mapping.turning) return;
            Flip(this, card);
            await AnimationExtention.Select(card);
        }
    }
}