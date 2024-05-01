using UnityEngine;
using UnityEngine.UI;

namespace CodeFirst.Gameplay
{
    public class CardAction : MonoBehaviour
    {
        public void Flip(CardView view, Card card)
        {
            card.mapping.turning = true;
            AudioPlayer.Instance.PlayAudio(0);
            StartCoroutine(AnimationExtention.FlipTo(view, card, transform, 0.25f, true));
        }

        public void Change(Card card, Image img)
        {
            if (card.mapping.spriteID == -1 || img == null) return;
            if (card.mapping.flipped)
                img.sprite = BoardLoader.Instance.GetSprite(card.mapping.spriteID);
            else
                img.sprite = BoardLoader.Instance.CardBack();
        }

        public void Deactivate(Image img)
        {
            StartCoroutine(AnimationExtention.FadeTo(img));
        }

        public void Activate(Image img)
        {
            if (img)
                img.color = Color.white;
        }

        public void Reset(Card card)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            card.mapping.flipped = true;
        }
    }
}