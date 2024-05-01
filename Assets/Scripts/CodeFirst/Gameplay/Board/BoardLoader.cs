using CodeFirst;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace CodeFirst.Gameplay
{
    public class BoardLoader : MonoBehaviour, IBoardable
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject cardList;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Sprite spriteBack;
        [SerializeField] private Text scoreField;

        private Board board;

        public static BoardLoader Instance;
        public static int gameSize = 2;

        private int spriteSelected;
        private int cardSelected;
        private int cardLeft;
        private bool gameStart;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Load(5, 5);
            Initialize();
        }

        private void Update()
        {
            scoreField.text = "Score: " + (board.mapping.cards.Length - cardLeft) / 2;
        }

        private void Initialize()
        {
            cardSelected = spriteSelected = -1;
            cardLeft = board.mapping.cards.Length;

            BoardAllocation();
            StartCoroutine(AnimationExtention.HideBoard(board.mapping.cards));
        }

        public void Load(int? x, int? y)
        {
            if (x.HasValue && y.HasValue) board = new Board(x.Value, y.Value);
            else board = new Board(2, 2);

            int isOdd = board.mapping.x % 2;
            board.mapping.cards = new Card[board.mapping.x * board.mapping.y - isOdd];

            Clear();

            RectTransform panelsize = panel.transform.GetComponent(typeof(RectTransform)) as RectTransform;
            float row_size = panelsize.sizeDelta.x;
            float col_size = panelsize.sizeDelta.y;
            float scale = 1.0f / board.mapping.x;
            float xInc = row_size / board.mapping.y;
            float yInc = col_size / board.mapping.x;
            float curX = -xInc * (float)(board.mapping.x / 2);
            float curY = -yInc * (float)(board.mapping.y / 2);

            if (isOdd == 0)
            {
                curX += xInc / 2;
                curY += yInc / 2;
            }

            float initialX = curX;

            for (int i = 0; i < board.mapping.y; i++)
            {
                curX = initialX;

                for (int j = 0; j < board.mapping.x; j++)
                {
                    GameObject c;

                    if (isOdd == 1 && i == (board.mapping.y - 1) && j == (board.mapping.x - 1))
                    {
                        int index = board.mapping.x / 2 * board.mapping.x + board.mapping.x / 2;
                        c = board.mapping.cards[index].mapping.go;
                        board.mapping.cards[index].mapping.x = curX;
                        board.mapping.cards[index].mapping.y = curY;

                        c.name = "ID : " + board.mapping.cards[index].mapping.id + " | x : " + curX + " | y: " + curY;
                    }
                    else
                    {
                        var g = Loader.Instantiate<CardView>(AddressableNames.Card);
                        c = g.gameObject;
                        c.transform.SetParent(cardList.transform);

                        int index = i * board.mapping.x + j;
                        var view = c.GetComponent<CardView>();
                        view.Initialize();
                        view.GetComponent<Button>()?.onClick.AddListener(delegate
                        {
                            view.OnClick();
                        });
                        view.ID = index;

                        board.mapping.cards[index] = view.card;
                        board.mapping.cards[index].mapping.id = index;
                        if (!string.IsNullOrEmpty(c.name))
                        {
                            board.mapping.cards[index].mapping.x = curX;
                            board.mapping.cards[index].mapping.y = curY;
                        }
                        board.mapping.cards[index].mapping.go = c;

                        c.transform.localScale = new Vector3(scale, scale);
                        c.name = "ID : " + board.mapping.cards[index].mapping.id + " | x : " + curX + " | y: " + curY;
                    }

                    c.transform.localPosition = new Vector3(curX, curY, 0);
                    curX += xInc;
                }

                curY += yInc;
            }
        }

        public void Clear()
        {
            foreach (Transform child in cardList.transform)
            {
                child.GetComponent<CardView>().
                    GetComponent<Button>()?.
                    onClick.RemoveAllListeners();

                GameObject.Destroy(child.gameObject);
            }

            Resources.UnloadUnusedAssets();
        }

        public Sprite GetSprite(int spriteId)
        {
            return sprites[spriteId];
        }

        public Sprite CardBack()
        {
            return spriteBack;
        }

        private void BoardAllocation()
        {
            int i, j;
            int[] selectedID = new int[board.mapping.cards.Length / 2];

            for (i = 0; i < board.mapping.cards.Length / 2; i++)
            {
                int value = Random.Range(0, sprites.Length - 1);
                for (j = i; j > 0; j--)
                {
                    if (selectedID[j - 1] == value)
                        value = (value + 1) % sprites.Length;
                }
                selectedID[i] = value;
            }

            for (i = 0; i < board.mapping.cards.Length; i++)
            {
                board.mapping.cards[i].view.Active();
                board.mapping.cards[i].view.SpriteID = -1;
                board.mapping.cards[i].view.ResetRotation();
            }

            for (i = 0; i < board.mapping.cards.Length / 2; i++)
                for (j = 0; j < 2; j++)
                {
                    int value = Random.Range(0, board.mapping.cards.Length - 1);
                    while (board.mapping.cards[value].view.SpriteID != -1) //.view.SpriteID
                        value = (value + 1) % board.mapping.cards.Length;

                    board.mapping.cards[value].view.SpriteID = selectedID[i];
                    //board.mapping.cards[value].mapping.spriteID = selectedID[i];
                }
        }

        public void OnClick(int spriteId, int cardId)
        {
            if (spriteSelected == -1)
            {
                spriteSelected = spriteId;
                cardSelected = cardId;
            }
            else
            {
                if (spriteSelected == spriteId)
                {
                    board.mapping.cards[cardSelected].view.Inactive();
                    board.mapping.cards[cardId].view.Inactive();
                    cardLeft -= 2;

                    AudioPlayer.Instance.PlayAudio(cardLeft == 0 ? 1 : 2);
                }
                else
                {
                    board.mapping.cards[cardSelected].view.Flip();
                    board.mapping.cards[cardId].view.Flip();

                    AudioPlayer.Instance.PlayAudio(3);
                }
                cardSelected = spriteSelected = -1;
            }
        }

        public void OnClickStart()
        {

        }

        public void OnClickSave()
        {
            string potion = JsonUtility.ToJson(board);
            System.IO.File.WriteAllText(Application.streamingAssetsPath + "/LastBoardData.json", potion);
        }

        public void OnClickLoad()
        {
            cardSelected = spriteSelected = -1;
            cardLeft = board.mapping.cards.Length;
            
            var result = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/LastBoardData.json");
            board = null;
            board = JsonUtility.FromJson<Board>(result);

            int isOdd = board.mapping.x % 2;
            float scale = 1.0f / board.mapping.x;

            Clear();

            for (int i = 0; i < board.mapping.y; i++)
            {
                for (int j = 0; j < board.mapping.x; j++)
                {
                    GameObject c;

                    if (isOdd == 1 && i == (board.mapping.y - 1) && j == (board.mapping.x - 1))
                    {
                        int index = board.mapping.x / 2 * board.mapping.x + board.mapping.x / 2;
                        c = board.mapping.cards[index].mapping.go;
                    }
                    else
                    {
                        var g = Loader.Instantiate<CardView>(AddressableNames.Card);
                        c = g.gameObject;
                        c.transform.SetParent(cardList.transform);

                        int index = i * board.mapping.x + j;
                        var view = c.GetComponent<CardView>();
                        view.Initialize();
                        view.GetComponent<Button>()?.onClick.AddListener(delegate
                        {
                            view.OnClick();
                        });

                        board.mapping.cards[index].view = view;

                        c.name = "ID : " + board.mapping.cards[index].mapping.id + " | x : " + board.mapping.cards[index].mapping.x + " | y: " + board.mapping.cards[index].mapping.y;
                        if (board.mapping.cards[index].mapping.flipped)
                        {
                            board.mapping.cards[index].view.Inactive();
                            cardLeft--;
                        }

                        c.transform.localScale = new Vector3(scale, scale);
                        c.transform.localPosition = new Vector3(board.mapping.cards[index].mapping.x, board.mapping.cards[index].mapping.y, 0);

                        board.mapping.cards[index].mapping.go = c;
                    }
                }
            }
            
            StartCoroutine(AnimationExtention.HideBoard(board.mapping.cards));

            for (int i = 0; i < board.mapping.cards.Length; i++)
            {
                board.mapping.cards[i].view.SpriteID = board.mapping.cards[i].mapping.spriteID;
                board.mapping.cards[i].view.ID = board.mapping.cards[i].mapping.id;
            }
        }
    }
}