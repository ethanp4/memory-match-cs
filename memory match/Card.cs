using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        enum SUIT { clubs, spades, hearts, diamonds }
        const int suitCount = 3;
        enum VALUE { ace, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king }
        const int valueCount = 12;

        private SUIT suit;
        private VALUE value;

        static Card() { //populate cardsAvailability
            for (int i = 0; i <= suitCount; i++) {
                for (int j = 0; j <= valueCount; j++) {
                    cardsAvailability.Add($"{i}{j}", true);
                }
            }
        }

        public static List<Card> getCardsList() {
            return cardsList;
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
        public override string ToString() {
            return $"This card is a {value} of {suit}\n";
        }
    }

}
