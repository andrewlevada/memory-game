using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		Random rnd = new Random();
		Button[,] buttons = null;
		Button PrevBTN = null;
        const int shmebulok = 4;
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
					nb.Text = "*";
                    nb.Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    nb.Location = new Point(i * 50, j * 50);
					nb.Size = new Size(50, 50);
                    nb.FlatStyle = FlatStyle.Flat;

					nb.Click += button1_Click;

                    nb.Parent = this;

					buttons[i, j] = nb;

                    nb.BringToFront();
				}

            pictureBox1.Size = new Size(shmebulok * 50, shmebulok * 50);
            pictureBox1.Location = new Point(0, 0);
        }

		private async void button1_Click(object sender, EventArgs e)
		{
			if (PrevBTN == null)
			{
				PrevBTN = sender as Button;
				PrevBTN.Text = PrevBTN.Tag.ToString();
                PrevBTN.Enabled = false;
			}
			else
			{
				Button newBTN = sender as Button;

				if (newBTN.Tag.ToString() == PrevBTN.Tag.ToString())
				{
					newBTN.Visible = false;
					PrevBTN.Visible = false;
                    CheckWin();
                }
				else
				{
                    newBTN.Text = newBTN.Tag.ToString();
                    PrevBTN.Enabled = true;
                    await Task.Delay(500);
                    newBTN.Text = "*";
                    PrevBTN.Text = "*";
				}

				PrevBTN = null;
			}
        }

        private void CheckWin()
        {
            foreach (Button e in buttons)
                if (e.Visible == true) return;

            DialogResult dialogResult = MessageBox.Show("You won!", "WIN", MessageBoxButtons.RetryCancel);
            if (dialogResult == DialogResult.Retry)
                TilesGen();
        }
    }
}
