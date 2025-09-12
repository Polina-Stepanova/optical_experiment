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
    public partial class Form1 : Form
    {
        Graphics g;Experiment EXPRMT;Form2 Obj_info;Form3 Lens_info;

        public Form1()
        {
            InitializeComponent(); button3.Text = "Скрыть линейку";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            g = panel1.CreateGraphics();
             EXPRMT = new Experiment();
            Obj_info = new Form2();
            Lens_info = new Form3();
            panel1.Refresh();
        }

        void SaveState()  
        { 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            saveFileDialog1.Title = "Сохранить данные эксперимента";
            saveFileDialog1.ShowDialog();
            BinaryFormatter bf = new BinaryFormatter();
            if (saveFileDialog1.FileName != "")
            {
                FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                bf.Serialize(fs, EXPRMT);
                fs.Close();
            }
        }
        void LoadState()  
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.Title = "Загрузить данные эксперимента";
            BinaryFormatter bf = new BinaryFormatter(); FileStream fs;
            if (openFileDialog1.ShowDialog() == DialogResult.OK & ((fs = (System.IO.FileStream)openFileDialog1.OpenFile()) != null))
            {
                EXPRMT = (Experiment)(bf.Deserialize(fs));
                fs.Close();
                if (EXPRMT.ray_visibility) button2.Text=  "Скрыть построение";
                else button2.Text = "Построить";
                if (EXPRMT.scaleline_visibility) button3.Text = "Скрыть линейку";
                else button3.Text = "Показать линейку";
            }

        }


        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveState();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadState(); Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Показать линейку")
            {
                button3.Text = "Скрыть линейку";EXPRMT.scaleline_visibility = true;
            }
            else
            {
                button3.Text = "Показать линейку"; EXPRMT.scaleline_visibility = false;
            }
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Построить")
            {
                button2.Text = "Скрыть построение";
                EXPRMT.ray_visibility = true;
                EXPRMT.ConstructResImage();
            }
            else {
                button2.Text = "Построить";
                EXPRMT.ray_visibility = false;
            }
            panel1.Refresh();

        }
        private void ObjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (Obj_info.Created == false)
            {
                Obj_info = new Form2();
                Obj_info.ChangesSaved += ChangesSent_obj;
                Obj_info.editable = true;
                Obj_info.EXPRMT_objedit = new ObjectImage(EXPRMT.OrigObj());

                Obj_info.label2.Text = "исходный объект";

                Obj_info.checkBox1.Enabled = true;
                Obj_info.numericUpDown2.Enabled = true;
                Obj_info.numericUpDown2.Value = (decimal)EXPRMT.OrigObj().ShowSize();
                Obj_info.checkBox1.Checked = EXPRMT.OrigObj().Infin();
                Obj_info.numericUpDown1.Enabled = !EXPRMT.OrigObj().Infin();
                if (!Obj_info.checkBox1.Checked & (EXPRMT.OrigObj().ShowX() >= 0))
                    Obj_info.numericUpDown1.Value = (decimal)EXPRMT.OrigObj().ShowX();

                Obj_info.radioButton1.Enabled = true;
                Obj_info.radioButton2.Enabled = true;
                Obj_info.radioButton1.Checked = EXPRMT.OrigObj().OrUp();
                Obj_info.radioButton2.Checked = !EXPRMT.OrigObj().OrUp();

                Obj_info.changesmade = false;

                Obj_info.ShowDialog();
            }
            else
            {
                Obj_info.Activate(); //was the editing window for the object already
                if (!Obj_info.editable) MessageBox.Show("Окно параметров объекта/изображений уже используется", "Предупреждение");
            }
        }


        public void ChangesSent_obj(object sender, ApplyChangesEventArgs e)
        {
            if (e.was_editable)
            {
                EXPRMT.EditOrigObj(e.EXPRMT_objedited);
                EXPRMT.ConstructResImage();
                panel1.Refresh();
            }
        }

        private void addLensToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (EXPRMT.N_Lenses() < 5)
            {
                if (Lens_info.Created == false)
                {
                    Lens_info = new Form3();
                    Lens_info.ChangesSaved += ChangesSent_lens_add;
                    Lens_info.newlens = true;
                    Lens_info.deletedlens = false;
                    Lens_info.EXPRMT_lensedit = new ConvergingLens(panel1.Width / 2, 10, (LensType)1);//"default"
                    Lens_info.ShowDialog();
                }
                else
                {
                    Obj_info.Activate();
                    MessageBox.Show("Окно параметров линзы уже используется", "Предупреждение");
                }
            }
        }


        public void ChangesSent_lens_add(object sender, ApplyChangesEventArgs2 e)
        {
            Lens temp;
            if (!e.was_del)
            {
                if (e.lensedit.ShowLtype() >= (LensType)1 & e.lensedit.ShowLtype() <= (LensType)3)
                    temp = new ConvergingLens(e.lensedit.ShowX(), e.lensedit.ShowFdist(), e.lensedit.ShowLtype());
                else temp = new DivergingLens(e.lensedit.ShowX(), e.lensedit.ShowFdist(), e.lensedit.ShowLtype());
                if (!e.was_new)
                {
                    EXPRMT.Remove_Lens(EXPRMT.lens_id_editing);
                }

                EXPRMT.Add_Lens(temp);
                EXPRMT.lens_id_editing = EXPRMT.Which_Lens_hit(temp.ShowX());
                EXPRMT.selected_lens_id = EXPRMT.lens_id_editing;
            }
            else
            {
                if (!e.was_new) EXPRMT.Remove_Lens(EXPRMT.lens_id_editing);
                EXPRMT.selected_lens_id = -1; EXPRMT.lens_id_editing = -1;
            }

            EXPRMT.ConstructResImage();
            panel1.Refresh();

        }

        private void editLensToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (EXPRMT.selected_lens_id >= 0)
            {

                if (Lens_info.Created == false)
                {
                    EXPRMT.lens_id_editing = EXPRMT.selected_lens_id;
                    Lens temp = EXPRMT.Remove_Lens(EXPRMT.lens_id_editing);
                    EXPRMT.Add_Lens(temp);
                    EXPRMT.selected_lens_id = EXPRMT.Which_Lens_hit(temp.ShowX());
                    EXPRMT.lens_id_editing = EXPRMT.selected_lens_id;

                    Lens_info = new Form3();
                    Lens_info.ChangesSaved += ChangesSent_lens_edit;
                    Lens_info.newlens = false;
                    Lens_info.deletedlens = false;
                    if (temp.ShowLtype() >= (LensType)1 & temp.ShowLtype() <= (LensType)3)
                        Lens_info.EXPRMT_lensedit = new ConvergingLens(temp.ShowX(), temp.ShowFdist(), temp.ShowLtype());
                    else Lens_info.EXPRMT_lensedit = new DivergingLens(temp.ShowX(), temp.ShowFdist(), temp.ShowLtype());
                    Lens_info.changesmade = false;

                    Lens_info.ShowDialog();
                }
                else
                {
                    Obj_info.Activate();
                    MessageBox.Show("Окно параметров линзы уже используется", "Предупреждение");
                }
            }
        }

        public void ChangesSent_lens_edit(object sender, ApplyChangesEventArgs2 e)
        {
            Lens temp;

            EXPRMT.Remove_Lens(EXPRMT.lens_id_editing);

            if (!e.was_del)
            {
                if (e.lensedit.ShowLtype() >= (LensType)1 & e.lensedit.ShowLtype() <= (LensType)3)
                    temp = new ConvergingLens(e.lensedit.ShowX(), e.lensedit.ShowFdist(), e.lensedit.ShowLtype());
                else temp = new DivergingLens(e.lensedit.ShowX(), e.lensedit.ShowFdist(), e.lensedit.ShowLtype());
                EXPRMT.Add_Lens(temp);
                EXPRMT.lens_id_editing = EXPRMT.Which_Lens_hit(temp.ShowX());
                EXPRMT.selected_lens_id = EXPRMT.lens_id_editing;
            }
            else
            {
                EXPRMT.selected_lens_id = -1; EXPRMT.lens_id_editing = -1;
            }

            EXPRMT.ConstructResImage();
            panel1.Refresh();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            sbyte selected_lens=EXPRMT.Which_Lens_hit(e.X);
            panel1.Refresh();
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (EXPRMT.selected_obj_image_id >= 0) {

                if (Obj_info.Created == false)
                {
                    Obj_info = new Form2();
                    Obj_info.ChangesSaved += ChangesSent_obj;
                    Obj_info.editable = false;
                    Obj_info.EXPRMT_objedit = new ObjectImage(EXPRMT.ViewImage(EXPRMT.selected_obj_image_id));

                    if (Obj_info.EXPRMT_objedit.ReOrIm()) Obj_info.label2.Text = "действительное";
                    else Obj_info.label2.Text = "мнимое";

                    Obj_info.radioButton1.Checked = Obj_info.EXPRMT_objedit.OrUp();
                    Obj_info.radioButton2.Checked = !Obj_info.EXPRMT_objedit.OrUp();
                    Obj_info.radioButton1.Enabled = false;
                    Obj_info.radioButton2.Enabled = false;

                    Obj_info.numericUpDown2.Value = (decimal)Obj_info.EXPRMT_objedit.ShowSize();
                    Obj_info.numericUpDown2.Enabled = false;

                    Obj_info.checkBox1.Checked = Obj_info.EXPRMT_objedit.Infin();
                    if (!Obj_info.checkBox1.Checked & (Obj_info.EXPRMT_objedit.ShowX() >= 0))
                        Obj_info.numericUpDown1.Value = (decimal)Obj_info.EXPRMT_objedit.ShowX();

                    Obj_info.numericUpDown1.Enabled = false;
                    Obj_info.checkBox1.Enabled = false;

                    Obj_info.ShowDialog();
                }
                else
                {
                    Obj_info.Activate(); //was the editing window for the object already
                    MessageBox.Show("Окно параметров изображений уже используется", "Предупреждение");
                }
            }
             
        }

        private void finalResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EXPRMT.ExperRez() != null)
            {

                if (EXPRMT.ExperRez().Infin())
                {
                    if(EXPRMT.ExperRez().ReOrIm())
                        MessageBox.Show("Лучи сошлись\nИзображение - точка в точке фокуса линзы", "Информация об изображении");
                    else if (EXPRMT.ExperRez().ShowSize()>=0)
                        MessageBox.Show("Лучи разошлись\nИзображение - мнимая точка в точке фокуса линзы", "Информация об изображении");
                    else MessageBox.Show("Лучи параллельны\nИзображения не существует", "Информация об изображении");
                }
                else if (Obj_info.Created == false)
                {
                    Obj_info = new Form2();
                    Obj_info.ChangesSaved += ChangesSent_obj;
                    Obj_info.editable = false;
                    Obj_info.EXPRMT_objedit = new ObjectImage(EXPRMT.ExperRez());

                    if (Obj_info.EXPRMT_objedit.ReOrIm()) Obj_info.label2.Text = "действительное";
                    else Obj_info.label2.Text = "мнимое";

                    Obj_info.radioButton1.Checked = Obj_info.EXPRMT_objedit.OrUp();
                    Obj_info.radioButton2.Checked = !Obj_info.EXPRMT_objedit.OrUp();
                    Obj_info.radioButton1.Enabled = false;
                    Obj_info.radioButton2.Enabled = false;

                    Obj_info.numericUpDown2.Value = (decimal)Obj_info.EXPRMT_objedit.ShowSize();
                    Obj_info.numericUpDown2.Enabled = false;

                    Obj_info.checkBox1.Checked = Obj_info.EXPRMT_objedit.Infin();
                    if (!Obj_info.checkBox1.Checked & (Obj_info.EXPRMT_objedit.ShowX() >= 0))
                        Obj_info.numericUpDown1.Value = (decimal)Obj_info.EXPRMT_objedit.ShowX();

                    Obj_info.numericUpDown1.Enabled = false;
                    Obj_info.checkBox1.Enabled = false;

                    Obj_info.ShowDialog();
                }
                else
                {
                    Obj_info.Activate(); //the editing window for the object was  already open
                    MessageBox.Show("Окно параметров изображений уже используется", "Предупреждение");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            sbyte selected_lens = EXPRMT.Which_Lens_hit(e.X);
            sbyte selected_img = EXPRMT.Which_Image_hit(e.X);
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            EXPRMT.Draw(g, panel1.Width, panel1.Height / 2);
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            panel1.Refresh();
        }

    }
}
