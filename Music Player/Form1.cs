using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;



namespace Music_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Audio files|*.mp3; *.wav; *.flac";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    foreach (string file in openFileDialog.FileNames)
                    {
                        if (listBox1.FindString(file.ToString()) == 0)
                            {
                            MessageBox.Show("There is already that file.");                                
                        }
                        else { listBox1.Items.Add(file.ToString()); }                        
                    }
                }              
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
            MessageBox.Show(axWindowsMediaPlayer1.currentMedia.duration.ToString());
            MessageBox.Show(axWindowsMediaPlayer1.currentMedia.durationString);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex + 2 > listBox1.Items.Count)
                {
                    listBox1.SelectedIndex = 0;
                    axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                    trackBar2.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;
                    Console.WriteLine(((int)axWindowsMediaPlayer1.currentMedia.duration).ToString());
                }
                else
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                    trackBar2.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add songs first.");
            }
        }
        
        
            

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex == 0)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                    trackBar2.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;
                }
                else
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                    axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                    trackBar2.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add songs first.");
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            label1.Text = trackBar1.Value.ToString() + "%";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar2.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            trackBar2.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
        }
    }
}
