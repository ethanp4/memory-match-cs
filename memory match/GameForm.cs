namespace memory_match {
    public partial class GameForm : Form {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        const int framerate = 60;
        public static Point mPos;
        public static Point windowSize = new Point(1280, 960);
        public static bool mouseWasClicked = false;
        public static int cardPairs;

        public GameForm(int pairs) {
            InitializeComponent();
            cardPairs = pairs;
            initGame();

            Text = $"Memory matching {pairs} pairs";
            DoubleBuffered = true;
            Width = windowSize.X;
            Height = windowSize.Y;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            timer.Interval = (int)Math.Floor(1f / (float)framerate * 1000f); // frametime for 60 fps
            timer.Tick += invalidateTimer;
            timer.Start();
        }
        private void invalidateTimer(object sender, EventArgs e) {
            Invalidate(); //repaint once every 16 ms for 60 fps
        }

        protected override void OnPaint(PaintEventArgs e) {
            var g = e.Graphics; // graphics object to draw with
            Game.setRects();
            Game.checkCollision(mouseWasClicked);
            mouseWasClicked = false;
            Game.checkCards();
            Game.drawCards(g);
            Game.drawUI(g);
        }

        private void formMouseMove(object sender, MouseEventArgs e) {
            mPos = e.Location; //this needs to be set from here in order to get the local position
        }

        private void onFormClick(object sender, EventArgs e) {
            mouseWasClicked = true;
        }

        private void initGame() {
            Card.resetLists();
            for (int i = 0; i < cardPairs; i++) { // generate 10 pairs
                Card.createCardPair();
            }
            Card.shuffleList(); // call this once after initializing all the cards
            Game.score = 0;
            Game.misses = 0;
        }

        private void formKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Space && Game.score == cardPairs) {
                initGame();
            }
            if (e.KeyCode == Keys.Escape) {
                Close();
            }
        }
    }

    public static class Game {
        private static Font font = new Font("Times New Roman", 16);
        private static Dictionary<Card, Rectangle> collisionRects = new();
        public static int score = 0;
        public static int misses = 0;
        private static bool resetFlipState = false;

        public static void checkCards() {
            var cards = Card.getPlayableCards();
            Dictionary<Card, string> flippedCards = new();
            flippedCards.Clear();
            foreach (var c in cards) {
                if (c.flippedOver) flippedCards[c] = c.ToString();
            }
            if (flippedCards.Count == 2) {

                if (flippedCards.ElementAt(0).Value == flippedCards.ElementAt(1).Value) {
                    score++;
                    flippedCards.ElementAt(0).Key.collected = true;
                    flippedCards.ElementAt(1).Key.collected = true;
                }
            }
            if (flippedCards.Count == 3 || resetFlipState) {
                foreach (var c in cards) {
                    c.flippedOver = false;
                }
                resetFlipState = false;
                misses++;
            }
        }

        public static void setRects() {
            collisionRects.Clear();
            var playableCards = Card.getPlayableCards();
            var marginSize = 150;
            var cardCount = playableCards.Count;
            if (playableCards.Count > 0) {
                for (int i = 0; i < cardCount; i++) { //set rects of playable cards
                    //var cardOrigin = new Point(((i % 11) * 100) + marginSize / 4, GameForm.windowSize.Y / 2 + (i >= 11 ? 0 : 210)); //represents the top left / offset of the rect and string
                    var cardOrigin = new Point(((i % 11) * 100) + marginSize / 4, GameForm.windowSize.Y / 2 + (i >= 11 ? 210 : 0));
                    var cardRect = new Rectangle(cardOrigin.X, cardOrigin.Y, 125, 200);
                    playableCards[i].rect = cardRect;
                    collisionRects[playableCards[i]] = cardRect;
                }
            }

            var colCards = Card.getCollectedCards();
            for (int i = 0; i < colCards.Count; i++) { //set rects of played cards
                var cardOrigin = new Point((i * 100) + marginSize / 4, GameForm.windowSize.Y / 4);
                var cardRect = new Rectangle(cardOrigin.X, cardOrigin.Y, 125, 200);
                colCards[i].rect = cardRect; //no need for collision
            }
        }

        public static void checkCollision(bool mouseWasClicked) {
            var m = GameForm.mPos;
            foreach (var c in Card.getPlayableCards()) {
                c.hoveredOver = false;
            }
            for (int i = collisionRects.Count - 1; i >= 0; i--) {
                var kvp = collisionRects.ElementAt(i);
                var c = kvp.Key;
                var r = kvp.Value;
                if (m.X >= r.X && m.X <= (r.X + r.Width) &&
                    m.Y >= r.Y && m.Y <= (r.Y + r.Height)) {
                    c.hoveredOver = true;
                    if (mouseWasClicked) {
                        if (c.flippedOver) {
                            resetFlipState = true;
                        } else {
                            c.flippedOver = true;
                            c.hoveredOver = false;
                        }
                    }
                    break; //only want to set the rightmost card as highlighted
                }
            }
        }

        public static void drawUI(Graphics g) {
            var toWin = GameForm.cardPairs;
            g.DrawString($"Score: {score.ToString()}\nNeed {toWin} to win\n{misses} {(misses == 1 ? "miss" : "misses")}", font, Brushes.Black, 15, 15);
            if (score == toWin) {
                var background = new Rectangle(GameForm.windowSize.X / 2 - 220, GameForm.windowSize.Y / 2 - 120, 380, 100);
                g.FillRectangle(Brushes.BlanchedAlmond, background);
                g.DrawString("You win, press space to restart\nor escape to exit", font, Brushes.Black, GameForm.windowSize.X / 2 - 200, GameForm.windowSize.Y / 2 - 100);
            }
        }

        public static void drawCards(Graphics g) {
            var cards = Card.getAllCards();
            //for (int i = cards.Count - 1; i > 0; i--) {
            //var c = cards[i];
            foreach(var c in Card.getAllCards()) {
                var info = c.getInfo();
                var suit = info.Key;
                var value = info.Value;
                Color cardColor;

                if (suit == Card.SUIT.spades || suit == Card.SUIT.clubs || !c.flippedOver) { //set color
                    cardColor = Color.Black;
                } else {
                    cardColor = Color.Red;
                }

                var cardString = c.toStringArray();
                var cardPen = new Pen(cardColor, 3);
                var cardBrush = new SolidBrush(cardColor);
                var cardRect = c.hoveredOver ? new Rectangle(c.rect.X, c.rect.Y - 20, c.rect.Width, c.rect.Height) : c.rect;
                g.DrawRectangle(cardPen, cardRect);
                g.FillRectangle(Brushes.Beige, cardRect);
                if (c.flippedOver) {
                    g.DrawString(cardString[0], font, cardBrush, cardRect.X, cardRect.Y);
                    g.DrawString(cardString[1], font, cardBrush, cardRect.X, cardRect.Y + 18);
                }
            }

        }
    }
}
