using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_match {
    public partial class GameForm : Form {
        public GameForm() {
            InitializeComponent();
            for (int i = 0; i < 30; i++) {
                
            } //need to generate a pair of cards that are both the same, use a static method of card to do this
            Card.createCardPair();
            Card.createCardPair();
            //var cards = Card.getCardsList();
            //foreach (var c in cards) {
            //    richTextBox1.Text += c.ToString();
            //}
        }
    }
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
        enum VALUE { ace, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king}
        const int valueCount = 12;

        private SUIT suit;
        private VALUE value;

        static Card() { //populate cardsAvailability
            for (int i = 0; i <= suitCount; i++) {
                for (int j = 0; j <= valueCount; j++) {
                    cardsAvailability.Add($"{i}{j}", false);
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
            } while (sel.Value == true); //if the card has been used, then go again
            var suit = int.Parse(sel.Key.Substring(0, 1));
            var val = int.Parse(sel.Key.Substring(1));
            //cardsList.Add(this);
            new Card((SUIT)suit, (VALUE)val);
            new Card((SUIT)suit, (VALUE)val);

            Console.WriteLine("asdf");
        }

        private Card(SUIT s, VALUE v) {
            this.suit = s;
            this.value = v;
            cardsList.Add(this);
        }

        public static void generateCardPair() {

        }
        public override string ToString() {
            return $"This card is a {value} of {suit}\n";
        }

        private static bool doesCardExist(SUIT s, VALUE v) {
            foreach (var c in cardsList) {
                if (c.suit == s &&  c.value == v) return true;
            }
            return false;
        }
    }
}
