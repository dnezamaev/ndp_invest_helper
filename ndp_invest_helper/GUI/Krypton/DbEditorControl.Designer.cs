
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
            this.krPageSecuritiesIssuers = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.radioButton_Issuer = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioButton_Securities = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.comboBox_SecurityIssuer = new SergeUtils.EasyCompletionComboBox();
            this.krPageCurrencies = new ComponentFactory.Krypton.Navigator.KryptonPage();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.krPageSecuritiesIssuers)).BeginInit();
            this.krPageSecuritiesIssuers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.krPageCurrencies)).BeginInit();
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
            this.krPageSecuritiesIssuers,
            this.krPageCurrencies});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(922, 723);
            this.kryptonNavigator1.TabIndex = 1;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // krPageSecuritiesIssuers
            // 
            this.krPageSecuritiesIssuers.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.krPageSecuritiesIssuers.Controls.Add(this.kryptonPanel1);
            this.krPageSecuritiesIssuers.Controls.Add(this.comboBox_SecurityIssuer);
            this.krPageSecuritiesIssuers.Flags = 65534;
            this.krPageSecuritiesIssuers.LastVisibleSet = true;
            this.krPageSecuritiesIssuers.MinimumSize = new System.Drawing.Size(50, 50);
            this.krPageSecuritiesIssuers.Name = "krPageSecuritiesIssuers";
            this.krPageSecuritiesIssuers.Size = new System.Drawing.Size(780, 721);
            this.krPageSecuritiesIssuers.Text = "Бумаги, эмитенты";
            this.krPageSecuritiesIssuers.ToolTipTitle = "Page ToolTip";
            this.krPageSecuritiesIssuers.UniqueName = "B27FF867BEEF49C9F38A863D8E027AEF";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.radioButton_Issuer);
            this.kryptonPanel1.Controls.Add(this.radioButton_Securities);
            this.kryptonPanel1.Location = new System.Drawing.Point(16, 14);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(173, 32);
            this.kryptonPanel1.TabIndex = 12;
            // 
            // radioButton_Issuer
            // 
            this.radioButton_Issuer.Location = new System.Drawing.Point(81, 3);
            this.radioButton_Issuer.Name = "radioButton_Issuer";
            this.radioButton_Issuer.Size = new System.Drawing.Size(83, 24);
            this.radioButton_Issuer.TabIndex = 13;
            this.radioButton_Issuer.Values.Text = "Эмитент";
            // 
            // radioButton_Securities
            // 
            this.radioButton_Securities.Checked = true;
            this.radioButton_Securities.Location = new System.Drawing.Point(3, 3);
            this.radioButton_Securities.Name = "radioButton_Securities";
            this.radioButton_Securities.Size = new System.Drawing.Size(72, 24);
            this.radioButton_Securities.TabIndex = 12;
            this.radioButton_Securities.Values.Text = "Бумага";
            this.radioButton_Securities.CheckedChanged += new System.EventHandler(this.radioButton_Securities_CheckedChanged);
            // 
            // comboBox_SecurityIssuer
            // 
            this.comboBox_SecurityIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_SecurityIssuer.DisplayMember = "FullName";
            this.comboBox_SecurityIssuer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SecurityIssuer.FormattingEnabled = true;
            this.comboBox_SecurityIssuer.Location = new System.Drawing.Point(14, 61);
            this.comboBox_SecurityIssuer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_SecurityIssuer.MatchingMethod = SergeUtils.StringMatchingMethod.SubString;
            this.comboBox_SecurityIssuer.Name = "comboBox_SecurityIssuer";
            this.comboBox_SecurityIssuer.Size = new System.Drawing.Size(753, 31);
            this.comboBox_SecurityIssuer.TabIndex = 9;
            // 
            // krPageCurrencies
            // 
            this.krPageCurrencies.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.krPageCurrencies.Flags = 65534;
            this.krPageCurrencies.LastVisibleSet = true;
            this.krPageCurrencies.MinimumSize = new System.Drawing.Size(50, 50);
            this.krPageCurrencies.Name = "krPageCurrencies";
            this.krPageCurrencies.Size = new System.Drawing.Size(709, 448);
            this.krPageCurrencies.Text = "Валюты";
            this.krPageCurrencies.ToolTipTitle = "Page ToolTip";
            this.krPageCurrencies.UniqueName = "C161C5FE2AFF45BA29AE3CFCF38484FF";
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
            ((System.ComponentModel.ISupportInitialize)(this.krPageSecuritiesIssuers)).EndInit();
            this.krPageSecuritiesIssuers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.krPageCurrencies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage krPageSecuritiesIssuers;
        private ComponentFactory.Krypton.Navigator.KryptonPage krPageCurrencies;
        private SergeUtils.EasyCompletionComboBox comboBox_SecurityIssuer;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Issuer;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioButton_Securities;
    }
}
