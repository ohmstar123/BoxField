using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);

        //create a list to hold a column of boxes
        List<Box> boxesLeft = new List<Box>();

        //counter
        int counter;

        //create hero values
        Box hero;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            //TODO - set game start values
            Box b = new Box(4, 36, 10);
            boxesLeft.Add(b);

            hero = new Box(50, 300, 20);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //TODO - update location of all boxes (drop down screen)
            foreach (Box b in boxesLeft)
            {
                b.Fall();
            }
            //TODO - remove box if it has gone of screen
            if (boxesLeft[0].y > 200)
            {
                boxesLeft.RemoveAt(0);
            }
            //TODO - add new box if it is time
            counter++;
            if (counter == 5)
            {
                Box box = new Box(4, 36, 10);
                boxesLeft.Add(box);
                counter = 0;
            }

            //move hero
            if (leftArrowDown)
            {
                hero.Move("left");
            }

            if (rightArrowDown)
            {
                hero.Move("right");
            }
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //TODO - draw boxes to screen
            foreach (Box b in boxesLeft)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            e.Graphics.FillRectangle(boxBrush, hero.x, hero.y, hero.size, hero.size);
        }
    }
}
