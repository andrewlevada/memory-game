using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		Random rnd = new Random();
		/// <summary>
		/// ПРедыдущая кнопка (КОТОРАЯ СЕЙЧАС открыта)
		/// </summary>
		Button prevBtn = null;
		Button[,] tiles = null;
		const int K = 4;

		public Form1()
		{
			InitializeComponent();
			InitTiles();
		}

        public void InitTiles()
        {
            int[] nums = new int[K * K];

            for (int i = 0; i < K * K; i++)
                nums[i] = i / 2 + 1;
            for (int k = 0; k < 1000; k++)
            {
                int p1 = rnd.Next(K * K);
                int p2 = rnd.Next(K * K);
                int tmp = nums[p1];
                nums[p1] = nums[p2];
                nums[p2] = tmp;
            }

            Size = new Size(417, 440);
            tiles = new Button[4, 4];
            for (int i = 0; i < K; ++i)
                for (int j = 0; j < K; ++j)
                {
                    var nb = new Button();
                    nb.Tag = nums[i * K + j];
                    //nb.Text = nums[i * K + j].ToString();
                    nb.Text = "*";
                    nb.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    nb.Location = new Point(i * 100, j * 100);
                    nb.Size = new Size(100, 100);
                    nb.Click += button1_ClickAsync;
                    nb.Parent = this;
                    tiles[i, j] = nb;
                }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (prevBtn == null)
            {
                prevBtn = sender as Button;
                prevBtn.Text = prevBtn.Tag.ToString();
                prevBtn.Enabled = false;
            }
            else
            {
                Button newBtn = sender as Button;

                //int tag1 = int.Parse((newBtn).Tag.ToString());
                //int tag2 = int.Parse((prevBtn).Tag.ToString());

                if (newBtn.Tag.ToString() != prevBtn.Tag.ToString())
                {
                    newBtn.Text = newBtn.Tag.ToString();
                    await Task.Delay(1000);
                    newBtn.Text = "*";
                    prevBtn.Text = "*";
                    prevBtn.Enabled = true;
                }
                else
                {
                    //newBtn.Text = tag1.ToString();
                    // prevBtn.Text = tag2.ToString();
                    newBtn.Text = newBtn.Tag.ToString();
                    newBtn.Enabled = false;
                    prevBtn.Enabled = false;
                }
                prevBtn = null;

                //   а что, если мы два раза нажали на одну и ту же кнопку??
               
            }
            //int number = int.Parse((sender as Button).Tag.ToString());
            //MessageBox.Show("нажата кнопка " + number.ToString(), "Заголовок");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
