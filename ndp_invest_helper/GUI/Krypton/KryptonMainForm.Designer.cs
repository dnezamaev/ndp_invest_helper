
namespace ndp_invest_helper.GUI.Krypton
{
    partial class KryptonMainForm
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
            this.components = new System.ComponentModel.Container();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonDockingManager = new ComponentFactory.Krypton.Docking.KryptonDockingManager();
            this.kryptonPanelMain = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonDockableWorkspace = new ComponentFactory.Krypton.Docking.KryptonDockableWorkspace();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CancelDeal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_RunTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_XmlToSqlite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Service = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanelMain
            // 
            this.kryptonPanelMain.Controls.Add(this.kryptonDockableWorkspace);
            this.kryptonPanelMain.Controls.Add(this.menuStrip1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanelMain.TabIndex = 0;
            // 
            // kryptonDockableWorkspace
            // 
            this.kryptonDockableWorkspace.AutoHiddenHost = false;
            this.kryptonDockableWorkspace.CompactFlags = ((ComponentFactory.Krypton.Workspace.CompactFlags)(((ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptyCells | ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | ComponentFactory.Krypton.Workspace.CompactFlags.PromoteLeafs)));
            this.kryptonDockableWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace.Location = new System.Drawing.Point(0, 28);
            this.kryptonDockableWorkspace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonDockableWorkspace.Name = "kryptonDockableWorkspace";
            // 
            // 
            // 
            this.kryptonDockableWorkspace.Root.UniqueName = "5C17D7183E62446062888CCECCE03369";
            this.kryptonDockableWorkspace.Root.WorkspaceControl = this.kryptonDockableWorkspace;
            this.kryptonDockableWorkspace.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace.Size = new System.Drawing.Size(800, 422);
            this.kryptonDockableWorkspace.TabIndex = 16;
            this.kryptonDockableWorkspace.TabStop = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.действияToolStripMenuItem,
            this.toolStripMenuItem_Service,
            this.toolStripMenuItem_About});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // действияToolStripMenuItem
            // 
            this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_CancelDeal,
            this.toolStripMenuItem_RunTask,
            this.toolStripMenuItem_XmlToSqlite});
            this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
            this.действияToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.действияToolStripMenuItem.Text = "Действия";
            // 
            // toolStripMenuItem_CancelDeal
            // 
            this.toolStripMenuItem_CancelDeal.Name = "toolStripMenuItem_CancelDeal";
            this.toolStripMenuItem_CancelDeal.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem_CancelDeal.Text = "Отменить сделку";
            this.toolStripMenuItem_CancelDeal.Click += new System.EventHandler(this.toolStripMenuItem_CancelDeal_Click);
            // 
            // toolStripMenuItem_RunTask
            // 
            this.toolStripMenuItem_RunTask.Name = "toolStripMenuItem_RunTask";
            this.toolStripMenuItem_RunTask.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem_RunTask.Text = "Выполнить task.xml";
            this.toolStripMenuItem_RunTask.Click += new System.EventHandler(this.toolStripMenuItem_RunTask_Click);
            // 
            // toolStripMenuItem_XmlToSqlite
            // 
            this.toolStripMenuItem_XmlToSqlite.Name = "toolStripMenuItem_XmlToSqlite";
            this.toolStripMenuItem_XmlToSqlite.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem_XmlToSqlite.Text = "Импорт базы из XML";
            this.toolStripMenuItem_XmlToSqlite.Click += new System.EventHandler(this.toolStripMenuItem_XmlToSqlite_Click);
            // 
            // toolStripMenuItem_Service
            // 
            this.toolStripMenuItem_Service.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Settings});
            this.toolStripMenuItem_Service.Name = "toolStripMenuItem_Service";
            this.toolStripMenuItem_Service.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItem_Service.Text = "Сервис";
            // 
            // toolStripMenuItem_Settings
            // 
            this.toolStripMenuItem_Settings.Name = "toolStripMenuItem_Settings";
            this.toolStripMenuItem_Settings.Size = new System.Drawing.Size(176, 26);
            this.toolStripMenuItem_Settings.Text = "Настройки...";
            this.toolStripMenuItem_Settings.Click += new System.EventHandler(this.toolStripMenuItem_Settings_Click);
            // 
            // toolStripMenuItem_About
            // 
            this.toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            this.toolStripMenuItem_About.Size = new System.Drawing.Size(118, 24);
            this.toolStripMenuItem_About.Text = "О программе";
            this.toolStripMenuItem_About.Click += new System.EventHandler(this.toolStripMenuItem_About_Click);
            // 
            // KryptonMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanelMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "KryptonMainForm";
            this.Text = "NDP invest helper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.kryptonPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Docking.KryptonDockingManager kryptonDockingManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem действияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CancelDeal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RunTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_XmlToSqlite;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Service;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_About;
        private ComponentFactory.Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Settings;
    }
}