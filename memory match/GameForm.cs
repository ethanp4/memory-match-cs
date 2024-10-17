﻿using System;
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
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        const int framerate = 60;
        public static Point mPos;

        public GameForm() {
            InitializeComponent();
            for (int i = 0; i < 10; i++) { // generate 10 pairs
                Card.createCardPair();
            } 
            
            var cards = Card.getCardsList();
            foreach (var c in cards) {
                richTextBox1.Text += c.ToString();
            }

            DoubleBuffered = true;
            Width = 800;
            Height = 600;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            timer.Interval = (int)Math.Floor(1f / (float)framerate * 1000f); // frametime for 60 fps
            timer.Tick += invalidateTimer;
            timer.Start();
        }
        private void invalidateTimer(object sender, EventArgs e) {
            Invalidate(); //repaint once every 16 ms for 60 fps
        }

        protected override void OnPaint(PaintEventArgs e) {
            var g = e.Graphics; // graphics object to draw with
            Game.drawGame(g);
            UI.drawUI(g);
            //base.OnPaint(e); // idk if this is needed
        }

        private void formMouseMove(object sender, MouseEventArgs e) {
            mPos = e.Location; //this needs to be set from here in order to get the local position
        }
    }
    public static class UI {
        private static Font font = new Font("Times New Roman", 16);
        public static void drawUI(Graphics g) {
            g.DrawString("Mouse position: " + GameForm.mPos.ToString(), font, Brushes.Black, new Point(0, 0));
        }
    }


    public static class Game {
        public static void drawGame(Graphics g) {
            g.DrawEllipse(Pens.Aqua, new Rectangle(100, 100, 100, 100));
        }
    }
}
