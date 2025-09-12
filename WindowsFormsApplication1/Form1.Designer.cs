namespace OpticalExperiment
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.объектToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьЛинзуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьЛинзуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сведенияОбИзображенииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбранномToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.финальномРезультатеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(427, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(204, 284);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "Скрыть построение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(12, 284);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 34);
            this.button3.TabIndex = 2;
            this.button3.Text = "Показать линейку";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(529, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.LoadToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton1.Text = "Файл";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // LoadToolStripMenuItem
            // 
            this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            this.LoadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoadToolStripMenuItem.Text = "Загрузить";
            this.LoadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.объектToolStripMenuItem1,
            this.добавитьЛинзуToolStripMenuItem1,
            this.изменитьЛинзуToolStripMenuItem,
            this.сведенияОбИзображенииToolStripMenuItem});
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(77, 22);
            this.toolStripButton2.Text = "Параметры";
            // 
            // объектToolStripMenuItem1
            // 
            this.объектToolStripMenuItem1.Name = "объектToolStripMenuItem1";
            this.объектToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.объектToolStripMenuItem1.Text = "Объект";
            this.объектToolStripMenuItem1.Click += new System.EventHandler(this.ObjectToolStripMenuItem1_Click);
            // 
            // добавитьЛинзуToolStripMenuItem1
            // 
            this.добавитьЛинзуToolStripMenuItem1.Name = "добавитьЛинзуToolStripMenuItem1";
            this.добавитьЛинзуToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.добавитьЛинзуToolStripMenuItem1.Text = "Добавить линзу";
            this.добавитьЛинзуToolStripMenuItem1.Click += new System.EventHandler(this.addLensToolStripMenuItem1_Click);
            // 
            // изменитьЛинзуToolStripMenuItem
            // 
            this.изменитьЛинзуToolStripMenuItem.Name = "изменитьЛинзуToolStripMenuItem";
            this.изменитьЛинзуToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.изменитьЛинзуToolStripMenuItem.Text = "Изменить линзу";
            this.изменитьЛинзуToolStripMenuItem.Click += new System.EventHandler(this.editLensToolStripMenuItem_Click);
            // 
            // сведенияОбИзображенииToolStripMenuItem
            // 
            this.сведенияОбИзображенииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбранномToolStripMenuItem,
            this.финальномРезультатеToolStripMenuItem});
            this.сведенияОбИзображенииToolStripMenuItem.Name = "сведенияОбИзображенииToolStripMenuItem";
            this.сведенияОбИзображенииToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.сведенияОбИзображенииToolStripMenuItem.Text = "Сведения об изображении";
            // 
            // выбранномToolStripMenuItem
            // 
            this.выбранномToolStripMenuItem.Name = "выбранномToolStripMenuItem";
            this.выбранномToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.выбранномToolStripMenuItem.Text = "Выбранном";
            this.выбранномToolStripMenuItem.Click += new System.EventHandler(this.selectedToolStripMenuItem_Click);
            // 
            // финальномРезультатеToolStripMenuItem
            // 
            this.финальномРезультатеToolStripMenuItem.Name = "финальномРезультатеToolStripMenuItem";
            this.финальномРезультатеToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.финальномРезультатеToolStripMenuItem.Text = "Финальном результате";
            this.финальномРезультатеToolStripMenuItem.Click += new System.EventHandler(this.finalResultToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 225);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 350);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Optical Experiment";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem объектToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem добавитьЛинзуToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem изменитьЛинзуToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сведенияОбИзображенииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбранномToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem финальномРезультатеToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}

