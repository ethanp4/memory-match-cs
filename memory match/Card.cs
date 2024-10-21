using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace memory_match {
    //run logic then re-render frame every 16 ms
    //on form click will compare mouse position against all 
    //card rects for collision
    //it will set that cards state to flipped
    //if two cards states are flipped, the game loop will compare these cards
    //if theyre the same, remove them and add score, otherwise flip them back over
    public class Card {
        private static List<Card> cardsList = new List<Card>(); //this will contain all cards and will be used by game logic and renderer
        private static Dictionary<string, bool> cardsAvailability = new(); //this will contain each card combo (52) followed by if theyre used or not (default false)
        //this is used to avoid calling random repeatedly until the combination is unused

        public enum SUIT { clubs, spades, hearts, diamonds }
        const int suitCount = 3;
        public enum VALUE { ace, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king }
        const int valueCount = 12;

        private SUIT suit;
        private VALUE value;
        public bool flippedOver = false; //true when clicked to see its type
        public bool hoveredOver = false;
        public bool collected = false; //when matched
        public Rectangle rect;

        public KeyValuePair<SUIT, VALUE> getInfo() {
            return new KeyValuePair<SUIT, VALUE>(suit, value);
        }

        public static void resetLists() {
            cardsList.Clear();
            cardsAvailability.Clear();
            for (int i = 0; i <= suitCount; i++) {
                for (int j = 0; j <= valueCount; j++) {
                    cardsAvailability.Add($"{i}{j}", true);
                }
            }
        }

        public static List<Card> getAllCards() {
            return cardsList;
        }

        public static List<Card> getPlayableCards() {
            var ret = new List<Card>();
            foreach(var c in cardsList) {
                if (!c.collected) {
                    ret.Add(c);
                }
            }
            return ret;
        }

        public static List<Card> getCollectedCards() {
            var ret = new List<Card>();
            foreach(var c in cardsList) {
                if (c.collected) {
                    ret.Add(c);
                }
            }
            return ret;
        }

        public static void shuffleList() {
            var arr = cardsList.ToArray();
            new Random().Shuffle(arr); // this operation is done in place
            cardsList = new List<Card>(arr);
        }

        public static void createCardPair() {
            KeyValuePair<string, bool> sel;
            do {
                sel = cardsAvailability.ElementAt(new Random().Next(0, 51));
            } while (sel.Value == false); //if the card has been used, then go again
            var suit = int.Parse(sel.Key.Substring(0, 1));
            var val = int.Parse(sel.Key.Substring(1));
            //cardsList.Add(this);
            new Card((SUIT)suit, (VALUE)val);
            new Card((SUIT)suit, (VALUE)val);
            cardsAvailability[$"{suit}{val}"] = false; //make it so this card cant be reused
        }

        private Card(SUIT s, VALUE v) {
            this.suit = s;
            this.value = v;
            cardsList.Add(this);
        }
        public string[] toStringArray() {
            string[] cardString = { "", "" };
            switch (value) {
                case Card.VALUE.ace:
                    cardString[0] = "A";
                    break;
                case Card.VALUE.two:
                    cardString[0] += 2;
                    break;
                case Card.VALUE.three:
                    cardString[0] += 3;
                    break;
                case Card.VALUE.four:
                    cardString[0] += 4;
                    break;
                case Card.VALUE.five:
                    cardString[0] += 5;
                    break;
                case Card.VALUE.six:
                    cardString[0] += 6;
                    break;
                case Card.VALUE.seven:
                    cardString[0] += 7;
                    break;
                case Card.VALUE.eight:
                    cardString[0] += 8;
                    break;
                case Card.VALUE.nine:
                    cardString[0] += 9;
                    break;
                case Card.VALUE.ten:
                    cardString[0] += 10;
                    break;
                case Card.VALUE.jack:
                    cardString[0] += "J";
                    break;
                case Card.VALUE.king:
                    cardString[0] += "K";
                    break;
                case Card.VALUE.queen:
                    cardString[0] += "Q";
                    break;
            }
            switch (suit) {
                case Card.SUIT.spades:
                    cardString[1] += "♠";
                    break;
                case Card.SUIT.clubs:
                    cardString[1] += "♣";
                    break;
                case Card.SUIT.diamonds:
                    cardString[1] += "♦";
                    break;
                case Card.SUIT.hearts:
                    cardString[1] += "♥";
                    break;
            }
            return cardString;
        }

        public override string ToString() {
            var arr = toStringArray();
            return $"{arr[0]}{arr[1]}";
        }
    }

}
