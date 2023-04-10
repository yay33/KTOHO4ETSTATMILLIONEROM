using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTOHO4ETSTATMILLIONEROM
{
    public partial class MainWinodw : Form
    {

        static bool CheatModeStatus = true;

        List<QuestionModel> MainList = new List<QuestionModel>();
        public MainWinodw()
        {
            InitializeComponent();
            listBox1.ForeColor = Color.White;
            listBox1.BackColor = Color.MidnightBlue;
            labelTimer.Visible = false;
            label2.Visible = false;
            labelLose.Visible = false;
            GetListQuestion();
            NextQuestion();
        }
        public void GetListQuestion()
        {
            string[] quest = File.ReadAllLines($"{AppContext.BaseDirectory}quest.txt");
            foreach (string item in quest)
            {
                string[] str = item.Split('\t');
                QuestionModel dataquest = new QuestionModel
                {
                    QuestionBody = str[0],
                    Answer1 = str[1],
                    Answer2 = str[2],
                    Answer3 = str[3],
                    Answer4 = str[4],
                    CorrectAnswer = Convert.ToInt32(str[5]),
                    Level = Convert.ToInt32(str[6]),
                };
                MainList.Add(dataquest);
            }
        }

        static int CorrectAnswer = 0;
        private void ChoseAnswer(int buttonnubmer)
        {
            if (CheatModeStatus)
            {
                buttonAnswer1.BackColor = Color.MidnightBlue;
                buttonAnswer2.BackColor = Color.MidnightBlue;
                buttonAnswer3.BackColor = Color.MidnightBlue;
                buttonAnswer4.BackColor = Color.MidnightBlue;
            }
            if (buttonnubmer == CorrectAnswer)
            {
                CurrentLevel++;
                if (CurrentLevel == 15)
                    Victory();
                else
                    NextQuestion();
            }
            else
            {
                if (Attepmt2)
                {
                    MessageBox.Show("Ответ был неверный. Вас спас второй шанс");
                    Attepmt2 = false;
                    return;
                }
                Lose(buttonnubmer);
            }
        }
        private void Lose(int chosebut)
        {
            if (CorrectAnswer == 1)
                buttonAnswer1.BackColor = Color.Green;
            if (CorrectAnswer == 2)
                buttonAnswer2.BackColor = Color.Green;
            if (CorrectAnswer == 3)
                buttonAnswer3.BackColor = Color.Green;
            if (CorrectAnswer == 4)
                buttonAnswer4.BackColor = Color.Green;
            if (chosebut == 1)
                buttonAnswer1.BackColor = Color.Red;
            if (chosebut == 2)
                buttonAnswer2.BackColor = Color.Red;
            if (chosebut == 3)
                buttonAnswer3.BackColor = Color.Red;
            if (chosebut == 4)
                buttonAnswer4.BackColor = Color.Red;
            buttonAnswer1.Enabled = false;
            buttonAnswer2.Enabled = false;
            buttonAnswer3.Enabled = false;
            buttonAnswer4.Enabled = false;
            button5050.Enabled = false;
            button2ndAttempt.Enabled = false;
            buttonFriend.Enabled = false;
            buttonHelpAudience.Enabled = false;
            buttonSwap.Enabled = false;
            labelLose.Visible = true;
        }
        private void Victory()
        {
            listBox1.SelectedIndex = 0;
            string win = "Победа";
            buttonAnswer1.Enabled = false;
            buttonAnswer2.Enabled = false;
            buttonAnswer3.Enabled = false;
            buttonAnswer4.Enabled = false;
            buttonAnswer1.Text = win;
            buttonAnswer2.Text = win;
            buttonAnswer3.Text = win;
            buttonAnswer4.Text = win;
            label1.Text = win;
        }
        private void CheatMode()
        {
            if (CorrectAnswer == 1)
                buttonAnswer1.BackColor = Color.Green;
            if (CorrectAnswer == 2)
                buttonAnswer2.BackColor = Color.Green;
            if (CorrectAnswer == 3)
                buttonAnswer3.BackColor = Color.Green;
            if (CorrectAnswer == 4)
                buttonAnswer4.BackColor = Color.Green;
        }
        static int CurrentLevel = 1;
        private void NextQuestion()
        {
            buttonAnswer1.Enabled = true;
            buttonAnswer2.Enabled = true;
            buttonAnswer3.Enabled = true;
            buttonAnswer4.Enabled = true;
            listBox1.SelectedIndex = 15 - CurrentLevel;
            var list = MainList.Where(x => x.Level == CurrentLevel).ToList();
            Random rnd = new Random();
            int numquest = rnd.Next(list.Count);
            var dataquest = list[numquest];
            label1.Text = dataquest.QuestionBody;
            buttonAnswer1.Text = dataquest.Answer1;
            buttonAnswer2.Text = dataquest.Answer2;
            buttonAnswer3.Text = dataquest.Answer3;
            buttonAnswer4.Text = dataquest.Answer4;
            CorrectAnswer = dataquest.CorrectAnswer;
            if (CheatModeStatus)
                CheatMode();
        }

        
        private bool AffirmativeAnswer()
        {
            return true;
        }
        private void buttonAnswer1_Click(object sender, EventArgs e)
        {
            buttonAnswer1.Enabled = false;
            if (AffirmativeAnswer())
                ChoseAnswer(1);
        }

        private void buttonAnswer2_Click(object sender, EventArgs e)
        {
            buttonAnswer2.Enabled = false;
            if (AffirmativeAnswer())
                ChoseAnswer(2);
        }

        private void buttonAnswer3_Click(object sender, EventArgs e)
        {
            buttonAnswer3.Enabled = false;
            if (AffirmativeAnswer())
                ChoseAnswer(3);
        }

        private void buttonAnswer4_Click(object sender, EventArgs e)
        {
            buttonAnswer4.Enabled = false;
            if (AffirmativeAnswer())
                ChoseAnswer(4);
        }

        private void buttonHelpAudience_Click(object sender, EventArgs e)
        {
            buttonHelpAudience.Enabled = false;
            Stats newform = new Stats();
            newform.Show();
        }
        private void DisableAnswer(int number)
        {
            if (number == 1)
                buttonAnswer1.Enabled = false;
            if (number == 2)
                buttonAnswer2.Enabled = false;
            if (number == 3)
                buttonAnswer3.Enabled = false;
            if (number == 4)
                buttonAnswer4.Enabled = false;
        }
        private void button5050_Click(object sender, EventArgs e)
        {
            button5050.Enabled = false;
            Random rnd = new Random();
            int disable1 = 0;
            int countdis = 0;
            while (countdis < 2)
            {
                int disable = rnd.Next(1, 4);
                if (disable != CorrectAnswer)
                {
                    if (disable1 != disable)
                    {
                        DisableAnswer(disable);
                        countdis++;
                        disable1 = disable;
                    }
                }
            }
        }

        private async void buttonFriend_Click(object sender, EventArgs e)
        {
            buttonFriend.Enabled = false;
            DateTime end = DateTime.Now.AddSeconds(30);
            labelTimer.Visible = true;
            label2.Visible = true;
            while (DateTime.Now <= end)
            {
                labelTimer.Text = (end - DateTime.Now).ToString("ss");
                await Task.Delay(100);
            }
            labelTimer.Visible = false;
            label2.Visible = false;
            MessageBox.Show("Время вышло");
        }

        private void button2ndAttempt_Click(object sender, EventArgs e)
        {
            Attepmt2 = true;
            button2ndAttempt.Enabled = false;
        }
        static bool Attepmt2 = false;
        private void buttonSwap_Click(object sender, EventArgs e)
        {
            buttonAnswer1.Enabled = true;
            buttonAnswer2.Enabled = true;
            buttonAnswer3.Enabled = true;
            buttonAnswer4.Enabled = true;
            buttonAnswer1.BackColor = Color.MidnightBlue;
            buttonAnswer2.BackColor = Color.MidnightBlue;
            buttonAnswer3.BackColor = Color.MidnightBlue;
            buttonAnswer4.BackColor = Color.MidnightBlue;
            var list = MainList.Where(x => x.Level == CurrentLevel).ToList();
            Random rnd = new Random();
            int numquest = rnd.Next(list.Count);
            var dataquest = list[numquest];
            label1.Text = dataquest.QuestionBody;
            buttonAnswer1.Text = dataquest.Answer1;
            buttonAnswer2.Text = dataquest.Answer2;
            buttonAnswer3.Text = dataquest.Answer3;
            buttonAnswer4.Text = dataquest.Answer4;
            CorrectAnswer = dataquest.CorrectAnswer;
            if (CheatModeStatus)
                CheatMode();
            buttonSwap.Enabled = false;
        }
    }
}
