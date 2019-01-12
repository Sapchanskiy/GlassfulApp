namespace GlassfullPlugin.UI
{
    partial class MainForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LowDiameter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BottomThickness = new System.Windows.Forms.TextBox();
            this.Height = new System.Windows.Forms.TextBox();
            this.HighDiameter = new System.Windows.Forms.TextBox();
            this.WallWidth = new System.Windows.Forms.TextBox();
            this.BuildButton = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.LowDiameter);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.BottomThickness);
            this.groupBox2.Controls.Add(this.Height);
            this.groupBox2.Controls.Add(this.HighDiameter);
            this.groupBox2.Controls.Add(this.WallWidth);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 184);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Построение детали";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Диаметр нижней окружности (см)";
            // 
            // LowDiameter
            // 
            this.LowDiameter.Location = new System.Drawing.Point(221, 142);
            this.LowDiameter.Name = "LowDiameter";
            this.LowDiameter.Size = new System.Drawing.Size(68, 20);
            this.LowDiameter.TabIndex = 9;
            this.LowDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDoubleTextBoxs_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Толщина дна стакана (см)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Высота стакана (см)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Диаметр верхней окружности (см)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Толщина стенки стакана (см)";
            // 
            // BottomThickness
            // 
            this.BottomThickness.Location = new System.Drawing.Point(221, 116);
            this.BottomThickness.Name = "BottomThickness";
            this.BottomThickness.Size = new System.Drawing.Size(68, 20);
            this.BottomThickness.TabIndex = 3;
            this.BottomThickness.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDoubleTextBoxs_KeyPress);
            // 
            // Height
            // 
            this.Height.Location = new System.Drawing.Point(221, 89);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(68, 20);
            this.Height.TabIndex = 2;
            this.Height.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDoubleTextBoxs_KeyPress);
            // 
            // HighDiameter
            // 
            this.HighDiameter.Location = new System.Drawing.Point(221, 63);
            this.HighDiameter.Name = "HighDiameter";
            this.HighDiameter.Size = new System.Drawing.Size(68, 20);
            this.HighDiameter.TabIndex = 1;
            this.HighDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDoubleTextBoxs_KeyPress);
            // 
            // WallWidth
            // 
            this.WallWidth.Location = new System.Drawing.Point(221, 37);
            this.WallWidth.Name = "WallWidth";
            this.WallWidth.Size = new System.Drawing.Size(68, 20);
            this.WallWidth.TabIndex = 0;
            this.WallWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDoubleTextBoxs_KeyPress);
            // 
            // BuildButton
            // 
            this.BuildButton.Location = new System.Drawing.Point(122, 204);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(75, 23);
            this.BuildButton.TabIndex = 8;
            this.BuildButton.Text = "Построить";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 239);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BuildButton);
            this.MaximumSize = new System.Drawing.Size(341, 378);
            this.MinimumSize = new System.Drawing.Size(241, 278);
            this.Name = "MainForm";
            this.Text = "Построение подшипника";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BottomThickness;
        private System.Windows.Forms.TextBox Height;
        private System.Windows.Forms.TextBox HighDiameter;
        private System.Windows.Forms.TextBox WallWidth;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LowDiameter;
    }
}

