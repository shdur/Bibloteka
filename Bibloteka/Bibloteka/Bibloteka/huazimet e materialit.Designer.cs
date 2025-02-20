namespace Bibloteka
{
    partial class huazimet_e_materialit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(huazimet_e_materialit));
            this.label1huaz = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnkluser = new System.Windows.Forms.Button();
            this.btnklvon = new System.Windows.Forms.Button();
            this.btnklpagtotal = new System.Windows.Forms.Button();
            this.btnklpag = new System.Windows.Forms.Button();
            this.btnklhuaz = new System.Windows.Forms.Button();
            this.btnklmat = new System.Windows.Forms.Button();
            this.biblotekaDataSet2 = new Bibloteka.BiblotekaDataSet2();
            this.clientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientsTableAdapter = new Bibloteka.BiblotekaDataSet2TableAdapters.ClientsTableAdapter();
            this.biblotekaDataSet3 = new Bibloteka.BiblotekaDataSet3();
            this.loansBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loansTableAdapter = new Bibloteka.BiblotekaDataSet3TableAdapters.LoansTableAdapter();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerhuazimi = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerkthimi = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDowntarifa = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnhuaruaj = new System.Windows.Forms.Button();
            this.dataGridViewhuaz = new System.Windows.Forms.DataGridView();
            this.combohuaklienti = new System.Windows.Forms.ComboBox();
            this.combohuamateriali = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.biblotekaDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.biblotekaDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loansBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDowntarifa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewhuaz)).BeginInit();
            this.SuspendLayout();
            // 
            // label1huaz
            // 
            this.label1huaz.AutoSize = true;
            this.label1huaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1huaz.ForeColor = System.Drawing.Color.White;
            this.label1huaz.Location = new System.Drawing.Point(79, 154);
            this.label1huaz.Name = "label1huaz";
            this.label1huaz.Size = new System.Drawing.Size(0, 25);
            this.label1huaz.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(120, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Chocolate;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.btnkluser);
            this.panel1.Controls.Add(this.btnklvon);
            this.panel1.Controls.Add(this.btnklpagtotal);
            this.panel1.Controls.Add(this.btnklpag);
            this.panel1.Controls.Add(this.btnklhuaz);
            this.panel1.Controls.Add(this.btnklmat);
            this.panel1.Controls.Add(this.label1huaz);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 978);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(57, 702);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(282, 66);
            this.button1.TabIndex = 9;
            this.button1.Text = "Klientet Aktive";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 895);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(111, 70);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnkluser
            // 
            this.btnkluser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnkluser.Location = new System.Drawing.Point(57, 798);
            this.btnkluser.Name = "btnkluser";
            this.btnkluser.Size = new System.Drawing.Size(282, 66);
            this.btnkluser.TabIndex = 7;
            this.btnkluser.Text = "Userat";
            this.btnkluser.UseVisualStyleBackColor = true;
            this.btnkluser.Click += new System.EventHandler(this.btnkluser_Click);
            // 
            // btnklvon
            // 
            this.btnklvon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnklvon.Location = new System.Drawing.Point(57, 610);
            this.btnklvon.Name = "btnklvon";
            this.btnklvon.Size = new System.Drawing.Size(282, 66);
            this.btnklvon.TabIndex = 6;
            this.btnklvon.Text = "Huazimet e Vonuara";
            this.btnklvon.UseVisualStyleBackColor = true;
            this.btnklvon.Click += new System.EventHandler(this.btnklvon_Click);
            // 
            // btnklpagtotal
            // 
            this.btnklpagtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnklpagtotal.Location = new System.Drawing.Point(57, 513);
            this.btnklpagtotal.Name = "btnklpagtotal";
            this.btnklpagtotal.Size = new System.Drawing.Size(282, 66);
            this.btnklpagtotal.TabIndex = 5;
            this.btnklpagtotal.Text = "Pagesat Totale të Klientëve";
            this.btnklpagtotal.UseVisualStyleBackColor = true;
            this.btnklpagtotal.Click += new System.EventHandler(this.btnklpagtotal_Click);
            // 
            // btnklpag
            // 
            this.btnklpag.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnklpag.Location = new System.Drawing.Point(57, 421);
            this.btnklpag.Name = "btnklpag";
            this.btnklpag.Size = new System.Drawing.Size(282, 66);
            this.btnklpag.TabIndex = 4;
            this.btnklpag.Text = "Menaxhimin e Pagesave";
            this.btnklpag.UseVisualStyleBackColor = true;
            this.btnklpag.Click += new System.EventHandler(this.btnklpag_Click);
            // 
            // btnklhuaz
            // 
            this.btnklhuaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnklhuaz.Location = new System.Drawing.Point(57, 332);
            this.btnklhuaz.Name = "btnklhuaz";
            this.btnklhuaz.Size = new System.Drawing.Size(282, 66);
            this.btnklhuaz.TabIndex = 3;
            this.btnklhuaz.Text = "Menagjimin e Materialeve";
            this.btnklhuaz.UseVisualStyleBackColor = true;
            this.btnklhuaz.Click += new System.EventHandler(this.btnklhuaz_Click);
            // 
            // btnklmat
            // 
            this.btnklmat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnklmat.Location = new System.Drawing.Point(57, 241);
            this.btnklmat.Name = "btnklmat";
            this.btnklmat.Size = new System.Drawing.Size(282, 66);
            this.btnklmat.TabIndex = 2;
            this.btnklmat.Text = "Menaxhimi i Klienteve";
            this.btnklmat.UseVisualStyleBackColor = true;
            this.btnklmat.Click += new System.EventHandler(this.btnklmat_Click);
            // 
            // biblotekaDataSet2
            // 
            this.biblotekaDataSet2.DataSetName = "BiblotekaDataSet2";
            this.biblotekaDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientsBindingSource
            // 
            this.clientsBindingSource.DataMember = "Clients";
            this.clientsBindingSource.DataSource = this.biblotekaDataSet2;
            // 
            // clientsTableAdapter
            // 
            this.clientsTableAdapter.ClearBeforeFill = true;
            // 
            // biblotekaDataSet3
            // 
            this.biblotekaDataSet3.DataSetName = "BiblotekaDataSet3";
            this.biblotekaDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // loansBindingSource
            // 
            this.loansBindingSource.DataMember = "Loans";
            this.loansBindingSource.DataSource = this.biblotekaDataSet3;
            // 
            // loansTableAdapter
            // 
            this.loansTableAdapter.ClearBeforeFill = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(494, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 29);
            this.label5.TabIndex = 46;
            this.label5.Text = "Klienti :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(938, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 29);
            this.label2.TabIndex = 48;
            this.label2.Text = "Materiali :";
            // 
            // dateTimePickerhuazimi
            // 
            this.dateTimePickerhuazimi.Location = new System.Drawing.Point(1561, 230);
            this.dateTimePickerhuazimi.Name = "dateTimePickerhuazimi";
            this.dateTimePickerhuazimi.Size = new System.Drawing.Size(233, 22);
            this.dateTimePickerhuazimi.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1350, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(205, 29);
            this.label6.TabIndex = 49;
            this.label6.Text = "Data e huazimit :";
            // 
            // dateTimePickerkthimi
            // 
            this.dateTimePickerkthimi.Location = new System.Drawing.Point(630, 321);
            this.dateTimePickerkthimi.Name = "dateTimePickerkthimi";
            this.dateTimePickerkthimi.Size = new System.Drawing.Size(233, 22);
            this.dateTimePickerkthimi.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(437, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 29);
            this.label3.TabIndex = 51;
            this.label3.Text = "Data e kthimit :";
            // 
            // numericUpDowntarifa
            // 
            this.numericUpDowntarifa.Location = new System.Drawing.Point(1161, 321);
            this.numericUpDowntarifa.Name = "numericUpDowntarifa";
            this.numericUpDowntarifa.Size = new System.Drawing.Size(230, 22);
            this.numericUpDowntarifa.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(938, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 29);
            this.label4.TabIndex = 55;
            this.label4.Text = "Tarifa e denimit  :";
            // 
            // btnhuaruaj
            // 
            this.btnhuaruaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuaruaj.Location = new System.Drawing.Point(1043, 445);
            this.btnhuaruaj.Name = "btnhuaruaj";
            this.btnhuaruaj.Size = new System.Drawing.Size(179, 84);
            this.btnhuaruaj.TabIndex = 56;
            this.btnhuaruaj.Text = "Ruaj";
            this.btnhuaruaj.UseVisualStyleBackColor = true;
            this.btnhuaruaj.Click += new System.EventHandler(this.btnhuaruaj_Click);
            // 
            // dataGridViewhuaz
            // 
            this.dataGridViewhuaz.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewhuaz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewhuaz.Location = new System.Drawing.Point(818, 661);
            this.dataGridViewhuaz.Name = "dataGridViewhuaz";
            this.dataGridViewhuaz.RowHeadersWidth = 51;
            this.dataGridViewhuaz.RowTemplate.Height = 24;
            this.dataGridViewhuaz.Size = new System.Drawing.Size(669, 309);
            this.dataGridViewhuaz.TabIndex = 58;
            // 
            // combohuaklienti
            // 
            this.combohuaklienti.FormattingEnabled = true;
            this.combohuaklienti.Location = new System.Drawing.Point(630, 223);
            this.combohuaklienti.Name = "combohuaklienti";
            this.combohuaklienti.Size = new System.Drawing.Size(233, 24);
            this.combohuaklienti.TabIndex = 59;
            // 
            // combohuamateriali
            // 
            this.combohuamateriali.FormattingEnabled = true;
            this.combohuamateriali.Location = new System.Drawing.Point(1089, 228);
            this.combohuamateriali.Name = "combohuamateriali";
            this.combohuamateriali.Size = new System.Drawing.Size(220, 24);
            this.combohuamateriali.TabIndex = 60;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(221, 907);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 45);
            this.button2.TabIndex = 13;
            this.button2.Text = "Kthehu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // huazimet_e_materialit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1924, 977);
            this.Controls.Add(this.combohuamateriali);
            this.Controls.Add(this.combohuaklienti);
            this.Controls.Add(this.dataGridViewhuaz);
            this.Controls.Add(this.btnhuaruaj);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDowntarifa);
            this.Controls.Add(this.dateTimePickerkthimi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerhuazimi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Name = "huazimet_e_materialit";
            this.Text = "Huazimet e Materialit";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.huazimet_e_materialit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.biblotekaDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.biblotekaDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loansBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDowntarifa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewhuaz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1huaz;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnkluser;
        private System.Windows.Forms.Button btnklvon;
        private System.Windows.Forms.Button btnklpagtotal;
        private System.Windows.Forms.Button btnklpag;
        private System.Windows.Forms.Button btnklhuaz;
        private System.Windows.Forms.Button btnklmat;
        private BiblotekaDataSet2 biblotekaDataSet2;
        private System.Windows.Forms.BindingSource clientsBindingSource;
        private BiblotekaDataSet2TableAdapters.ClientsTableAdapter clientsTableAdapter;
        private BiblotekaDataSet3 biblotekaDataSet3;
        private System.Windows.Forms.BindingSource loansBindingSource;
        private BiblotekaDataSet3TableAdapters.LoansTableAdapter loansTableAdapter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerhuazimi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerkthimi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDowntarifa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnhuaruaj;
        private System.Windows.Forms.DataGridView dataGridViewhuaz;
        private System.Windows.Forms.ComboBox combohuaklienti;
        private System.Windows.Forms.ComboBox combohuamateriali;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}