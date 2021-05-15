using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Prototyping_of_Project
{
    public class Card
    {
        //Variables
        public string num { get; private set; }

        public string type { get; private set; }

        public Image image { get; private set; }

        //Default constructor.
        public Card(string num, string type)
        {
            this.num = num;
            this.type = type;
            this.image = Image.FromFile("./PNG/" + num + type + ".png");
        }

        //A function that gets us the value of a card.
        public int getValue()
        {
            if(num == "10" || num == "J" || num == "Q" || num == "K")
                return 10;
            else if (num == "A")
                return 11;
            else
                return Int32.Parse(num);
        }
        
        //A function that tells us if a card is an Ace or not. Just used for a shortcut
        public bool isAce()
        {
            return "A" == num;
        }
    }
}
