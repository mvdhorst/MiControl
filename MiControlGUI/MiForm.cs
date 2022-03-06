﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MiControl;
using MiControl.Colors.Video;

namespace MiControlGUI
{
    public partial class MiForm : Form
    {
        Controller Controller = new Controller("255.255.255.255");
        ScreenColor Ambilight = new ScreenColor();

        public MiForm()
        {
            InitializeComponent();
        }
        
        void MiFormLoad(object sender, EventArgs e)
		{
        	cmbGroup.SelectedIndex = 0;
		}
        
        void ItemIPAddressLeave(object sender, EventArgs e)
		{
        	Controller = new Controller(itemIPAddress.Text);
		}
        
        private void btnOn1_Click(object sender, EventArgs e)
        {
            Controller.White.SwitchOn(1);
        }
        
        private void btnOff1_Click(object sender, EventArgs e)
        {
        	Controller.White.SwitchOff(1);
        }
        
        private void trackBrightness1_Scroll(object sender, EventArgs e)
        {
            Controller.White.BrightnessUp(1);
        }
        
        private void btnOn2_Click(object sender, EventArgs e)
        {
            Controller.White.SwitchOn(2);
        }
        
        private void btnOff2_Click(object sender, EventArgs e)
        {
        	Controller.White.SwitchOff(2);
        }
        
        private void trackBrightness2_Scroll(object sender, EventArgs e)
        {
            Controller.White.BrightnessUp(2);
        }

        private void btnOn3_Click(object sender, EventArgs e)
        {
            Controller.White.SwitchOn(3);
        }

        private void btnOff3_Click(object sender, EventArgs e)
        {
            Controller.White.SwitchOff(3);
        }

        private void trackBrightness3_Scroll(object sender, EventArgs e)
        {
            Controller.White.BrightnessUp(3);
        }
        
        private void btnOn4_Click(object sender, EventArgs e)
        {
            Controller.White.SwitchOn(4);
        }
        
        private void btnOff4_Click(object sender, EventArgs e)
        {
        	Controller.White.SwitchOff(4);
        }
        
        private void trackBrightness4_Scroll(object sender, EventArgs e)
        {
            Controller.White.BrightnessUp(4);
        }

        private void btnAmbi_Click(object sender, EventArgs e)
        {
            if(!bwAmbi.IsBusy) {
                bwAmbi.RunWorkerAsync();
            } else {
                bwAmbi.CancelAsync();
            }
        }

        private void bwAmbi_DoWork(object sender, DoWorkEventArgs e)
        {
            Color color;

            while(!bwAmbi.CancellationPending) {
                color = Ambilight.AverageColor();
                btnAmbi.BackColor = color;
                btnAmbi.Invoke((MethodInvoker)delegate {
                    btnAmbi.Text = string.Format("H:{0} S:{1} L:{2}", 
                        color.GetHue(),
                        color.GetSaturation(),
                        color.GetBrightness());
                });

                Controller.RGBW.SetTrueColor(cmbGroup.SelectedIndex, color);
                Thread.Sleep(50);
            }

            btnAmbi.BackColor = SystemColors.Control;
            btnAmbi.Text = "Ambi";
        }	
    }
}
