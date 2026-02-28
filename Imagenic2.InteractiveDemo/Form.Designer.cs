namespace Imagenic2.InteractiveDemo
{
    partial class Form
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
            pictureBox = new PictureBox();
            menuStrip = new MenuStrip();
            entitiesToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            cubeToolStripMenuItem = new ToolStripMenuItem();
            coneToolStripMenuItem = new ToolStripMenuItem();
            sphereToolStripMenuItem = new ToolStripMenuItem();
            controlToolStripMenuItem = new ToolStripMenuItem();
            keyboardToolStripMenuItem = new ToolStripMenuItem();
            mouseToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 33);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1356, 717);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.MouseMove += pictureBox_MouseMove;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { entitiesToolStripMenuItem, controlToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1356, 33);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // entitiesToolStripMenuItem
            // 
            entitiesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addToolStripMenuItem });
            entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            entitiesToolStripMenuItem.Size = new Size(84, 29);
            entitiesToolStripMenuItem.Text = "Entities";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cubeToolStripMenuItem, coneToolStripMenuItem, sphereToolStripMenuItem });
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(148, 34);
            addToolStripMenuItem.Text = "Add";
            // 
            // cubeToolStripMenuItem
            // 
            cubeToolStripMenuItem.Name = "cubeToolStripMenuItem";
            cubeToolStripMenuItem.Size = new Size(169, 34);
            cubeToolStripMenuItem.Text = "Cube";
            // 
            // coneToolStripMenuItem
            // 
            coneToolStripMenuItem.Name = "coneToolStripMenuItem";
            coneToolStripMenuItem.Size = new Size(169, 34);
            coneToolStripMenuItem.Text = "Cone";
            // 
            // sphereToolStripMenuItem
            // 
            sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            sphereToolStripMenuItem.Size = new Size(169, 34);
            sphereToolStripMenuItem.Text = "Sphere";
            // 
            // controlToolStripMenuItem
            // 
            controlToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { keyboardToolStripMenuItem, mouseToolStripMenuItem });
            controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            controlToolStripMenuItem.Size = new Size(87, 29);
            controlToolStripMenuItem.Text = "Control";
            // 
            // keyboardToolStripMenuItem
            // 
            keyboardToolStripMenuItem.Checked = true;
            keyboardToolStripMenuItem.CheckState = CheckState.Checked;
            keyboardToolStripMenuItem.Name = "keyboardToolStripMenuItem";
            keyboardToolStripMenuItem.Size = new Size(270, 34);
            keyboardToolStripMenuItem.Text = "Keyboard";
            keyboardToolStripMenuItem.Click += keyboardToolStripMenuItem_Click;
            // 
            // mouseToolStripMenuItem
            // 
            mouseToolStripMenuItem.Name = "mouseToolStripMenuItem";
            mouseToolStripMenuItem.Size = new Size(270, 34);
            mouseToolStripMenuItem.Text = "Mouse";
            mouseToolStripMenuItem.Click += mouseToolStripMenuItem_Click;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1356, 750);
            Controls.Add(pictureBox);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "Form";
            Text = "Interactive Demo";
            FormClosed += Form_FormClosed;
            KeyDown += Form_KeyDown;
            KeyUp += Form_KeyUp;
            Resize += Form_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private MenuStrip menuStrip;
        private ToolStripMenuItem entitiesToolStripMenuItem;
        private ToolStripMenuItem controlToolStripMenuItem;
        private ToolStripMenuItem keyboardToolStripMenuItem;
        private ToolStripMenuItem mouseToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem cubeToolStripMenuItem;
        private ToolStripMenuItem coneToolStripMenuItem;
        private ToolStripMenuItem sphereToolStripMenuItem;
    }
}
