using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Pairs_Game
{
    public partial class Form1 : Form
    {
        Label firstLabelClicked = null;
        Label secondLabelClicked = null;

        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "d", "d", "%", "%", "@", "@", "~", "~",
            "-", "-", "Y", "Y", "N", "N", "e", "e"
        };


        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    //This makes the text the same color as the background so it is invisible 
                    iconLabel.ForeColor = iconLabel.BackColor;

                    //This removes the previously selected icon from the available options to generate next
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label_click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true) //This ignores other clicks if the timer is running.
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                //If the color is alredy black it does nothing
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //If the variable firstLabelClicked is null then set it to black
                if (firstLabelClicked == null)
                {
                    firstLabelClicked = clickedLabel; //Sets the variable to the clicked label.
                    firstLabelClicked.ForeColor = Color.Black;
                    return;
                }

                secondLabelClicked = clickedLabel; //Sets the variable to the clicked label.
                secondLabelClicked.ForeColor = Color.Black; //Sets that label to black.

                ValidateWin(); //This executes the popup message if all items are matched.

                if (firstLabelClicked.Text == secondLabelClicked.Text)
                {
                    firstLabelClicked = null;
                    secondLabelClicked = null;
                    return;
                }

                timer1.Start();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); //Makes sure the clock is not running. This stops the clock before the click starts. 
            firstLabelClicked.ForeColor = firstLabelClicked.BackColor;
            secondLabelClicked.ForeColor = secondLabelClicked.BackColor;
            firstLabelClicked = null;
            secondLabelClicked = null;
        }

        private void ValidateWin()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            { 
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("You matched all the icons!", "Congradulations, you win!");
            Close();

        }
    }
} 
