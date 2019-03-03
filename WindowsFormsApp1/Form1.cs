using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		Random rnd = new Random();
		Button[,] buttons = null;
		Button PrevBTN = null;
		const int shmebulok = 10;
		public Form1()
		{
			InitializeComponent();
			TilesGen();
		}

		public void TilesGen()
		{
			int[] nums = new int[shmebulok * shmebulok];

			for (int i = 0; i < shmebulok * shmebulok; i++)
				nums[i] = i / 2 + 1;

			for (int i = 0; i < 1000; i++)
			{
				int p1 = rnd.Next(shmebulok * shmebulok);
				int p2 = rnd.Next(shmebulok * shmebulok);

				int tmp = nums[p1];

				nums[p1] = nums[p2];
				nums[p2] = tmp;
			}

			buttons = new Button[shmebulok, shmebulok];

			Size = new Size(shmebulok * 50 + 100, shmebulok * 50 + 100);

			for (int i = 0; i < shmebulok; i++)
				for (int j = 0; j < shmebulok; j++)
				{
					Button nb = new Button();
					nb.Tag = nums[i * shmebulok + j];
					nb.Text = nums[i * shmebulok + j].ToString();
					nb.Location = new Point(i * 50, j * 50);
					nb.Size = new Size(50, 50);

					nb.Click += button1_Click;

					nb.Parent = this;

					buttons[i, j] = nb;
				}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (PrevBTN == null)
			{
				PrevBTN = sender as Button;
				PrevBTN.Text = PrevBTN.Tag.ToString();
			}
			else
			{
				Button newBTN = sender as Button;

				if (newBTN.Tag == PrevBTN.Tag)
				{
					newBTN.Enabled = false;
					PrevBTN.Enabled = true;
				}
				else
				{
					PrevBTN.Text = "*";
				}
				PrevBTN = null;
			}

			/*
			int number = int.Parse((sender as Button).Tag.ToString());
			MessageBox.Show("BUTTON " + number + " PRESSED", "TITILE");
			*/
		}
	}
}
