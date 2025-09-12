using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OpticalExperiment
{
    public partial class Form3 : Form
    {
        public event ApplyChangesEventHandler2 ChangesSaved;
        public Lens EXPRMT_lensedit;
        public bool newlens,deletedlens;
        public bool changesmade;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            changesmade = false;
            if (EXPRMT_lensedit != null)
            {
                comboBox1.SelectedIndex = ((int)EXPRMT_lensedit.ShowLtype()) -1;
                numericUpDown1.Value = (decimal)EXPRMT_lensedit.ShowX();
                numericUpDown2.Value = (decimal)EXPRMT_lensedit.ShowFdist();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deletedlens = true;
            ChangesSaved(this, new ApplyChangesEventArgs2(EXPRMT_lensedit, newlens, deletedlens));
            changesmade = false; Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesmade) {
                if (MessageBox.Show("Выйти без сохранения изменений?", "Предупреждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
                { e.Cancel = true; return; }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changesmade = false; Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesmade = true;
            if(comboBox1.SelectedIndex>=0& comboBox1.SelectedIndex <= 2)
            {
                EXPRMT_lensedit = new ConvergingLens(EXPRMT_lensedit.ShowX(), EXPRMT_lensedit.ShowFdist(), (LensType)(comboBox1.SelectedIndex + 1));
            }
            else
            {
                EXPRMT_lensedit = new DivergingLens(EXPRMT_lensedit.ShowX(), EXPRMT_lensedit.ShowFdist(), (LensType)(comboBox1.SelectedIndex + 1));
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            changesmade = true;
            if (EXPRMT_lensedit.ShowLtype() >= (LensType)1 & EXPRMT_lensedit.ShowLtype() <= (LensType)3)
            {
                EXPRMT_lensedit = new ConvergingLens((float)numericUpDown1.Value, EXPRMT_lensedit.ShowFdist(), EXPRMT_lensedit.ShowLtype());
            }
            else
            {
                EXPRMT_lensedit = new DivergingLens((float)numericUpDown1.Value, EXPRMT_lensedit.ShowFdist(), EXPRMT_lensedit.ShowLtype());
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            changesmade = true;
            if (EXPRMT_lensedit.ShowLtype() >= (LensType)1 & EXPRMT_lensedit.ShowLtype() <= (LensType)3)
            {
                EXPRMT_lensedit = new ConvergingLens(EXPRMT_lensedit.ShowX(), (float)numericUpDown2.Value, EXPRMT_lensedit.ShowLtype());
            }
            else
            {
                EXPRMT_lensedit = new DivergingLens(EXPRMT_lensedit.ShowX(), (float)numericUpDown2.Value, EXPRMT_lensedit.ShowLtype());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {                 
            ChangesSaved(this, new ApplyChangesEventArgs2(EXPRMT_lensedit, newlens, deletedlens)); changesmade = false;
            if (newlens) {newlens = false;}
        }

    }
    public class ApplyChangesEventArgs2 : EventArgs
    {
        public Lens lensedit; public bool was_new,was_del;
        public ApplyChangesEventArgs2(Lens EXPRMT_lensedit, bool newlens,bool deletedlens)
        {
            was_new = newlens; was_del = deletedlens;
            if (!was_del)
            {
                if (EXPRMT_lensedit.ShowLtype() >= (LensType)1 & EXPRMT_lensedit.ShowLtype() <= (LensType)3)
                    lensedit = new ConvergingLens(EXPRMT_lensedit.ShowX(), EXPRMT_lensedit.ShowFdist(), EXPRMT_lensedit.ShowLtype());
                else lensedit = new DivergingLens(EXPRMT_lensedit.ShowX(), EXPRMT_lensedit.ShowFdist(), EXPRMT_lensedit.ShowLtype());
            }
        }
    }

    public delegate void ApplyChangesEventHandler2(object sender, ApplyChangesEventArgs2 e);
}
