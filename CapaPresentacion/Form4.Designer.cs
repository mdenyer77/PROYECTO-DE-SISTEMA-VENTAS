namespace CapaPresentacion
{
    partial class Form4
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDventaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaventaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDclienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalgeneralDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoventaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ventaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ventasDataSet4 = new CapaPresentacion.VentasDataSet4();
            this.ventaTableAdapter = new CapaPresentacion.VentasDataSet4TableAdapters.ventaTableAdapter();
            this.button5 = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventasDataSet4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(279, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FACTURACION";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDventaDataGridViewTextBoxColumn,
            this.fechaventaDataGridViewTextBoxColumn,
            this.iDclienteDataGridViewTextBoxColumn,
            this.totalgeneralDataGridViewTextBoxColumn,
            this.estadoventaDataGridViewCheckBoxColumn});
            this.dataGridView1.DataSource = this.ventaBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(-8, 146);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(816, 247);
            this.dataGridView1.TabIndex = 5;
            // 
            // iDventaDataGridViewTextBoxColumn
            // 
            this.iDventaDataGridViewTextBoxColumn.DataPropertyName = "ID_venta";
            this.iDventaDataGridViewTextBoxColumn.HeaderText = "ID_venta";
            this.iDventaDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.iDventaDataGridViewTextBoxColumn.Name = "iDventaDataGridViewTextBoxColumn";
            this.iDventaDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDventaDataGridViewTextBoxColumn.Width = 150;
            // 
            // fechaventaDataGridViewTextBoxColumn
            // 
            this.fechaventaDataGridViewTextBoxColumn.DataPropertyName = "Fecha_venta";
            this.fechaventaDataGridViewTextBoxColumn.HeaderText = "Fecha_venta";
            this.fechaventaDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.fechaventaDataGridViewTextBoxColumn.Name = "fechaventaDataGridViewTextBoxColumn";
            this.fechaventaDataGridViewTextBoxColumn.Width = 150;
            // 
            // iDclienteDataGridViewTextBoxColumn
            // 
            this.iDclienteDataGridViewTextBoxColumn.DataPropertyName = "ID_cliente";
            this.iDclienteDataGridViewTextBoxColumn.HeaderText = "ID_cliente";
            this.iDclienteDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.iDclienteDataGridViewTextBoxColumn.Name = "iDclienteDataGridViewTextBoxColumn";
            this.iDclienteDataGridViewTextBoxColumn.Width = 150;
            // 
            // totalgeneralDataGridViewTextBoxColumn
            // 
            this.totalgeneralDataGridViewTextBoxColumn.DataPropertyName = "Total_general";
            this.totalgeneralDataGridViewTextBoxColumn.HeaderText = "Total_general";
            this.totalgeneralDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.totalgeneralDataGridViewTextBoxColumn.Name = "totalgeneralDataGridViewTextBoxColumn";
            this.totalgeneralDataGridViewTextBoxColumn.Width = 150;
            // 
            // estadoventaDataGridViewCheckBoxColumn
            // 
            this.estadoventaDataGridViewCheckBoxColumn.DataPropertyName = "Estado_venta";
            this.estadoventaDataGridViewCheckBoxColumn.HeaderText = "Estado_venta";
            this.estadoventaDataGridViewCheckBoxColumn.MinimumWidth = 8;
            this.estadoventaDataGridViewCheckBoxColumn.Name = "estadoventaDataGridViewCheckBoxColumn";
            this.estadoventaDataGridViewCheckBoxColumn.Width = 150;
            // 
            // ventaBindingSource
            // 
            this.ventaBindingSource.DataMember = "venta";
            this.ventaBindingSource.DataSource = this.ventasDataSet4;
            // 
            // ventasDataSet4
            // 
            this.ventasDataSet4.DataSetName = "VentasDataSet4";
            this.ventasDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ventaTableAdapter
            // 
            this.ventaTableAdapter.ClearBeforeFill = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1, 102);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 40);
            this.button5.TabIndex = 6;
            this.button5.Text = "Visualizar Reporte";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(841, 378);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventasDataSet4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private VentasDataSet4 ventasDataSet4;
        private System.Windows.Forms.BindingSource ventaBindingSource;
        private VentasDataSet4TableAdapters.ventaTableAdapter ventaTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDventaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaventaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDclienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalgeneralDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn estadoventaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.Button button5;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
    }
}