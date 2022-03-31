
namespace ndp_invest_helper.GUI.Krypton
{
    partial class SettingsForm
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
            this.krPageCommon = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.checkBox_Log = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.comboBox_DataSource = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.krPageAnalytics = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.comboBox_ShowChangesFrom = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            ((System.ComponentModel.ISupportInitialize)(this.krPageCommon)).BeginInit();
            this.krPageCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBox_DataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.krPageAnalytics)).BeginInit();
            this.krPageAnalytics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBox_ShowChangesFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // krPageCommon
            // 
            this.krPageCommon.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.krPageCommon.Controls.Add(this.checkBox_Log);
            this.krPageCommon.Controls.Add(this.kryptonLabel1);
            this.krPageCommon.Controls.Add(this.comboBox_DataSource);
            this.krPageCommon.Flags = 65534;
            this.krPageCommon.LastVisibleSet = true;
            this.krPageCommon.MinimumSize = new System.Drawing.Size(50, 50);
            this.krPageCommon.Name = "krPageCommon";
            this.krPageCommon.Size = new System.Drawing.Size(709, 448);
            this.krPageCommon.Text = "Общие";
            this.krPageCommon.ToolTipTitle = "Page ToolTip";
            this.krPageCommon.UniqueName = "B27FF867BEEF49C9F38A863D8E027AEF";
            // 
            // checkBox_Log
            // 
            this.checkBox_Log.CheckPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.checkBox_Log.Location = new System.Drawing.Point(22, 73);
            this.checkBox_Log.Name = "checkBox_Log";
            this.checkBox_Log.Size = new System.Drawing.Size(101, 24);
            this.checkBox_Log.TabIndex = 2;
            this.checkBox_Log.Values.Text = "Писать лог";
            this.checkBox_Log.CheckedChanged += new System.EventHandler(this.checkBox_Log_CheckedChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(22, 25);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(136, 24);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Источник данных";
            // 
            // comboBox_DataSource
            // 
            this.comboBox_DataSource.DropDownWidth = 121;
            this.comboBox_DataSource.Location = new System.Drawing.Point(164, 25);
            this.comboBox_DataSource.Name = "comboBox_DataSource";
            this.comboBox_DataSource.Size = new System.Drawing.Size(225, 25);
            this.comboBox_DataSource.TabIndex = 0;
            this.comboBox_DataSource.Text = "krComboBoxDataSource";
            this.comboBox_DataSource.SelectedIndexChanged += new System.EventHandler(this.comboBox_DataSource_SelectedIndexChanged);
            // 
            // krPageAnalytics
            // 
            this.krPageAnalytics.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.krPageAnalytics.Controls.Add(this.kryptonLabel2);
            this.krPageAnalytics.Controls.Add(this.comboBox_ShowChangesFrom);
            this.krPageAnalytics.Flags = 65534;
            this.krPageAnalytics.LastVisibleSet = true;
            this.krPageAnalytics.MinimumSize = new System.Drawing.Size(50, 50);
            this.krPageAnalytics.Name = "krPageAnalytics";
            this.krPageAnalytics.Size = new System.Drawing.Size(709, 448);
            this.krPageAnalytics.Text = "Аналитика";
            this.krPageAnalytics.ToolTipTitle = "Page ToolTip";
            this.krPageAnalytics.UniqueName = "C161C5FE2AFF45BA29AE3CFCF38484FF";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(22, 25);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(188, 24);
            this.kryptonLabel2.TabIndex = 6;
            this.kryptonLabel2.Values.Text = "Показывать изменение с ";
            // 
            // comboBox_ShowChangesFrom
            // 
            this.comboBox_ShowChangesFrom.DropDownWidth = 121;
            this.comboBox_ShowChangesFrom.Location = new System.Drawing.Point(216, 25);
            this.comboBox_ShowChangesFrom.Name = "comboBox_ShowChangesFrom";
            this.comboBox_ShowChangesFrom.Size = new System.Drawing.Size(259, 25);
            this.comboBox_ShowChangesFrom.TabIndex = 5;
            this.comboBox_ShowChangesFrom.SelectedIndexChanged += new System.EventHandler(this.comboBox_ShowChangesFrom_SelectedIndexChanged);
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
            this.krPageCommon,
            this.krPageAnalytics});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(800, 450);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonNavigator1);
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.krPageCommon)).EndInit();
            this.krPageCommon.ResumeLayout(false);
            this.krPageCommon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBox_DataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.krPageAnalytics)).EndInit();
            this.krPageAnalytics.ResumeLayout(false);
            this.krPageAnalytics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBox_ShowChangesFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonPage krPageCommon;
        private ComponentFactory.Krypton.Navigator.KryptonPage krPageAnalytics;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox checkBox_Log;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox comboBox_DataSource;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox comboBox_ShowChangesFrom;
    }
}