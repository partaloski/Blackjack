using BlackJack;
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


    public partial class Form1 : Form
    {


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        //All variables
        private bool dealerAnimStarted;
        private bool playerAnimStarted1;
        private bool playerAnimStarted2;
        private bool anybuttonClicked;
        private bool goingUp;
        private bool goingDownP1;
        private bool goingDownP2;
        private bool goingDownD;
        private PictureBox playerAnimPic1;
        private PictureBox playerAnimPic2;
        private PictureBox dealerAnimPic;
        public List<PictureBox> dealerPic;
        public List<PictureBox> playerPic;
        public List<Card> dealerCards;
        public List<Card> playerCards;
        public bool[] usedDealer = new bool[6];
        public bool[] usedPlayer = new bool[6];
        private Game game;
        private long funds;
        public long bet;
        private int heightShouldBe;
        private int offsetDealer;
        private int offsetPlayer;
        private int playerOff;
        private int dealerOff;
        //Constructor
        public Form1()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            anybuttonClicked = false;
            InitializeComponent();
            Controls controls = new Controls();
            controls.ShowDialog();
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
            for (int i = 0; i < 6; i++)
            {
                usedDealer[i] = false;
                usedPlayer[i] = false;
            }

            Image img = Image.FromFile("./PNG/empty.png");
            //Asssigned values to all PictureBoxes for the player
            my1.Image = img;
            my2.Image = img;
            my3.Image = img;
            my4.Image = img;
            my5.Image = img;
            my6.Image = img;
            //Asssigned values to all PictureBoxes for the dealer
            dealer1.Image = img;
            dealer2.Image = img;
            dealer3.Image = img;
            dealer4.Image = img;
            dealer5.Image = img;
            dealer6.Image = img;
            //Set used cards to not be used
            dealerCards = new List<Card>();
            playerCards = new List<Card>();
            //setting funds to starting ps;
            updateFunds();
            playerAnimStarted1 = false;
            playerAnimStarted2 = false;
            dealerAnimStarted = false;
            goingDownP1 = false;
            goingDownP2 = false;

            playerOff = this.Size.Height + 50 + my1.Size.Height;
            dealerOff = 0 - 50 - dealer1.Size.Height;
            heightShouldBe = dealer1.Size.Height;
            offsetDealer = dealer1.Location.Y;
            offsetPlayer = my1.Location.Y;
        }

        //First, you need to deposit money to play.
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Deposit form = new Deposit();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes)
            {
                funds += form.credits;
                updateFunds();
                btnStart.Enabled = true;
                btnJustStart.Enabled = true;
            }
            resetFocus();
        }

        //Then we can start the game using an amount of credits.
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame form = new StartGame(funds);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                start();
                bet = form.value;
                funds -= bet;
                game.gameStarted = true;
                hit(true);
                hit(false);
                hit(true);
                btnDouble.Visible = btnHit.Visible;
                dealer2.Image = Image.FromFile("./PNG/back.png");
                game.getCards();
                lblDealer.Text = game.currentDealer;
                lblPlaye.Text = game.currentPlayer;
                updateCards();
                end();
                updateFunds();
            }
            resetFocus();
        }


        //A function for the just start button
        private void btnJustStart_Click(object sender, EventArgs e)
        {
            if (funds == 0)
            {
                MessageBox.Show("You do not have funds to start the game.");
                resetFocus();
                return;
            }
            else if (StartGame.lastBet > funds){
                StartGame.lastBet = funds;
            }
            start();
            bet = StartGame.lastBet;
            funds -= bet;
            game.gameStarted = true;
            hit(true);
            hit(false);
            hit(true);
            btnDouble.Visible = btnHit.Visible;
            dealer2.Image = Image.FromFile("./PNG/back.png");
            game.getCards();
            lblDealer.Text = game.currentDealer;
            lblPlaye.Text = game.currentPlayer;
            updateCards();
            end();
            updateFunds();
            resetFocus();
        }
        //We wrote a specific function so that the code can be reused.
        private void start()
        {
            btnHit.Enabled = true;
            btnDouble.Visible = btnHit.Visible;
            btnDouble.Enabled = true;
            btnStand.Enabled = true;
            btnStart.Enabled = false;
            btnJustStart.Enabled = false;
            //Set up buttons in order to make them clickable
            dealerPic = new List<PictureBox>();
            dealerPic.Add(dealer1);
            dealerPic.Add(dealer2);
            dealerPic.Add(dealer3);
            dealerPic.Add(dealer4);
            dealerPic.Add(dealer5);
            dealerPic.Add(dealer6);
            game.dealerCards = new List<Card>();
            //Set cards and pictures of cards for the dealer
            playerPic = new List<PictureBox>();
            playerPic.Add(my1);
            playerPic.Add(my2);
            playerPic.Add(my3);
            playerPic.Add(my4);
            playerPic.Add(my5);
            playerPic.Add(my6);
            game.playerCards = new List<Card>();
            //Set cards and pictures of cards for the player
            updateFunds();
            clearCards();
            for (int i = 0; i < 6; i++)
            {
                usedDealer[i] = false;
                usedPlayer[i] = false;
            }
            //Setting booleans and updating images and funds
        }
        //Double button function
        private async void btnDouble_Click(object sender, EventArgs e)
        {
            if (anybuttonClicked) return;
            anybuttonClicked = true;
            btnStand.Enabled = false;
            btnHit.Enabled = false;
            if (funds >= bet)
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                funds -= bet;
                bet *= 2;
                hit(true);
                game.getCards();
                lblDealer.Text = game.currentDealer;
                lblPlaye.Text = game.currentPlayer;
                game.pdouble();
                game.stand();
                updateCards();
                updateFunds();
                while (game.checkForWin() == "continue")
                {
                    await Task.Delay(800);
                    hit(false);
                    updateCards();
                    btnHit.Enabled = false;
                    btnStand.Enabled = false;
                }
                end();
            }
            else
            {
                MessageBox.Show("Cannot bet double since your funds are low.");
                btnStand.Enabled = true;
                btnHit.Enabled = true;
                btnDouble.Visible = false;
            }
            resetFocus();
            anybuttonClicked = false;
        }
        //Stand button function
        private async void btnStand_Click(object sender, EventArgs e)
        {
            if (anybuttonClicked) return;
            anybuttonClicked = true;
            btnStand.Enabled = false;
            btnHit.Enabled = false;
            btnDouble.Enabled = false;
            bool firstDealer = true;
            if (game.gameStarted == false || game.doubled())
                return;
            game.stand();
            while (game.checkForWin() == "continue")
            {
                await Task.Delay(800);
                if (firstDealer)
                {
                    hit(false);
                }
                updateCards();
            }
            end();
            resetFocus();
            anybuttonClicked = false;
        }

        //Hit button function
        private void btnHit_Click(object sender, EventArgs e)
        {
            if (anybuttonClicked) return;
            anybuttonClicked = true;
            if (game.gameStarted == false || game.doubled())
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                anybuttonClicked = false;
                return;
            }
            hit(true);
            updateCards();
            end();
            resetFocus();
            anybuttonClicked = false;
        }

        //A function to update the images of the cards
        //A simple function that makes the code reusable
        private void updateCards()
        {
            for (int i = 0; i < 6 && i < dealerCards.Count; i++)
            {
                if (!usedDealer[i])
                {
                    if (i != 1)
                    {
                        dealerPic[i].Image = dealerCards[i].image;
                    }
                    usedDealer[i] = true;
                }
            }

            for (int i = 0; i < 6 && i < playerCards.Count; i++)
            {
                if (!usedPlayer[i])
                {
                    playerPic[i].Image = playerCards[i].image;
                    usedPlayer[i] = true;
                }
            }

            lblDealer.Text = game.currentDealer;
            lblPlaye.Text = game.currentPlayer;
        }

        //A function to set the images to the default placeholder
        private void clearCards()
        {
            foreach (PictureBox picture in playerPic)
            {
                picture.Image = Image.FromFile("./PNG/empty.png");
            }
            foreach (PictureBox picture in dealerPic)
            {
                picture.Image = Image.FromFile("./PNG/empty.png");
            }
        }

        //A quick function to update the funds label of the form
        private void updateFunds()
        {
            long tmp = funds;
            List<long> ints = new List<long>();
            while (tmp > 0)
            {
                ints.Add(tmp % 1000);
                tmp /= 1000;
            }
            string xx = "";
            for (int i = ints.Count - 1; i >= 0; i--)
            {
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


        //A function that checks for an end and if one is seen 
        //The game is being resetted while you are told that you lost/won and how much, etc etc.
        private async void end()
        {
            string outx = game.checkForWin();
            updateCards();
            if (outx == "blackjack")
            {
                long x = Convert.ToInt64(Convert.ToDouble(bet) * 2.5);
                funds += x;
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                await Task.Delay(400);
                MessageBox.Show("BLACKJACK!!!! YOU WON " + (x).ToString() + " WITH YOUR BET OF " + bet.ToString() + "!!!!");
            }
            else if (outx == "player")
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                funds += bet * 2;
                await Task.Delay(400);
                MessageBox.Show("YOU WON " + (bet * 2).ToString() + " WITH YOUR BET OF " + bet.ToString() + "!!!!");
            }
            else if (outx == "dealerBlackjack")
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                await Task.Delay(400);
                MessageBox.Show("THE DEALER HAD BLACKJACK, HE WON!!!!");
            }
            else if (outx == "push")
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                funds += bet;
                await Task.Delay(400);
                MessageBox.Show("YOU GET YOUR MONEY BACK!");
            }
            else if (outx == "dealer")
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                await Task.Delay(400);
                MessageBox.Show("YOU LOST " + (bet).ToString() + ", THE DEALER HAS WON! Better luck next time!");
            }
            else if (outx == "continue") return;
            game.reset();
            dealerCards = new List<Card>();
            playerCards = new List<Card>();
            game.currentDealer = "";
            game.currentPlayer = "";
            lblDealer.Text = "";
            lblPlaye.Text = "";
            clearCards();
            updateFunds();
            btnStart.Enabled = true;
            btnJustStart.Enabled = true;
            btnDouble.Enabled = false;
            btnStand.Enabled = false;
            btnHit.Enabled = false;
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (funds == 0)
            {
                MessageBox.Show("You cannot refund since you do not have enough chips.");
                resetFocus();
                return;
            }
            Withdraw withdraw = new Withdraw(funds);

            withdraw.ShowDialog();
            if (withdraw.DialogResult == DialogResult.Yes)
            {
                funds -= withdraw.withdraw_amount;
                updateFunds();
            }
            resetFocus();
        }

        //A simple event caller function that toggles the showcasing of the player's balance and clearing up the game a little
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            int shouldBeL = 39;
            int shouldBe = 70;
            goingUp = (shouldBeL <= label1.Top && shouldBe <= lblFunds.Top);
            timerAnim.Interval = 3;
            timerAnim.Start();
            resetFocus();
        }

        private void timerAnim_Tick(object sender, EventArgs e)
        {
            if (this.goingUp && label1.Location.Y > -90 && lblFunds.Location.Y > -90)
            {
                int labelp = label1.Location.Y - 10;
                int fundsp = lblFunds.Location.Y - 10;
                label1.Location = new Point(label1.Location.X, labelp);
                lblFunds.Location = new Point(lblFunds.Location.X, fundsp);
                return;
            }
            else if (this.goingUp)
            {
                timerAnim.Stop();
                button1.Enabled = true;
            }
            if (!this.goingUp && label1.Location.Y <= 39 && lblFunds.Location.Y <= 70)
            {
                int labelp = label1.Location.Y + 10;
                int fundsp = lblFunds.Location.Y + 10;
                label1.Location = new Point(label1.Location.X, labelp);
                lblFunds.Location = new Point(lblFunds.Location.X, fundsp);
                return;
            }
            else if (!this.goingUp)
            {
                timerAnim.Stop();
                button1.Enabled = true;
            }
        }

        private void animatePlayerCardOne()
        {
            if (playerAnimPic1.Top == offsetPlayer)
                goingDownP1 = true;
            else
                goingDownP1 = false;
            playerAnim.Start();
        }

        private void animatePlayerCardTwo()
        {
            if (playerAnimPic2.Top == offsetPlayer)
                goingDownP2 = true;
            else
                goingDownP2 = false;
            playerAnim2.Start();
        }

        private void animateDealerCard()
        {
            //15
            if (dealerAnimPic.Top == offsetDealer)
                goingDownD = true;
            else
                goingDownD = false;
            dealerAnim.Start();
        }

        private void playerAnim_Tick(object sender, EventArgs e)
        {
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnDouble.Enabled = false;
            int pos;
            if (goingDownP1)
                pos = playerAnimPic1.Top + 25;
            else
                pos = playerAnimPic1.Top - 25;
            if (pos > playerOff)
                pos = playerOff;
            else if (pos < offsetPlayer)
                pos = offsetPlayer;
            playerAnimPic1.Location = new Point(playerAnimPic1.Location.X, pos);
            if (playerAnimPic1.Top == playerOff || playerAnimPic1.Top == offsetPlayer)
            {
                playerAnimStarted1 = false;
                playerAnim.Stop();
                if (!playerAnimStarted1 && !playerAnimStarted2)
                {
                    btnHit.Enabled = true;
                    btnStand.Enabled = true;
                    btnDouble.Enabled = true;
                }
            }

        }

        private void dealerAnim_Tick(object sender, EventArgs e)
        {
            int pos;
            if (goingDownD)
                pos = dealerAnimPic.Top - 25;
            else
                pos = dealerAnimPic.Top + 25;
            if (pos < dealerOff)
                pos = dealerOff;
            else if (pos > offsetDealer)
                pos = offsetDealer;
            dealerAnimPic.Location = new Point(dealerAnimPic.Location.X, pos);
            if (dealerAnimPic.Top == offsetDealer || dealerAnimPic.Top == dealerOff)
            {
                dealerAnimStarted = false;
                dealerAnim.Stop();
            }
        }


        private void playerAnim2_Tick(object sender, EventArgs e)
        {
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnDouble.Enabled = false;
            int pos;
            if (goingDownP2)
                pos = playerAnimPic2.Top + 25;
            else
                pos = playerAnimPic2.Top - 25;
            if (pos > playerOff)
                pos = playerOff;
            else if (pos < offsetPlayer)
                pos = offsetPlayer;
            playerAnimPic2.Location = new Point(playerAnimPic2.Location.X, pos);
            if (playerAnimPic2.Top == playerOff || playerAnimPic2.Top == offsetPlayer)
            {
                playerAnimStarted2 = false;
                playerAnim2.Stop();
                if (!playerAnimStarted1 && !playerAnimStarted2)
                {
                    btnHit.Enabled = true;
                    btnStand.Enabled = true;
                    btnDouble.Enabled = true;
                }
            }
        }
        private Size oldSize;
        private void Form1_Load(object sender, EventArgs e) => oldSize = base.Size;

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            /*
             * 
             * 
            Size size = this.Size;
            int h = size.Height;
            int w = size.Width;
            double ratioS = 1.8479;
            double ratioI = Convert.ToDouble(w) / Convert.ToDouble(h);
            if(ratioI != ratioS)
            {
                h = Convert.ToInt32(ratioS * Convert.ToDouble(w));
            }
            this.Size = new Size(w, h);
             */
            int width = newSize.Width - oldSize.Width;
            control.Left += (control.Left * width) / oldSize.Width;
            control.Width += (control.Width * width) / oldSize.Width;

            int height = newSize.Height - oldSize.Height;
            control.Top += (control.Top * height) / oldSize.Height;
            control.Height += (control.Height * height) / oldSize.Height;



            playerOff = this.Size.Height + 50 + my1.Size.Height;
            dealerOff = 0 - 50 - dealer1.Size.Height;
            heightShouldBe = dealer1.Size.Height;
            offsetDealer = dealer1.Location.Y;
            offsetPlayer = my1.Location.Y;
        }

        private bool growing;
        private bool firstDealer = false;
        private void resizeStart()
        {
            firstDealer = true;
            growing = false;
            resizeTimer.Start();
        }
        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            int size = dealer2.Size.Height;
            int height = dealer2.Location.Y;
            if (size <= heightShouldBe / 2 && !growing)
            {
                dealer2.Image = dealerCards[1].image;
                size = heightShouldBe / 2;
                growing = true;
            }
            if (size >= heightShouldBe && growing)
            {
                size = heightShouldBe;
                height = offsetDealer;
                dealer2.Size = new Size(dealer2.Size.Width, size);
                dealer2.Location = new Point(dealer2.Location.X, height);
                resizeTimer.Stop();
                return;
            }
            if (!growing)
            {
                height += 12;
                size -= 25;
            }

            else if (growing)
            {
                height -= 12;
                size += 25;
            }
            dealer2.Size = new Size(dealer2.Size.Width, size);
            dealer2.Location = new Point(dealer2.Location.X, height);
        }


        //A function used whenever one of the two hits.
        //Entry parameter is wether the one hitting is the player, if so, the value to send as an argument is true.
        //Otherwise it's false.
        private void hit(Boolean player)
        {
            if (game.gameStarted == false)
                return;
            btnDouble.Visible = false;
            Card c = game.getCard(true);
            if (player)
            {
                playerCards.Add(c);
                game.playerCards.Add(c);
                int index = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (usedPlayer[i] == false)
                    {
                        index = i;
                        break;
                    }
                }
                if (game.checkForWin() == "blackjack")
                {
                    btnHit.Enabled = false;
                    btnStand.Enabled = false;
                }
                playerPic[index].Location = new Point(playerPic[index].Location.X, playerOff);
                playerPic[index].Image = c.image;
                PictureBox playerAnimPicAnon = playerPic[index];
                if (!playerAnimStarted1)
                {
                    btnHit.Enabled = false;
                    btnStand.Enabled = false;
                    btnDouble.Enabled = false;
                    playerAnimStarted1 = true;
                    playerAnimPic1 = playerAnimPicAnon;
                    playerAnim.Start();
                }
                else
                {
                    btnHit.Enabled = false;
                    btnStand.Enabled = false;
                    btnDouble.Enabled = false;
                    playerAnimStarted2 = true;
                    playerAnimPic2 = playerAnimPicAnon;
                    playerAnim2.Start();
                }
            }
            else
            {
                dealerCards.Add(c);
                game.dealerCards.Add(c);
                int index = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (usedDealer[i] == false)
                    {
                        index = i;
                        break;
                    }
                }
                if (index == 1)
                {
                    resizeStart();
                    firstDealer = true;
                }
                if (!firstDealer)
                {
                    dealerPic[index].Location = new Point(dealerPic[index].Location.X, dealerOff);
                    dealerPic[index].Image = c.image;
                }
                game.getCards();
                lblDealer.Text = game.currentDealer;
                lblPlaye.Text = game.currentPlayer;
                PictureBox dealerPicAnon = dealerPic[index];
                dealerAnimStarted = true;
                dealerAnimPic = dealerPicAnon;
                if (!firstDealer)
                    dealerAnim.Start();
                else
                    firstDealer = false;

                if (game.checkForWin() == "blackjack")
                {
                    btnHit.Enabled = false;
                    btnStand.Enabled = false;
                }
            }
            if (game.checkForWin() == "blackjack")
            {
                btnHit.Enabled = false;
                btnStand.Enabled = false;
            }
            updateCards();
        }

        private void my5_Click(object sender, EventArgs e)
        {

        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Space == stand
            //Enter == hit
            //Tab == double
            //Ctrl+N == Start
            //Ctrl+Shift+N == Set and Start
            //Ctrl+D == Deposit
            //Ctrl+W == Withdraw
            Controls controls = new Controls();
            controls.ShowDialog();
        }

        private void hideAllButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = !button1.Visible;
            btnDeposit.Visible = !btnDeposit.Visible;
            btnWithdraw.Visible = !btnWithdraw.Visible;
            btnStart.Visible = !btnStart.Visible;
            btnJustStart.Visible = !btnJustStart.Visible;
        }

        private void toggleFundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
                button1_Click(sender, e);
        }

        private void depositFundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnDeposit.Enabled)
                btnDeposit_Click(sender, e);
        }

        private void withdrawFundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnWithdraw.Enabled)
                btnWithdraw_Click(sender, e);
        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnJustStart.Enabled)
                btnJustStart_Click(sender, e);
        }

        private void changeBetAndStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnStart.Enabled)
                btnStart_Click(sender, e);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string pressedKey = e.KeyCode.ToString();
            if (pressedKey.ToLower() == "s")
            {
                //Stand
                if (btnStand.Enabled && btnStand.Visible) btnStand_Click(sender, e);
            }
            else if (pressedKey.ToLower() == "a")
            {
                //Hit
                if (btnHit.Enabled && btnHit.Visible) btnHit_Click(sender, e);
            }
            else if (pressedKey.ToLower() == "d")
            {
                //Double
                if (btnDouble.Enabled && btnDouble.Visible) btnDouble_Click(sender, e);
            }
        }

        private void dshToggle_Click(object sender, EventArgs e)
        {
            btnStand.Visible = !btnStand.Visible;
            btnHit.Visible = !btnHit.Visible;
            btnDouble.Visible = !btnDouble.Visible;
        }

        private async void resetFocus()
        {
            reverseEnabled();
            await Task.Delay(20);
            reverseEnabled();
        }

        private void reverseEnabled()
        {
            btnHit.Enabled = !btnHit.Enabled;
            btnStand.Enabled = !btnStand.Enabled;
            btnDouble.Enabled = !btnDouble.Enabled;
            btnDeposit.Enabled = !btnDeposit.Enabled;
            btnWithdraw.Enabled = !btnWithdraw.Enabled;
            button1.Enabled = !button1.Enabled;
            btnStart.Enabled = !btnStart.Enabled;
            btnJustStart.Enabled = !btnJustStart.Enabled;
        }
    }
}
