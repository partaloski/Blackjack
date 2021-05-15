using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototyping_of_Project
{
    public partial class Form1 : Form {
        //All variables
        public List<PictureBox> dealerPic;
        public List<PictureBox> playerPic;
        public List<Card> dealerCards;
        public List<Card> playerCards;
        public bool[] usedDealer = new bool[5];
        public bool[] usedPlayer = new bool[5];
        private Game game;
        private long funds;
        public long bet;
        //Constructor
        public Form1(){
            InitializeComponent();
            funds = 0;
            bet = 0;
            game = new Game();
            //Setting defaults
            dealerPic = new List<PictureBox>();
            dealerPic.Add(dealer1);
            dealerPic.Add(dealer2);
            dealerPic.Add(dealer3);
            dealerPic.Add(dealer4);
            dealerPic.Add(dealer5);
            //Dealer pictures added
            playerPic = new List<PictureBox>();
            playerPic.Add(my1);
            playerPic.Add(my2);
            playerPic.Add(my3);
            playerPic.Add(my4);
            playerPic.Add(my5);
            //Player's pictures added
            for (int i = 0; i < 5; i++){
                usedDealer[i] = false;
                usedPlayer[i] = false;
            }

            Image img = Image.FromFile("./PNG/empty.png");
            my1.Image = img;
            my2.Image = img;
            my3.Image = img;
            my4.Image = img;
            my5.Image = img;

            dealer1.Image = img;
            dealer2.Image = img;
            dealer3.Image = img;
            dealer4.Image = img;
            dealer5.Image = img;
            //Set used cards to not be used
            dealerCards = new List<Card>();
            playerCards = new List<Card>();
            //setting funds to starting ps;
            updateFunds();
        }

        //First, you need to deposit money to play.
        private void btnDeposit_Click(object sender, EventArgs e) {
            Deposit form = new Deposit();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes) {
                funds += form.credits;
                updateFunds();
                btnStart.Enabled = true;
            }
        }

        //Then we can start the game using an amount of credits.
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame form = new StartGame(funds);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK){
                start();
                bet = form.value;
                funds -= bet;
                hit(true);
                hit(false);
                hit(true);
                btnDouble.Visible = true;
                dealer2.Image = Image.FromFile("./PNG/back.png");
                game.getCards();
                lblDealer.Text = game.currentDealer;
                lblPlayer.Text = game.currentPlayer;
                updateCards();
                end();
                updateFunds();
                btnStand.Enabled = true;
                btnHit.Enabled = true;
            }
        }
        //We wrote a specific function so that the code can be reused.
        private void start(){
            btnHit.Enabled = true;
            btnDouble.Enabled = true;
            btnStand.Enabled = true;
            btnStart.Enabled = false;
            //Set up buttons in order to make them clickable
            dealerPic = new List<PictureBox>();
            dealerPic.Add(dealer1);
            dealerPic.Add(dealer2);
            dealerPic.Add(dealer3);
            dealerPic.Add(dealer4);
            dealerPic.Add(dealer5);
            game.dealerCards = new List<Card>();
            //Set cards and pictures of cards for the dealer
            playerPic = new List<PictureBox>();
            playerPic.Add(my1);
            playerPic.Add(my2);
            playerPic.Add(my3);
            playerPic.Add(my4);
            playerPic.Add(my5);
            game.playerCards = new List<Card>();
            //Set cards and pictures of cards for the player
            updateFunds();
            clearCards();
            for (int i = 0; i < 5; i++){
                usedDealer[i] = false;
                usedPlayer[i] = false;
            }
            //Setting booleans and updating images and funds
        }
        //Double button function
        private void btnDouble_Click(object sender, EventArgs e)
        {
            btnStand.Enabled = false;
            btnHit.Enabled = false;
            if (funds >= bet){
                funds -= bet;
                bet *= 2;
                hit(true);
                game.getCards();

                lblDealer.Text = game.currentDealer;
                lblPlayer.Text = game.currentPlayer;
                updateCards();
                updateFunds();

            }
            else{
                MessageBox.Show("Cannot bet double since your funds are low.");
            }
            btnStand.Enabled = true;
            btnHit.Enabled = true;
        }
        //Stand button function
        private async void btnStand_Click(object sender, EventArgs e){
            btnStand.Enabled = false;
            btnHit.Enabled = false;
            game.stand();
            while (game.checkForWin() == "continue"){
                await Task.Delay(750);
                hit(false);
                updateCards();
            }
            end();

            btnStand.Enabled = true;
            btnHit.Enabled = true;
        }

        //Hit button function
        private void btnHit_Click(object sender, EventArgs e){
            hit(true);
            updateCards();
            end();
        }

        //A function to update the images of the cards
        //A simple function that makes the code reusable
        private void updateCards(){
            for (int i = 0; i < 5 && i < dealerCards.Count; i++){
                if (!usedDealer[i]){
                    dealerPic[i].Image = dealerCards[i].image;
                    usedDealer[i] = true;
                }
            }

            for (int i = 0; i < 5 && i < playerCards.Count; i++){
                if (!usedPlayer[i]){
                    playerPic[i].Image = playerCards[i].image;
                    usedPlayer[i] = true;
                }
            }

            lblDealer.Text = game.currentDealer;
            lblPlayer.Text = game.currentPlayer;
        }

        //A function to set the images to the default placeholder
        private void clearCards()
        {
            foreach (PictureBox picture in playerPic){
                picture.Image = Image.FromFile("./PNG/empty.png");
            }
            foreach (PictureBox picture in dealerPic){
                picture.Image = Image.FromFile("./PNG/empty.png");
            }
        }

        //A quick function to update the funds label of the form
        private void updateFunds(){
            string str = "";
            long tmp = funds;
            List<long> ints = new List<long>();
            while (tmp > 0){
                ints.Add(tmp % 1000);
                tmp /= 1000;
            }
            string xx = "";
            for(int i=ints.Count-1; i>=0; i--){
                if (i == ints.Count - 1)
                    xx += ints[i].ToString();
                else
                    xx += ints[i].ToString("000");
                if (i != 0)
                    xx += ",";
            }
            if (xx == "")
                xx = "0";
            lblFunds.Text = xx + "$";
        }

        //A function used whenever one of the two hits.
        //Entry parameter is wether the one hitting is the player, if so, the value to send as an argument is true.
        //Otherwise it's false.
        private void hit(Boolean player)
        {
            btnDouble.Visible = false;
            Card c = game.getCard(true);
            if (player){
                playerCards.Add(c);
                game.playerCards.Add(c);
                int index = 0;
                for (int i = 0; i < 5; i++){
                    if (usedPlayer[i] == false){
                        index = i;
                        break;
                    }
                }
                playerPic[index].Image = c.image;
            }
            else{
                dealerCards.Add(c);
                game.dealerCards.Add(c);
                int index = 0;
                for (int i = 0; i < 5; i++){
                    if (usedDealer[i] == false){
                        index = i;
                        break;
                    }
                }
                dealerPic[index].Image = c.image;
                game.getCards();
                lblDealer.Text = game.currentDealer;
                lblPlayer.Text = game.currentPlayer;
            }
            updateCards();
        }

        //A function that checks for an end and if one is seen 
        //The game is being resetted while you are told that you lost/won and how much, etc etc.
        private void end(){
            string outx = game.checkForWin();
            updateCards();
            if (outx == "blackjack"){
                long x = Convert.ToInt64(Convert.ToDouble(bet) * 2.5);
                funds += x;
                MessageBox.Show("BLACKJACK!!!! YOU WON " + (x).ToString() + " WITH YOUR BET OF " + bet.ToString() + "!!!!");
            }
            else if (outx == "player"){
                funds += bet * 2;
                MessageBox.Show("YOU WON " + (bet * 2).ToString() + " WITH YOUR BET OF " + bet.ToString() + "!!!!");
            }
            else if (outx == "push"){
                funds += bet;
                MessageBox.Show("YOU GET YOUR MONEY BACK!");
            }
            else if(outx == "dealer"){
                MessageBox.Show("YOU LOST " + (bet).ToString() + ", THE DEALER HAS WON! Better luck next time!");
            }
            if (outx == "continue") return;
            game.reset();
            dealerCards = new List<Card>();
            playerCards = new List<Card>();
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnDouble.Enabled = false;
            game.currentDealer = "";
            game.currentPlayer = "";
            lblDealer.Text = "";
            lblPlayer.Text = "";
            clearCards();
            updateFunds();
            btnStart.Enabled = true;
            btnStand.Enabled = false;
            btnHit.Enabled = false;
        }
    }
}
