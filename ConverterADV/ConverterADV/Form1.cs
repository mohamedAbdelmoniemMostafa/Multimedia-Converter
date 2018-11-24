using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using NReco.VideoConverter;

namespace ConverterADV
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        string file, path, type, compare;

       
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file = ofd.FileName;
            }
        }
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (file != compare && type != compare)
            {
                var converter = new NReco.VideoConverter.FFMpegConverter();
                //start Bg worker
                backgroundWorker1.RunWorkerAsync();
                converter.ConvertMedia(file, @path, type);
                bunifuCircleProgressbar1.Visible = true;
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton6.Visible = true;
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            var converter = new NReco.VideoConverter.FFMpegConverter();
            //request cancellation
            backgroundWorker1.CancelAsync();
            converter.Stop();
            bunifuCircleProgressbar1.Value = 0;
            file = compare;
            path = compare;
            type = compare;

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            bunifuFlatButton4.Visible = false;
            bunifuFlatButton5.Visible = true;
            var converter = new NReco.VideoConverter.FFMpegConverter();
            //request cancellation
            backgroundWorker1.CancelAsync();
            converter.Stop();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (file != compare && path != compare)
            {
                var converter = new NReco.VideoConverter.FFMpegConverter();
                //start Bg worker
                backgroundWorker1.RunWorkerAsync();
                converter.ConvertMedia(file, @path, type);
            }
            bunifuCircleProgressbar1.Value = 0;
            file = compare;
            path = compare;
            type = compare;
            bunifuFlatButton5.Visible = false;
            bunifuFlatButton4.Visible = true;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (file != compare && type != compare)
            {
                DialogResult r = saveFileDialog1.ShowDialog();
                if (r == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName + "." + type;

                }
            }
        }

        private void metroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.mp4;
        }
        private void metroRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.avi;
        }
        private void metroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.flv;
        }
        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.wmv;
        }
        private void metroRadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.mov;
        }
        private void metroRadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.m4v;
        }
        private void metroRadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.mpeg;
        }
        private void metroRadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.swf;
        }
        private void metroRadioButton9_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.flv;
        }
        private void metroRadioButton10_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.rm;
        }
        private void metroRadioButton11_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.ast;
        }
        private void metroRadioButton12_CheckedChanged(object sender, EventArgs e)
        {
            type = NReco.VideoConverter.Format.gif;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                //check for cancelled first
                if (backgroundWorker1.CancellationPending)
                {
                    //cancel
                    e.Cancel = true;
                }
                else
                {
                    Rate();
                    backgroundWorker1.ReportProgress(i);
                }
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            bunifuCircleProgressbar1.Value = e.ProgressPercentage;
            bunifuCircleProgressbar1.Update();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("you have cancelled");
            }
            else
            {
                MessageBox.Show("Convert completed successfully");
                bunifuCircleProgressbar1.Value = 0;
                file = compare;
                path = compare;
                type = compare;
            }
        }
        private void Rate()
        {
            //susbend thread 1 s
            Thread.Sleep(1000);
        }
    }
}
