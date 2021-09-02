
namespace FileCollector
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSource = new System.Windows.Forms.Label();
            this.comboBoxSource = new System.Windows.Forms.ComboBox();
            this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.checkBoxRegExpression = new System.Windows.Forms.CheckBox();
            this.labelDestination = new System.Windows.Forms.Label();
            this.comboBoxDestination = new System.Windows.Forms.ComboBox();
            this.checkBoxDirectoryTree = new System.Windows.Forms.CheckBox();
            this.buttonCollect = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(12, 15);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(42, 12);
            this.labelSource.TabIndex = 0;
            this.labelSource.Text = "&Source:";
            // 
            // comboBoxSource
            // 
            this.comboBoxSource.AllowDrop = true;
            this.comboBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.comboBoxSource.FormattingEnabled = true;
            this.comboBoxSource.Location = new System.Drawing.Point(83, 12);
            this.comboBoxSource.Name = "comboBoxSource";
            this.comboBoxSource.Size = new System.Drawing.Size(593, 20);
            this.comboBoxSource.TabIndex = 1;
            this.comboBoxSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.comboBoxPath_DragDrop);
            this.comboBoxSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.comboBoxPath_DragEnter);
            // 
            // checkBoxRecursive
            // 
            this.checkBoxRecursive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRecursive.AutoSize = true;
            this.checkBoxRecursive.Location = new System.Drawing.Point(682, 14);
            this.checkBoxRecursive.Name = "checkBoxRecursive";
            this.checkBoxRecursive.Size = new System.Drawing.Size(75, 16);
            this.checkBoxRecursive.TabIndex = 2;
            this.checkBoxRecursive.Text = "&Recursive";
            this.checkBoxRecursive.UseVisualStyleBackColor = true;
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(12, 41);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(34, 12);
            this.labelFilter.TabIndex = 3;
            this.labelFilter.Text = "&Filter:";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilter.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(83, 38);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(593, 20);
            this.comboBoxFilter.TabIndex = 4;
            // 
            // checkBoxRegExpression
            // 
            this.checkBoxRegExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRegExpression.AutoSize = true;
            this.checkBoxRegExpression.Location = new System.Drawing.Point(682, 40);
            this.checkBoxRegExpression.Name = "checkBoxRegExpression";
            this.checkBoxRegExpression.Size = new System.Drawing.Size(106, 16);
            this.checkBoxRegExpression.TabIndex = 5;
            this.checkBoxRegExpression.Text = "Reg. &Expression";
            this.checkBoxRegExpression.UseVisualStyleBackColor = true;
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(12, 67);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(65, 12);
            this.labelDestination.TabIndex = 6;
            this.labelDestination.Text = "&Destination:";
            // 
            // comboBoxDestination
            // 
            this.comboBoxDestination.AllowDrop = true;
            this.comboBoxDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDestination.FormattingEnabled = true;
            this.comboBoxDestination.Location = new System.Drawing.Point(83, 64);
            this.comboBoxDestination.Name = "comboBoxDestination";
            this.comboBoxDestination.Size = new System.Drawing.Size(593, 20);
            this.comboBoxDestination.TabIndex = 7;
            this.comboBoxDestination.DragDrop += new System.Windows.Forms.DragEventHandler(this.comboBoxPath_DragDrop);
            this.comboBoxDestination.DragEnter += new System.Windows.Forms.DragEventHandler(this.comboBoxPath_DragEnter);
            // 
            // checkBoxDirectoryTree
            // 
            this.checkBoxDirectoryTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDirectoryTree.AutoSize = true;
            this.checkBoxDirectoryTree.Location = new System.Drawing.Point(682, 66);
            this.checkBoxDirectoryTree.Name = "checkBoxDirectoryTree";
            this.checkBoxDirectoryTree.Size = new System.Drawing.Size(98, 16);
            this.checkBoxDirectoryTree.TabIndex = 8;
            this.checkBoxDirectoryTree.Text = "Directory &Tree";
            this.checkBoxDirectoryTree.UseVisualStyleBackColor = true;
            // 
            // buttonCollect
            // 
            this.buttonCollect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCollect.Location = new System.Drawing.Point(713, 90);
            this.buttonCollect.Name = "buttonCollect";
            this.buttonCollect.Size = new System.Drawing.Size(75, 23);
            this.buttonCollect.TabIndex = 9;
            this.buttonCollect.Text = "&Collect";
            this.buttonCollect.UseVisualStyleBackColor = true;
            this.buttonCollect.Click += new System.EventHandler(this.buttonCollect_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 125);
            this.Controls.Add(this.buttonCollect);
            this.Controls.Add(this.checkBoxDirectoryTree);
            this.Controls.Add(this.comboBoxDestination);
            this.Controls.Add(this.labelDestination);
            this.Controls.Add(this.checkBoxRegExpression);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.checkBoxRecursive);
            this.Controls.Add(this.comboBoxSource);
            this.Controls.Add(this.labelSource);
            this.Name = "FormMain";
            this.Text = "FileCollector";
            this.Load += new System.EventHandler(this.load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.ComboBox comboBoxSource;
        private System.Windows.Forms.CheckBox checkBoxRecursive;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.CheckBox checkBoxRegExpression;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.ComboBox comboBoxDestination;
        private System.Windows.Forms.CheckBox checkBoxDirectoryTree;
        private System.Windows.Forms.Button buttonCollect;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

