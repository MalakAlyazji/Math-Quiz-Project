using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathGameProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        enum enqlevel { easy=1,hard=2,med=3,mix=4};
        enum enoperation { sum=1,sub=2,mult=3,divv=4,mixx=5};
        enum engender { female,male};
        struct stround
        {
            public enqlevel roundlevel;
            public enoperation roundop;
            public int nrounds;
            public engender roundgender;
            public int rightanswer;
        }
        stround round;
        struct stquiz
        {
            public int n1, n2;
            public int countround, counttime;
            public short nrightanswer,nwronganswer;
            public int correctanswer;
            public int timer;
        }
        stquiz quiz;
        string operation (enoperation op)
        {
            switch (op)
            {
                case enoperation.sum:
                    return "+";
                    break;
                case enoperation.sub:
                    return "-";
                    break;
                case enoperation.mult:
                    return "*";
                    break;
                case enoperation.divv:
                    return "/";
                    break;
                default:
                    return "+";

            }
        }
        int randomnum(int from ,int to)
        {
            Random rnd = new Random();
            return rnd.Next(from ,to);
        }
        enqlevel fillqlevel(int num)
        {
            if (num == 4)
                num = randomnum(1, 3);
            switch (num)
            {
                case 1:
                    return enqlevel.easy;
                    break;
                case 2:
                    return enqlevel.med;
                    break;
                default:
                    return enqlevel.hard;
            }
            
        }
        enoperation filloperation(int num)
        {
            if (num == 5)
                num = randomnum(1,4 );
            switch (num)
            {
                case 1:
                    return enoperation.sum;
                    break;
                case 2:
                    return enoperation.sub;
                    break;
                case 3:
                    return enoperation.mult;
                    break;
                default:
                    return enoperation.divv;
            }
        }
        void changegenderphoto()
        {
            if (rbfemale.Checked)
            {
                pbgender.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\qg.jpg");
            }
            else
            {
                pbgender.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\boy.jpg");
               
            }
        }
        void  ValidatingTrackBarError(TrackBar tb , string message, CancelEventArgs e)
        {
            if (tb.Value == 0)
            {
                e.Cancel = true;
                tb.Focus();
                errorProvider1.SetError(tb,message);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tb, "");
            }
        }
        void ValidatingRadioButtonError(RadioButton rb, string message, CancelEventArgs e)
        {
            if (!rb.Checked)
            {
                e.Cancel = true;
                rb.Focus();
                errorProvider1.SetError(rb, message);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(rb, "");

            }
        }
        void ValidatingprogressbarError(ProgressBar rb, string message, CancelEventArgs e)
        {
            if (rb.Value==0)
            {
                e.Cancel = true;
                rb.Focus();
                errorProvider1.SetError(rb, message);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(rb, "");
            }
        }
        void ValidatingnumericError(NumericUpDown rb, string message, CancelEventArgs e)
        {
            if (rb.Value == 0)
            {
                e.Cancel = true;
                rb.Focus();
                errorProvider1.SetError(rb, message);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(rb, "");
            }
        }
        private void trackBar1_Validating(object sender, CancelEventArgs e)
        {
            ValidatingTrackBarError(trackBar1, "You should fill trackbar question level", e); 
        }
        private void rbfemale_Validating_1(object sender, CancelEventArgs e)
        {
            ValidatingRadioButtonError(rbfemale, "You should fill radio button question level", e);
        }
        private void rbmale_Validating(object sender, CancelEventArgs e)
        {
            ValidatingRadioButtonError(rbmale, "You should fill radio button question level", e);
        }
        private void trackBar2_Validating_1(object sender, CancelEventArgs e)
        {
            ValidatingTrackBarError(trackBar2, "You should fill trackbar operation level", e);
        }    
        int calculatetrackbar(TrackBar tb)
        {
            switch (tb.Value)
            {
                case 0:
                    return 0;
                    break;
                 default:
                    return 25;

            }
        }
        int calculategender()
        {
            if (rbfemale.Checked)
                return 25;
            else if (rbmale.Checked)
                return 25;
            else
                return 0;
        }
        int calculateroundpb()
        {
            switch (numericUpDown1.Value)
            {
                case 0:
                    return 0;
                default:
                    return 25;
            }
                

        }
        int calculateprogressbar()
        {
            return calculatetrackbar(trackBar1) + calculatetrackbar(trackBar2) + calculategender() + calculateroundpb();
        }
        void updateprogressbar()
        {
            progressBar1.Value = calculateprogressbar();
            progressBar1.Refresh();
            if (progressBar1.Value == 100)
            {
                btstart.Enabled = true;
            }
            else
            {
                btstart.Enabled = false;
            }
        }
        void finishtimer()
        {
            if(MessageBox.Show("Time is out","Wrong Answer", MessageBoxButtons.OK) == DialogResult.OK)
            {
               // quiz.nwronganswer++;
                MessageBox.Show("the right answer is "+quiz.correctanswer.ToString(), "the answer", MessageBoxButtons.OK);
                pbmode.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\fail.jpg");
                btchechresult.Enabled = false;
                btnext.Enabled = true;
            }
            
         }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            quiz.timer++;
            lbtimer.Text= quiz.timer.ToString();
            if (quiz.timer == 30)
            {
                timer1.Enabled = false;
                finishtimer();
                return;
            }
            if (quiz.timer >= 20)
            {
                lbtimer.Refresh();
                lbtimer.BackColor = Color.Red;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            updateprogressbar();
             round.roundlevel=  fillqlevel(trackBar1.Value);
        }
        void updategender()
        {
            if (rbfemale.Checked){
                round.roundgender = engender.female;
            }
            else
            {
                round.roundgender = engender.male;
            }
        }
        private void rbfemale_CheckedChanged(object sender, EventArgs e)
        {
            updateprogressbar();
            updategender();
        }
        private void rbmale_CheckedChanged(object sender, EventArgs e)
        {
            updateprogressbar();
            updategender();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            updateprogressbar();
            round.roundop=filloperation(trackBar2.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            updateprogressbar();
            round.nrounds =Convert.ToInt32( numericUpDown1.Value);
        }
        void calculateround()
        {
            quiz.countround++;
            lbroundnum.Text = quiz.countround.ToString();
        }
        int simplecalculate(int n1,int n2,enoperation op)
        {
            switch (op)
            {
                case enoperation.sum:
                    return n1 + n2;
                        break;
                case enoperation.sub:
                    return n1 - n2;
                    break;
                case enoperation.mult:
                    return n1 * n2;
                    break;
                case enoperation.divv:
                    return n1 / n2;
                    break;
                default:
                    return n1 + n2;
            }
        }
        void fillrandom(int from,int to,enoperation op)
        {
            Random rnd = new Random();
            quiz.n1=rnd.Next(from, to);
            quiz.n2= rnd.Next(from, to);
            quiz.correctanswer= simplecalculate(quiz.n1, quiz.n2, op);
        }
        void genaratequestion()
        {
            switch (round.roundlevel)
            {
                case enqlevel.easy:
                    fillrandom(1,10,round.roundop);
                    break;
                case enqlevel.med:
                    fillrandom(10, 50, round.roundop);
                    break;
                case enqlevel.hard:
                    fillrandom(50, 100, round.roundop);
                    break;

            }
        }
        void fillquiz()
        {
            lbn1.Text = quiz.n1.ToString();
            lbop.Text = operation(round.roundop);
            lbn2.Text = quiz.n2.ToString();
        }
        bool checkresult()
        {
            btnext.Enabled = true;
            if (txtresult.Text == quiz.correctanswer.ToString())
            {
                quiz.nrightanswer++;
                return true;
            }
            else
            {
                quiz.nwronganswer++;
                return false;
            }
        }
        void settimer()
        {
            lbtimer.Enabled = true;
            timer1.Start();
        }
        void help()
        {
            if (bthelp.Tag.ToString() == "?")
            {
                if (MessageBox.Show("Are you sure you want to use your help?","Help",MessageBoxButtons.OK,MessageBoxIcon.Question) == DialogResult.OK)
                {
                    bthelp.Tag = "0";
                    notifyIcon1.Icon = SystemIcons.Application;
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.BalloonTipTitle = "Help For quiz";
                    notifyIcon1.BalloonTipText = quiz.n1.ToString() + operation(round.roundop) + quiz.n2.ToString() + "=" + quiz.correctanswer.ToString();
                    notifyIcon1.ShowBalloonTip(10);
                }
            }
            else
            {
                MessageBox.Show("No more hlp for you :(", "zero help", MessageBoxButtons.OK);
            }
        }
        void startquestion()
        {
            btstartquiz.Enabled = false;
            btback.Enabled = false;
            btnext.Enabled = false;
            calculateround();
            changegenderphoto();
            genaratequestion();
            fillquiz();
            checkresult();
            settimer();
        }
        private void btstartquiz_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("Are you sure you want to start this question? ", "Start?", MessageBoxButtons.OK) == DialogResult.OK)
            {
                tabControl1.SelectedTab = tabPage2;
                startquestion();
                groupBox2.Enabled = true;
            }
            //timer1.Enabled = true;
            //changegenderphoto();
            

        }
        private void btstart_Click(object sender, EventArgs e)
        {
            tabPage1.Enabled = false;
            tabControl1.SelectedTab = tabPage2;
            lboutof.Text = numericUpDown1.Value.ToString();
            groupBox2.Enabled = false;
            btnext.Enabled = false;
        }
        private void btback_Click(object sender, EventArgs e)
        {
            tabPage1.Enabled = true;
            tabControl1.SelectedTab = tabPage1;
        }

        private void btchechresult_Click(object sender, EventArgs e)
        {
            if (checkresult())
            {
                switch (round.roundgender)
                {
                    case engender.female:
                        pbmode.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\rightgirl .jpg");
                        break;
                    case engender.male:
                        pbmode.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\rigthboy.jpg");
                        break;

                }

            }
            else
            {
                switch (round.roundgender)
                {
                    case engender.female:
                        pbmode.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\wronggirl.jpg");
                        break;
                    case engender.male:
                        pbmode.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\wrong boy.jpg");
                        break;

                }
            }
                btchechresult.Enabled = false;
            timer1.Enabled = false;
            btnext.Enabled = true;
            bthelp.Enabled = false;
        }

        private void bthelp_Click(object sender, EventArgs e)
        {
            help();
        }
        void nextquestion()
        {
            if (quiz.countround == numericUpDown1.Value)
            {
                btnext.Enabled = false;
                btback.Enabled = true;
                MessageBox.Show("Quiz is Done", "Done",MessageBoxButtons.OK);
                btresult.Enabled = true;
                return;
            }
            calculateround();
            if (quiz.countround <= numericUpDown1.Value)
            {
                quiz.timer =  Convert.ToInt32(quiz.timer) - Convert.ToInt32(quiz.timer);
                txtresult.Text = "";
                genaratequestion();
                fillquiz();
                checkresult();
                settimer();
            }
        }
        private void btnext_Click(object sender, EventArgs e)
        {
            btchechresult.Enabled = true;
            nextquestion();
            
        }

        private void btback_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            tabPage1.Enabled = true;
        }
        void showresult()
        {
            lbgen.Text = round.roundgender.ToString();
            lbqlev.Text = round.roundlevel.ToString();
            lbopop.Text = round.roundop.ToString();
            lbroundd.Text = round.nrounds.ToString();
            lbnr.Text = quiz.nrightanswer.ToString();
            lbnw.Text = (round.nrounds-quiz.nrightanswer).ToString();
        }
        private void btresult_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            tabPage2.Enabled = false;
            showresult();
        }
        public string ispass()
        {
            return (quiz.nrightanswer >= quiz.nwronganswer) ? "Pass" : "Fail";
           
        }
        void fillviewlist()
        {
            string g = round.roundgender.ToString();
            string q = round.roundlevel.ToString();
            string op = round.roundop.ToString();
            string n = round.nrounds.ToString();
            string r = quiz.nrightanswer.ToString();
            string w = (round.nrounds- quiz.nrightanswer).ToString();
            string ispas=ispass();
            ListViewItem item = new ListViewItem(g);
            item.SubItems.Add(q);
            item.SubItems.Add(op);
            item.SubItems.Add(n);
            item.SubItems.Add(r);
            item.SubItems.Add(w);
            item.SubItems.Add(ispas);
            listView1.Items.Add(item);

        }
        private void btnextlast_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            fillviewlist();
        }
        void playagain()
        {
            tabControl1.SelectedTab = tabPage1;
            tabPage1.Enabled = true;
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            numericUpDown1.Value = 0;
            rbmale.Checked = false;
            rbfemale.Checked = false;
            progressBar1.Value = 0;

            bthelp.Tag = "?";
            tabPage2.Enabled = true;
            lbroundnum.Text = "0";
            lboutof.Text = "0";
            pbmode.Image = Image.FromFile(@"C:\Users\hp\Downloads\questionmark.jpg");
            pbgender.Image = Image.FromFile(@"C:\Users\hp\Downloads\questionmark.jpg");
            lbtimer.Text = "0";
            groupBox2.Enabled = false;
            btstartquiz.Enabled = true;
            btback.Enabled = true;
            btnext.Enabled = false;
            btresult.Enabled = false;
            lbn1.Text = "00";
            lbn2.Text = "00";
            lbop.Text = "+";
            quiz.nrightanswer = 0;
            quiz.nrightanswer = 0;
            quiz.nwronganswer = 0;
            quiz.timer = 0;
            quiz.countround = 0;
            txtresult.Text = " ";
            nextquestion();


            lbgen.Text = "-";
            lbqlev.Text = "-";
            lbopop.Text = "-";
            lbroundd.Text = "-";
            lbnr.Text = "-";
            lbnw.Text = "-";
        }
        private void btplay_Click(object sender, EventArgs e)
        {
            playagain();
        }

        private void txtresult_TextChanged(object sender, EventArgs e)
        {
            double i = 0;
            if(double.TryParse(txtresult.Text,out i))
            {
                errorProvider2.SetError(txtresult, "");
            }
            else
            {
                errorProvider2.SetError(txtresult, "Only numbers!");
            }
        }
    }
}
