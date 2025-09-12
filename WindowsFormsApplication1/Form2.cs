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
    public partial class Form2 : Form
    {
        public event ApplyChangesEventHandler ChangesSaved;
        public ObjectImage EXPRMT_objedit;
        public bool editable;
        public bool  changesmade;

        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            changesmade = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangesSaved(this, new ApplyChangesEventArgs(EXPRMT_objedit,editable)); changesmade = false;

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editable&changesmade)
            {
                if (MessageBox.Show("Выйти без сохранения изменений?", "Предупреждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
                { e.Cancel = true; return; }

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//up
        {
            if (radioButton1.Checked == true)
            { radioButton2.Checked = false;
                changesmade = true;
                EXPRMT_objedit = new ObjectImage(EXPRMT_objedit.ShowX(), EXPRMT_objedit.Infin(), EXPRMT_objedit.ShowSize(), true, EXPRMT_objedit.ObOrIm(), EXPRMT_objedit.ReOrIm());
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//down
        {
            if (radioButton2.Checked == true)
            { radioButton1.Checked = false;
                changesmade = true;
                EXPRMT_objedit = new ObjectImage(EXPRMT_objedit.ShowX(), EXPRMT_objedit.Infin(), EXPRMT_objedit.ShowSize(), false, EXPRMT_objedit.ObOrIm(), EXPRMT_objedit.ReOrIm());
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            { changesmade = true;
                EXPRMT_objedit = new ObjectImage((float)numericUpDown1.Value, EXPRMT_objedit.Infin(), EXPRMT_objedit.ShowSize(), EXPRMT_objedit.OrUp(), EXPRMT_objedit.ObOrIm(), EXPRMT_objedit.ReOrIm());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            changesmade = true;
            if (checkBox1.Checked == true)
            { numericUpDown1.Enabled = false;
                EXPRMT_objedit = new ObjectImage(EXPRMT_objedit.ShowX(),true, EXPRMT_objedit.ShowSize(), EXPRMT_objedit.OrUp(), EXPRMT_objedit.ObOrIm(), EXPRMT_objedit.ReOrIm());
            }
            else if (checkBox1.Checked == false)
            { numericUpDown1.Enabled = true;
                if (EXPRMT_objedit.ShowX() >= 0) numericUpDown1.Value = (decimal)EXPRMT_objedit.ShowX();
                EXPRMT_objedit = new ObjectImage(EXPRMT_objedit.ShowX(), false, EXPRMT_objedit.ShowSize(), EXPRMT_objedit.OrUp(), EXPRMT_objedit.ObOrIm(), EXPRMT_objedit.ReOrIm());
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            changesmade = true;
            EXPRMT_objedit = new ObjectImage(EXPRMT_objedit.ShowX(), EXPRMT_objedit.Infin(), (float)numericUpDown2.Value, EXPRMT_objedit.OrUp(), EXPRMT_objedit.ObOrIm(), EXPRMT_objedit.ReOrIm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class ApplyChangesEventArgs : EventArgs
    {
        public ObjectImage EXPRMT_objedited;public bool was_editable;
        public ApplyChangesEventArgs(ObjectImage EXPRMT_objedit,bool editable)
        {
            was_editable = editable;
            EXPRMT_objedited = new ObjectImage(EXPRMT_objedit);
        }

    }

    public delegate void ApplyChangesEventHandler(object sender, ApplyChangesEventArgs e);
}
