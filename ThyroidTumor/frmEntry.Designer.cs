namespace ThyroidTumor
{
    partial class frmEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntry));
            txtImagePath = new TextBox();
            btnBrowse = new Button();
            pictureBox1 = new PictureBox();
            lblResult = new Label();
            lblServerStatus = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // txtImagePath
            // 
            txtImagePath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtImagePath.BackColor = Color.White;
            txtImagePath.Font = new Font("Product Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtImagePath.Location = new Point(189, 395);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.PlaceholderText = "Browse or drag an image";
            txtImagePath.Size = new Size(353, 22);
            txtImagePath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBrowse.Font = new Font("Product Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBrowse.Location = new Point(548, 394);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 25);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.AllowDrop = true;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(268, 116);
            pictureBox1.MinimumSize = new Size(255, 255);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(255, 255);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            // 
            // lblResult
            // 
            lblResult.Anchor = AnchorStyles.Bottom;
            lblResult.BackColor = Color.Transparent;
            lblResult.Font = new Font("Product Sans", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResult.Location = new Point(268, 435);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(255, 21);
            lblResult.TabIndex = 3;
            lblResult.Text = "Result: ";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblServerStatus
            // 
            lblServerStatus.BackColor = Color.Transparent;
            lblServerStatus.Font = new Font("Product Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblServerStatus.ForeColor = Color.Red;
            lblServerStatus.Location = new Point(12, 10);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(195, 16);
            lblServerStatus.TabIndex = 4;
            lblServerStatus.Text = "Offline";
            lblServerStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(268, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(255, 98);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // frmEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 480);
            Controls.Add(pictureBox2);
            Controls.Add(lblServerStatus);
            Controls.Add(lblResult);
            Controls.Add(pictureBox1);
            Controls.Add(btnBrowse);
            Controls.Add(txtImagePath);
            Font = new Font("Product Sans", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(519, 519);
            Name = "frmEntry";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " THYRA";
            FormClosed += frmEntry_FormClosed;
            Load += frmEntry_Load;
            Resize += frmEntry_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtImagePath;
        private Button btnBrowse;
        private PictureBox pictureBox1;
        private Label lblResult;
        private Label lblServerStatus;
        private PictureBox pictureBox2;
    }
}
