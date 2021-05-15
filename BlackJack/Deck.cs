using System;
using System.Collections.Generic;
using System.Text;

namespace Prototyping_of_Project
{
    public class Deck
    {
        //Variables including the possibilities of a Card number and sign.
        public List<Card> cards { get; private set; }
        private Random rng = new Random();
        string [] allCards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "A", "J", "Q", "K" };
        string[] allTypes = { "C", "D", "H", "S" };

        //Default constructor.
        public Deck(int decks){
            cards = new List<Card>();
            foreach (string card in allCards){
                foreach(string type in allTypes){
                    Card c = new Card(card, type);
                    for (int i=0; i < decks; i++){
                        cards.Add(c);
                    }
                }
            }
            cards = Shuffle(cards);
        }

        //A function that returns a Shuffled deck 
        public  List<Card> Shuffle(List<Card> list){
            int n = list.Count*10;
            while (n > 1){
                n--;
                int k = rng.Next(0, list.Count - 1);
                int k2 = rng.Next(0, list.Count - 1);
                Card value = list[k];
                list[k] = list[k2];
                list[k2] = value;
            }
            return list;
        }
    }

   
}
