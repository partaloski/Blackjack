using System;
using System.Collections.Generic;
using System.Text;
namespace Prototyping_of_Project
{
    public class Game
    {
        //Variables
        public List<Card> deck { get; set; }
        public List<Card> playerCards { get; set; }
        public List<Card> dealerCards { get; set; }
        public string currentPlayer { get; set; }
        public string currentDealer { get; set; }
        private bool playerStood { get; set; }
        private bool playerHit { get; set; }

        //Default constructor
        public Game(){
            Deck deck = new Deck(2);
            this.deck = deck.cards;
            this.playerCards = new List<Card>();
            this.dealerCards = new List<Card>();
            reset();
        }

        //A very simple shortcut to tell the game that the player has pressed the stand button
        public void stand(){
            playerStood = true;
        }

        //Pulls the first card of a shuffled deck of cards, if the deck of cards has no cards, it gets 2 more decks.
        public Card getCard(bool player){
            if(deck.Count == 0){
                Deck deck = new Deck(2);
                this.deck = deck.cards;
            }
            Card gotten = deck[0];
            deck.Remove(gotten);
            return gotten;
        }

        //Gets the sum of the cards of both the dealer and player in the form of an array
        //Where
        // 0 ==  sumPlayer // 1 == sumPlayerAce // 2 == sumDealer // 3 == sumDealerAce
        public int[] getCards(){
            int sumPlayer = 0;
            int sumPlayerAce = 0;
            int sumDealer = 0;
            int sumDealerAce = 0;
            bool playerAce = false;
            bool dealerAce = false;
            foreach (Card card in playerCards){
                if (card.isAce()){
                    sumPlayer += 1;
                    sumPlayerAce += 11;
                    if (sumPlayerAce > 21)
                        sumPlayerAce -= 10;
                }
                else{
                    sumPlayerAce += card.getValue();
                    sumPlayer += card.getValue();
                }
            }
            foreach (Card card in dealerCards){
                if (card.isAce()) {
                    sumDealer += 1;
                    sumDealerAce += 11;
                    if (sumDealerAce > 21)
                        sumDealerAce -= 10;
                }
                else{
                    sumDealerAce += card.getValue();
                    sumDealer += card.getValue();
                }
            }
            if (sumPlayerAce > 21)
                sumPlayerAce = sumPlayer;
            if (sumDealerAce > 21)
                sumDealerAce = sumDealer;
            int[] ints = new int[4];
            ints[0] = sumPlayer;
            ints[1] = sumPlayerAce;
            ints[2] = sumDealer;
            ints[3] = sumDealerAce;
            if (playerStood)
                currentPlayer = Math.Max(sumPlayer, sumPlayerAce).ToString();
            else if (ints[0] != ints[1])
                currentPlayer = sumPlayer.ToString() + " / " + sumPlayerAce.ToString();
            else
                currentPlayer = sumPlayer.ToString();
            
            if (ints[2] != ints[3])
                currentDealer = sumDealer.ToString() + " / " + sumDealerAce.ToString();
            else
                currentDealer = sumDealer.ToString();
            return ints;
        }
        //A function I used to use back in the early stages to see if multiple aces were into a given list of cards
        private bool OneOrNone(List<Card> cards){
            int i = 0;
            foreach (Card card in cards){
                if (card.isAce()) i++;
            }
            return i < 2;
        }
        //A function that checks if there is a win
        //Returns "continue" that means the game hasn't come to an end
        //Returns "blackjack" that means the player won by a blackjack
        //Returns "dealer" that means the game has come to an end and the dealer won.
        //Returns "player" that means the game has come to an end and the player won.
        public string checkForWin(){
            int[] f = getCards();
            int sumPlayer = f[0];
            int sumPlayerA = f[1];
            int sumDealer = f[2];
            int sumDealerA = f[3];
            int player = Math.Max(sumPlayer, sumPlayerA);
            int dealer = Math.Max(sumDealer, sumDealerA);
            if (player == 21 && playerCards.Count == 2)
                return "blackjack";
            else if (player > 21)
                return "dealer";
            else if (dealer > 21)
                return "player";
            else if (dealer == 21 && dealerCards.Count == 2)
                return "dealer";
            else if (dealer == 17){
                if (player > dealer)
                    return "player";
                else if (player < dealer)
                    return "dealer";
                else
                    return "push";
            }
            else if (playerStood){
                if (dealer > player)
                    return "dealer";
                else if (dealer == player)
                    return "push";
                else
                    return "continue";
            }
            else
                return "continue";
        }

        //A function used whenever the game comes to an end and used to restart all the fields but keep the deck.
        public void reset(){
            this.playerCards = new List<Card>();
            this.dealerCards = new List<Card>();
            this.playerStood = false;
            this.playerHit = false;
            currentDealer = "";
            currentPlayer = "";
        }
    }
}
