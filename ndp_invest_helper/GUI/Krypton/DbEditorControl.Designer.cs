
namespace ndp_invest_helper.GUI.Krypton
{
    partial class DbEditorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.krPageDiversification = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.krPageCurrencies = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage4 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.dbEditorDiversityControl = new ndp_invest_helper.GUI.Krypton.DbEditorDiversityControl();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.krPageDiversification)).BeginInit();
            this.krPageDiversification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.krPageCurrencies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Bar.BarOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.kryptonNavigator1.Bar.CheckButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
            this.kryptonNavigator1.Bar.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.kryptonNavigator1.Bar.TabBorderStyle = ComponentFactory.Krypton.Toolkit.TabBorderStyle.OneNote;
            this.kryptonNavigator1.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.BarCheckButtonGroupOutside;
            this.kryptonNavigator1.PageBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.krPageDiversification,
            this.krPageCurrencies});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(922, 723);
            this.kryptonNavigator1.TabIndex = 1;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // krPageDiversification
            // 
            this.krPageDiversification.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.krPageDiversification.Controls.Add(this.dbEditorDiversityControl);
            this.krPageDiversification.Flags = 65534;
            this.krPageDiversification.LastVisibleSet = true;
            this.krPageDiversification.MinimumSize = new System.Drawing.Size(50, 50);
            this.krPageDiversification.Name = "krPageDiversification";
            this.krPageDiversification.Size = new System.Drawing.Size(783, 721);
            this.krPageDiversification.Text = "Диверсификация";
            this.krPageDiversification.ToolTipTitle = "Page ToolTip";
            this.krPageDiversification.UniqueName = "B27FF867BEEF49C9F38A863D8E027AEF";
            // 
            // krPageCurrencies
            // 
            this.krPageCurrencies.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.krPageCurrencies.Flags = 65534;
            this.krPageCurrencies.LastVisibleSet = true;
            this.krPageCurrencies.MinimumSize = new System.Drawing.Size(50, 50);
            this.krPageCurrencies.Name = "krPageCurrencies";
            this.krPageCurrencies.Size = new System.Drawing.Size(783, 721);
            this.krPageCurrencies.Text = "Валюты";
            this.krPageCurrencies.ToolTipTitle = "Page ToolTip";
            this.krPageCurrencies.UniqueName = "C161C5FE2AFF45BA29AE3CFCF38484FF";
            // 
            // kryptonPage4
            // 
            this.kryptonPage4.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage4.Flags = 65534;
            this.kryptonPage4.LastVisibleSet = true;
            this.kryptonPage4.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage4.Name = "kryptonPage4";
            this.kryptonPage4.Size = new System.Drawing.Size(100, 100);
            this.kryptonPage4.Text = "kryptonPage4";
            this.kryptonPage4.ToolTipTitle = "Page ToolTip";
            this.kryptonPage4.UniqueName = "CC32A7A41B374CDA4CBE9CBB733537F0";
            // 
            // dbEditorDiversityControl
            // 
            this.dbEditorDiversityControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbEditorDiversityControl.Location = new System.Drawing.Point(0, 0);
            this.dbEditorDiversityControl.Name = "dbEditorDiversityControl";
            this.dbEditorDiversityControl.Size = new System.Drawing.Size(783, 721);
            this.dbEditorDiversityControl.TabIndex = 0;
            // 
            // DbEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonNavigator1);
            this.Name = "DbEditorControl";
            this.Size = new System.Drawing.Size(922, 723);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.krPageDiversification)).EndInit();
            this.krPageDiversification.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.krPageCurrencies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage krPageDiversification;
        private ComponentFactory.Krypton.Navigator.KryptonPage krPageCurrencies;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage4;
        private DbEditorDiversityControl dbEditorDiversityControl;
    }
}
