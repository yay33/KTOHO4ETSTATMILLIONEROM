using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTOHO4ETSTATMILLIONEROM
{
    public partial class Stats : Form
    {
        public Stats()
        {
            InitializeComponent();
            Startup();
        }
        private async void Startup()
        {
            Random rnd = new Random();
            var end = DateTime.Now.AddSeconds(15);

            int[] percentages = new int[4];
            int remainingPercentage = 100;
            for (int i = 0; i < 4; i++)
            {
                percentages[i] = (i == 3) ? remainingPercentage : rnd.Next(1, remainingPercentage - 2);
                remainingPercentage -= percentages[i];
            }

            while (DateTime.Now <= end)
            {
                label6.Text = (end - DateTime.Now).ToString("ss");
                await Task.Delay(100);
            }

            progressBar1.Value = percentages[0];
            progressBar2.Value = percentages[1];
            progressBar3.Value = percentages[2];
            progressBar4.Value = percentages[3];
        }
    }
}
