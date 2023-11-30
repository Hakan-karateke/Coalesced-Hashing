namespace CoalescedHashing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnPerformans = new Button();
            labeltime = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            comboBoxMiktar = new ComboBox();
            label1 = new Label();
            labelCakisma = new Label();
            SuspendLayout();
            // 
            // btnPerformans
            // 
            btnPerformans.BackColor = Color.FromArgb(192, 192, 255);
            btnPerformans.Cursor = Cursors.Hand;
            btnPerformans.FlatStyle = FlatStyle.Popup;
            btnPerformans.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnPerformans.Location = new Point(704, 18);
            btnPerformans.Name = "btnPerformans";
            btnPerformans.Size = new Size(141, 41);
            btnPerformans.TabIndex = 0;
            btnPerformans.Text = "Performans Ölç";
            btnPerformans.UseVisualStyleBackColor = false;
            btnPerformans.Click += btnPerformans_Click;
            // 
            // labeltime
            // 
            labeltime.AutoSize = true;
            labeltime.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labeltime.Location = new Point(17, 221);
            labeltime.Name = "labeltime";
            labeltime.Size = new Size(0, 19);
            labeltime.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // comboBoxMiktar
            // 
            comboBoxMiktar.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMiktar.FormattingEnabled = true;
            comboBoxMiktar.Items.AddRange(new object[] { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100" });
            comboBoxMiktar.Location = new Point(627, 29);
            comboBoxMiktar.Name = "comboBoxMiktar";
            comboBoxMiktar.Size = new Size(57, 23);
            comboBoxMiktar.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(580, 27);
            label1.Name = "label1";
            label1.Size = new Size(28, 22);
            label1.TabIndex = 5;
            label1.Text = "%";
            // 
            // labelCakisma
            // 
            labelCakisma.AutoSize = true;
            labelCakisma.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelCakisma.Location = new Point(17, 247);
            labelCakisma.Name = "labelCakisma";
            labelCakisma.Size = new Size(0, 19);
            labelCakisma.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Honeydew;
            ClientSize = new Size(1044, 450);
            Controls.Add(labelCakisma);
            Controls.Add(label1);
            Controls.Add(comboBoxMiktar);
            Controls.Add(labeltime);
            Controls.Add(btnPerformans);
            Name = "Form1";
            Text = "Coalesced Hashing";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPerformans;
        private Label labeltime;
        private ContextMenuStrip contextMenuStrip1;
        private ComboBox comboBoxMiktar;
        private Label label1;
        private Label labelCakisma;
    }
}