using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		Random rnd = new Random();
		PictureBox[,] buttons = null;
		PictureBox PrevBTN = null;
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

			buttons = new PictureBox[shmebulok, shmebulok];

			Size = new Size(shmebulok * 100 + 15, shmebulok * 100 + 50);

			for (int i = 0; i < shmebulok; i++)
				for (int j = 0; j < shmebulok; j++)
				{
					PictureBox nb = new PictureBox();
					nb.Tag = nums[i * shmebulok + j];

					//nb.Text = "*";
                    //nb.Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));

                    nb.Location = new Point(i * 100, j * 100);
					nb.Size = new Size(100, 100);

					nb.BackgroundImage = Properties.Resources.back;
					nb.BackgroundImageLayout = ImageLayout.Zoom;

					nb.Click += button1_Click;

                    nb.Parent = this;

					buttons[i, j] = nb;

                    nb.BringToFront();
				}

            pictureBox1.Size = new Size(shmebulok * 100, shmebulok * 100);
            pictureBox1.Location = new Point(0, 0);

			progressBar1.Maximum = shmebulok * shmebulok * 10;
			progressBar1.Width = Size.Width;
			progressBar1.Location = new Point(0, Size.Height - 50);
			progressBar1.Value = 0;
        }

		private async void button1_Click(object sender, EventArgs e)
		{
			if (PrevBTN == null)
			{
				PrevBTN = sender as PictureBox;
				PrevBTN.BackgroundImage = Properties.Resources.ResourceManager.GetObject("img" + PrevBTN.Tag.ToString()) as Image;
			}
			else
			{
				PictureBox newBTN = sender as PictureBox;

				if (newBTN.Tag.ToString() == PrevBTN.Tag.ToString())
				{
					newBTN.Visible = false;
					PrevBTN.Visible = false;
					progressBar1.Value += 20;
                    CheckWin();
                }
				else
				{
					newBTN.BackgroundImage = Properties.Resources.ResourceManager.GetObject("img" + newBTN.Tag.ToString()) as Image;
					await Task.Delay(500);
					newBTN.BackgroundImage = Properties.Resources.back;
					PrevBTN.BackgroundImage = Properties.Resources.back;
				}

				PrevBTN = null;
			}
        }

        private void CheckWin()
        {
            foreach (PictureBox e in buttons)
                if (e.Visible == true) return;

            DialogResult dialogResult = MessageBox.Show("You won!", "WIN", MessageBoxButtons.RetryCancel);
            if (dialogResult == DialogResult.Retry)
                TilesGen();
        }
    }
}
