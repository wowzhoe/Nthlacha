using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeFirst.Gameplay
{
    public static class AnimationExtention
    {
        public static T Runner<T>(Func<T> funcToRun)
        {
            return funcToRun();
        }

        public static async Task Select(Card card)
        {
            await Task.Delay(500);
            BoardLoader.Instance.OnClick(card.mapping.spriteID, card.mapping.id);
        }

        public static Task Fade(Image img)
        {
            float rate = 1.0f / 2.5f;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * rate;
                img.color = Color.Lerp(img.color, Color.clear, t);
            }

            return null;
        }

        public static IEnumerator FadeTo(Image img)
        {
            float rate = 1.0f / 2.5f;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * rate;
                img.color = Color.Lerp(img.color, Color.clear, t);

                yield return null;
            }
        }

        public static IEnumerator FlipTo(CardView view, Card card, Transform thisTransform, float time, bool changeSprite)
        {
            Quaternion startRotation = thisTransform.rotation;
            Quaternion endRotation = thisTransform.rotation.eulerAngles.y >= 90 ? 
                thisTransform.rotation * Quaternion.Euler(new Vector3(0, -90, 0)) : 
                thisTransform.rotation*Quaternion.Euler(new Vector3(0, 90, 0));

            float rate = 1.0f / time;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * rate;
                thisTransform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

                yield return null;
            }

            if (changeSprite)
            {
                card.mapping.flipped = !card.mapping.flipped;
                view.Change();
                Coroutiner.instance.StartCoroutine(FlipTo(view, card, thisTransform, time, false));
            }
            else
                card.mapping.turning = false;
        }

        public static IEnumerator HideBoard(Card[] cards)
        {
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < cards.Length; i++)
                cards[i].view.Flip();
            yield return new WaitForSeconds(0.5f);
        }
    }
}