namespace Repair
{
    partial class Request
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
            this.button_List_request = new System.Windows.Forms.Button();
            this.descriptionText = new System.Windows.Forms.TextBox();
            this.severityBox = new System.Windows.Forms.ComboBox();
            this.equipmentTypeBox = new System.Windows.Forms.ComboBox();
            this.emailText = new System.Windows.Forms.TextBox();
            this.fullNameText = new System.Windows.Forms.TextBox();
            this.createRequest = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_List_request
            // 
            this.button_List_request.Location = new System.Drawing.Point(419, 228);
            this.button_List_request.Name = "button_List_request";
            this.button_List_request.Size = new System.Drawing.Size(101, 37);
            this.button_List_request.TabIndex = 24;
            this.button_List_request.Text = "Список заявок";
            this.button_List_request.UseVisualStyleBackColor = true;
            this.button_List_request.Visible = false;
            this.button_List_request.Click += new System.EventHandler(this.button_List_request_Click);
            // 
            // descriptionText
            // 
            this.descriptionText.Location = new System.Drawing.Point(12, 115);
            this.descriptionText.Multiline = true;
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.Size = new System.Drawing.Size(508, 110);
            this.descriptionText.TabIndex = 23;
            // 
            // severityBox
            // 
            this.severityBox.FormattingEnabled = true;
            this.severityBox.Location = new System.Drawing.Point(248, 64);
            this.severityBox.Name = "severityBox";
            this.severityBox.Size = new System.Drawing.Size(145, 21);
            this.severityBox.TabIndex = 22;
            // 
            // equipmentTypeBox
            // 
            this.equipmentTypeBox.FormattingEnabled = true;
            this.equipmentTypeBox.Location = new System.Drawing.Point(12, 64);
            this.equipmentTypeBox.Name = "equipmentTypeBox";
            this.equipmentTypeBox.Size = new System.Drawing.Size(121, 21);
            this.equipmentTypeBox.TabIndex = 21;
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(248, 25);
            this.emailText.Multiline = true;
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(272, 20);
            this.emailText.TabIndex = 20;
            // 
            // fullNameText
            // 
            this.fullNameText.Location = new System.Drawing.Point(12, 25);
            this.fullNameText.Multiline = true;
            this.fullNameText.Name = "fullNameText";
            this.fullNameText.Size = new System.Drawing.Size(219, 20);
            this.fullNameText.TabIndex = 19;
            // 
            // createRequest
            // 
            this.createRequest.Location = new System.Drawing.Point(201, 231);
            this.createRequest.Name = "createRequest";
            this.createRequest.Size = new System.Drawing.Size(113, 30);
            this.createRequest.TabIndex = 18;
            this.createRequest.Text = "Создать заявку";
            this.createRequest.UseVisualStyleBackColor = true;
            this.createRequest.Click += new System.EventHandler(this.createRequest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Описание проблемы";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Серьёзность проблемы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Тип оборудования";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Почта для связи:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Ф.И.О заказчика:";
            // 
            // Request
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 279);
            this.Controls.Add(this.button_List_request);
            this.Controls.Add(this.descriptionText);
            this.Controls.Add(this.severityBox);
            this.Controls.Add(this.equipmentTypeBox);
            this.Controls.Add(this.emailText);
            this.Controls.Add(this.fullNameText);
            this.Controls.Add(this.createRequest);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Request";
            this.Text = "Заявка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_List_request;
        private System.Windows.Forms.TextBox descriptionText;
        private System.Windows.Forms.ComboBox severityBox;
        private System.Windows.Forms.ComboBox equipmentTypeBox;
        private System.Windows.Forms.TextBox emailText;
        private System.Windows.Forms.TextBox fullNameText;
        private System.Windows.Forms.Button createRequest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}