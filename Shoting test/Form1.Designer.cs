namespace Shoting_test
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Main_Timer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.Refresh_Timer = new System.Windows.Forms.Timer(this.components);
            this.SpecialAtk_Timer = new System.Windows.Forms.Timer(this.components);
            this.Special2Atk_Timer = new System.Windows.Forms.Timer(this.components);
            this.Prj2_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Main_Timer
            // 
            this.Main_Timer.Interval = 15;
            this.Main_Timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("High Tower Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // Refresh_Timer
            // 
            this.Refresh_Timer.Interval = 1000;
            this.Refresh_Timer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // SpecialAtk_Timer
            // 
            this.SpecialAtk_Timer.Tick += new System.EventHandler(this.SpecialAtk_Timer_Tick);
            // 
            // Special2Atk_Timer
            // 
            this.Special2Atk_Timer.Interval = 75;
            this.Special2Atk_Timer.Tick += new System.EventHandler(this.Special2Atk_Timer_Tick);
            // 
            // Prj2_Timer
            // 
            this.Prj2_Timer.Interval = 35;
            this.Prj2_Timer.Tick += new System.EventHandler(this.Prj2_Timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Main_Timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer Refresh_Timer;
        private System.Windows.Forms.Timer SpecialAtk_Timer;
        private System.Windows.Forms.Timer Special2Atk_Timer;
        private System.Windows.Forms.Timer Prj2_Timer;
    }
}

