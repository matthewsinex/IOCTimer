using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IOCTimer
{
    public partial class Form1 : Form
    {
        private TimeSpan duration, remaining;
        private DateTime startTime;
        private int round = 0;
        private int timerTickCount = 0;
        private int TOTAL_MINUTES = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            duration = new TimeSpan(0, TOTAL_MINUTES, 1);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (round == 0)
            {
                lblRoundTitle.Text = "Poetry Commentary";
                startTime = DateTime.Now;
                timer1.Enabled = true;
                round = 1;
                btnContinue.Text = "Continue";
                lblRoundTitle.Visible = true;
            }

            else
            {
                lblRoundTitle.Text = "Interview";
                duration = new TimeSpan(0, TOTAL_MINUTES, 1);
                btnContinue.Enabled = false;
                startTime = DateTime.Now;
                timer1.Enabled = true;
                timer2.Enabled = false;
                this.BackColor = SystemColors.Control;
                lblTimer.Text = "10:00";
                round = 2;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //calcuates and displays the remaining time
            remaining = duration - (DateTime.Now - startTime);

            //two Minutes for Questions
            if (round == 1 && lblTimer.Text == "02:00")
            {
                twoMinuteWarning();
                lblRoundTitle.Text = "Poetry Commentary Questions";
            }
            
            if (remaining.TotalSeconds > 0)
                lblTimer.Text = remaining.ToString("mm':'ss");
            else
            {
                twoMinuteWarning();
                if (round == 1)
                {
                    round = 2;
                    btnContinue.Enabled = true;
                    btnContinue.Text = "Continue";
                }
                timer1.Enabled = false;

            }
        }

        //resets everything
        private void btnReset_Click(object sender, EventArgs e)
        {
            round = 0;
            lblRoundTitle.Visible = false;
            duration = new TimeSpan(0, TOTAL_MINUTES, 1);
            timer1.Enabled = false;
            timer2.Enabled = false;
            this.BackColor = SystemColors.Control;
            btnContinue.Enabled = true;
            lblTimer.Text = "10:00";
            timerTickCount = 0;
        }

        private void twoMinuteWarning()
        {
            timerTickCount = 0;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (remaining.TotalSeconds <= 0)
            {
                if (this.BackColor == SystemColors.Control)
                    this.BackColor = System.Drawing.Color.Red;
                else
                    this.BackColor = SystemColors.Control;

                timerTickCount += 1;

                if (timerTickCount >= 12)
                    timer2.Enabled = false;
            }
            else
            {
                if (this.BackColor == SystemColors.Control)
                    this.BackColor = System.Drawing.Color.Black;
                else
                    this.BackColor = SystemColors.Control;

                timerTickCount += 1;

                if (timerTickCount >= 10)
                    timer2.Enabled = false;
            }
        }


    }
}
