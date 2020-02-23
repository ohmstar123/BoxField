﻿using System;
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
        Random randGen = new Random();
        int alphaStart, redStart, greenStart, blueStart;
        int randomX;

        SolidBrush boxBrush = new SolidBrush(Color.White);
        SolidBrush heroBrush = new SolidBrush(Color.White);

        //create a list to hold a column of boxes

        List<Box> boxesLeft = new List<Box>();
        List<Box> boxesRight = new List<Box>();


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
            Box b = new Box(boxBrush, 4, 36, 10);
            boxesLeft.Add(b);

            Box a = new Box(boxBrush, 885, 36, 10);
            boxesRight.Add(a);

            hero = new Box(boxBrush, 50, 300, 20);
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

            foreach (Box a in boxesRight)
            {
                a.Fall();
            }

            //TODO - remove box if it has gone of screen
            if (boxesLeft[0].y > this.Height)
            {
                boxesLeft.RemoveAt(0);
            }

            if (boxesRight[0].y > this.Height)
            {
                boxesRight.RemoveAt(0);
            }
            //TODO - add new box if it is time
            counter++;
            if (counter == 20)
            {
                alphaStart = randGen.Next(1, 256);
                redStart = randGen.Next(1, 256);
                greenStart = randGen.Next(1, 256);
                blueStart = randGen.Next(1, 256);
                randomX = randGen.Next(1, this.Width);

                boxBrush = new SolidBrush(Color.FromArgb(alphaStart, redStart, greenStart, blueStart));

                
                Box box = new Box(boxBrush, randomX, 36, 10);
                boxesLeft.Add(box);

                Box box2 = new Box(boxBrush, randomX, 36, 10);
                boxesRight.Add(box2);

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

            foreach (Box a in boxesRight)
            {
                e.Graphics.FillRectangle(boxBrush, a.x, a.y, a.size, a.size);
            }

            e.Graphics.FillRectangle(heroBrush, hero.x, hero.y, hero.size, hero.size);
        }
    }
}
