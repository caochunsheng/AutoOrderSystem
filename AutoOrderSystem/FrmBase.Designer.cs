namespace AutoOrderSystem
{
    partial class FrmBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBase));
            this.SuspendLayout();
            // 
            // FrmBase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.CaptionBackColorBottom = System.Drawing.SystemColors.ButtonHighlight;
            this.CaptionBackColorTop = System.Drawing.SystemColors.ActiveCaption;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaptionHeight = 32;
            this.CloseBoxSize = new System.Drawing.Size(32, 30);
            this.CloseDownBack = global::AutoOrderSystem.Properties.Resources.Close_3;
            this.CloseMouseBack = global::AutoOrderSystem.Properties.Resources.Close_2;
            this.CloseNormlBack = global::AutoOrderSystem.Properties.Resources.Close_1;
            this.EffectCaption = CCWin.TitleType.Title;
            this.ICoOffset = new System.Drawing.Point(10, 0);
            this.MaxDownBack = global::AutoOrderSystem.Properties.Resources.Max_3;
            this.MaxMouseBack = global::AutoOrderSystem.Properties.Resources.Max_2;
            this.MaxNormlBack = global::AutoOrderSystem.Properties.Resources.Max_1;
            this.MaxSize = new System.Drawing.Size(32, 30);
            this.MiniDownBack = global::AutoOrderSystem.Properties.Resources.Min_3;
            this.MiniMouseBack = global::AutoOrderSystem.Properties.Resources.Min_2;
            this.MiniNormlBack = global::AutoOrderSystem.Properties.Resources.Min_1;
            this.MiniSize = new System.Drawing.Size(32, 30);
            this.Name = "FrmBase";
            this.Opacity = 0.5D;
            this.Radius = 0;
            this.RestoreDownBack = global::AutoOrderSystem.Properties.Resources.Restore_3;
            this.RestoreMouseBack = global::AutoOrderSystem.Properties.Resources.Restore_2;
            this.RestoreNormlBack = global::AutoOrderSystem.Properties.Resources.Restore_1;
            this.RoundStyle = CCWin.SkinClass.RoundStyle.None;
            this.ResumeLayout(false);

        }

        #endregion
    }
}