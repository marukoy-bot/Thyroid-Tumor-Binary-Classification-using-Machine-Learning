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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnBrowse = new MaterialSkin.Controls.MaterialButton();
            txtImagePath = new MaterialSkin.Controls.MaterialTextBox();
            lblResult = new MaterialSkin.Controls.MaterialLabel();
            lblServerStatus = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.AllowDrop = true;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(268, 175);
            pictureBox1.MinimumSize = new Size(255, 255);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(255, 255);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(268, 71);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(255, 98);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBrowse.AutoSize = false;
            btnBrowse.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnBrowse.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnBrowse.Depth = 0;
            btnBrowse.HighEmphasis = true;
            btnBrowse.Icon = (Image)resources.GetObject("btnBrowse.Icon");
            btnBrowse.Location = new Point(558, 436);
            btnBrowse.Margin = new Padding(4, 6, 4, 6);
            btnBrowse.MouseState = MaterialSkin.MouseState.HOVER;
            btnBrowse.Name = "btnBrowse";
            btnBrowse.NoAccentTextColor = Color.Empty;
            btnBrowse.Size = new Size(109, 50);
            btnBrowse.TabIndex = 7;
            btnBrowse.Text = "Browse";
            btnBrowse.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnBrowse.UseAccentColor = false;
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtImagePath
            // 
            txtImagePath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtImagePath.AnimateReadOnly = false;
            txtImagePath.BorderStyle = BorderStyle.None;
            txtImagePath.Depth = 0;
            txtImagePath.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtImagePath.Hint = "Browse or drag an image";
            txtImagePath.LeadingIcon = null;
            txtImagePath.Location = new Point(133, 436);
            txtImagePath.MaxLength = 50;
            txtImagePath.MouseState = MaterialSkin.MouseState.OUT;
            txtImagePath.Multiline = false;
            txtImagePath.Name = "txtImagePath";
            txtImagePath.Size = new Size(418, 50);
            txtImagePath.TabIndex = 8;
            txtImagePath.Text = "";
            txtImagePath.TrailingIcon = null;
            // 
            // lblResult
            // 
            lblResult.Anchor = AnchorStyles.Bottom;
            lblResult.AutoSize = true;
            lblResult.BackColor = Color.Transparent;
            lblResult.Depth = 0;
            lblResult.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblResult.Location = new Point(369, 501);
            lblResult.MouseState = MaterialSkin.MouseState.HOVER;
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(53, 19);
            lblResult.TabIndex = 9;
            lblResult.Text = "Result: ";
            // 
            // lblServerStatus
            // 
            lblServerStatus.AutoSize = true;
            lblServerStatus.BackColor = Color.Transparent;
            lblServerStatus.Depth = 0;
            lblServerStatus.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblServerStatus.ForeColor = Color.Red;
            lblServerStatus.Location = new Point(24, 71);
            lblServerStatus.MouseState = MaterialSkin.MouseState.HOVER;
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(49, 19);
            lblServerStatus.TabIndex = 10;
            lblServerStatus.Text = "Offline";
            // 
            // frmEntry
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 543);
            Controls.Add(lblServerStatus);
            Controls.Add(lblResult);
            Controls.Add(txtImagePath);
            Controls.Add(btnBrowse);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Font = new Font("Product Sans", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(519, 540);
            Name = "frmEntry";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " THYRA v1.0.3";
            FormClosed += frmEntry_FormClosed;
            Load += frmEntry_Load;
            Resize += frmEntry_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialButton btnBrowse;
        private MaterialSkin.Controls.MaterialTextBox txtImagePath;
        private MaterialSkin.Controls.MaterialLabel lblResult;
        private MaterialSkin.Controls.MaterialLabel lblServerStatus;
    }
}
