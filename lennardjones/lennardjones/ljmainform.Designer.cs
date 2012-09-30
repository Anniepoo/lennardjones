namespace lennardjones
{
    partial class ljmainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioCart = new System.Windows.Forms.RadioButton();
            this.radioManhatten = new System.Windows.Forms.RadioButton();
            this.twoSides = new System.Windows.Forms.CheckBox();
            this.newtonian = new System.Windows.Forms.CheckBox();
            this.friction = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // radioCart
            // 
            this.radioCart.AutoSize = true;
            this.radioCart.Location = new System.Drawing.Point(13, 13);
            this.radioCart.Name = "radioCart";
            this.radioCart.Size = new System.Drawing.Size(69, 17);
            this.radioCart.TabIndex = 0;
            this.radioCart.TabStop = true;
            this.radioCart.Text = "Cartesian";
            this.radioCart.UseVisualStyleBackColor = true;
            // 
            // radioManhatten
            // 
            this.radioManhatten.AutoSize = true;
            this.radioManhatten.Location = new System.Drawing.Point(13, 37);
            this.radioManhatten.Name = "radioManhatten";
            this.radioManhatten.Size = new System.Drawing.Size(91, 17);
            this.radioManhatten.TabIndex = 1;
            this.radioManhatten.TabStop = true;
            this.radioManhatten.Text = "Manhatten 45";
            this.radioManhatten.UseVisualStyleBackColor = true;
            // 
            // twoSides
            // 
            this.twoSides.AutoSize = true;
            this.twoSides.Location = new System.Drawing.Point(13, 61);
            this.twoSides.Name = "twoSides";
            this.twoSides.Size = new System.Drawing.Size(70, 17);
            this.twoSides.TabIndex = 2;
            this.twoSides.Text = "two sides";
            this.twoSides.UseVisualStyleBackColor = true;
            // 
            // newtonian
            // 
            this.newtonian.AutoSize = true;
            this.newtonian.Location = new System.Drawing.Point(13, 85);
            this.newtonian.Name = "newtonian";
            this.newtonian.Size = new System.Drawing.Size(77, 17);
            this.newtonian.TabIndex = 3;
            this.newtonian.Text = "Newtonian";
            this.newtonian.UseVisualStyleBackColor = true;
            // 
            // friction
            // 
            this.friction.AutoSize = true;
            this.friction.Location = new System.Drawing.Point(13, 109);
            this.friction.Name = "friction";
            this.friction.Size = new System.Drawing.Size(60, 17);
            this.friction.TabIndex = 4;
            this.friction.Text = "Friction";
            this.friction.UseVisualStyleBackColor = true;
            // 
            // ljmainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 842);
            this.Controls.Add(this.friction);
            this.Controls.Add(this.newtonian);
            this.Controls.Add(this.twoSides);
            this.Controls.Add(this.radioManhatten);
            this.Controls.Add(this.radioCart);
            this.Name = "ljmainform";
            this.Text = "Lennard Jones";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioCart;
        private System.Windows.Forms.RadioButton radioManhatten;
        private System.Windows.Forms.CheckBox twoSides;
        private System.Windows.Forms.CheckBox newtonian;
        private System.Windows.Forms.CheckBox friction;

    }
}

