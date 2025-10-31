using BigIntCalculator;
using System;
using System.Windows.Forms;

namespace BigIntCalculator
{
    public class FormMain : Form
    {
        // === Khai báo các control ===
        private Label labelA;
        private TextBox txtA;
        private Label labelB;
        private TextBox txtB;
        private Button btnCong;
        private Button btnTru;
        private Button btnNhan;
        private Button btnChia;
        private Label labelKQ;
        private TextBox txtKetQua;

        public FormMain()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.labelA = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.labelB = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.btnCong = new System.Windows.Forms.Button();
            this.btnTru = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnChia = new System.Windows.Forms.Button();
            this.labelKQ = new System.Windows.Forms.Label();
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelA
            // 
            this.labelA.Location = new System.Drawing.Point(21, 18);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(50, 23);
            this.labelA.TabIndex = 0;
            this.labelA.Text = "Số A:";
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(77, 18);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(370, 22);
            this.txtA.TabIndex = 1;
            // 
            // labelB
            // 
            this.labelB.Location = new System.Drawing.Point(21, 60);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(50, 23);
            this.labelB.TabIndex = 2;
            this.labelB.Text = "Số B:";
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(77, 57);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(370, 22);
            this.txtB.TabIndex = 3;
            // 
            // btnCong
            // 
            this.btnCong.Location = new System.Drawing.Point(57, 100);
            this.btnCong.Name = "btnCong";
            this.btnCong.Size = new System.Drawing.Size(150, 23);
            this.btnCong.TabIndex = 4;
            this.btnCong.Text = "Cộng (A + B)";
            this.btnCong.Click += new System.EventHandler(this.BtnCong_Click);
            // 
            // btnTru
            // 
            this.btnTru.Location = new System.Drawing.Point(57, 136);
            this.btnTru.Name = "btnTru";
            this.btnTru.Size = new System.Drawing.Size(150, 23);
            this.btnTru.TabIndex = 5;
            this.btnTru.Text = "Tru (A × B)";
            this.btnTru.Click += new System.EventHandler(this.BtnTru_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.Location = new System.Drawing.Point(275, 100);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(150, 23);
            this.btnNhan.TabIndex = 5;
            this.btnNhan.Text = "Nhân (A × B)";
            this.btnNhan.Click += new System.EventHandler(this.BtnNhan_Click);
            // 
            // btnChia
            // 
            this.btnChia.Location = new System.Drawing.Point(275, 136);
            this.btnChia.Name = "btnChia";
            this.btnChia.Size = new System.Drawing.Size(150, 23);
            this.btnChia.TabIndex = 5;
            this.btnChia.Text = "Chia (A × B)";
            this.btnChia.Click += new System.EventHandler(this.BtnChia_Click);
            // 
            // labelKQ
            // 
            this.labelKQ.Location = new System.Drawing.Point(14, 171);
            this.labelKQ.Name = "labelKQ";
            this.labelKQ.Size = new System.Drawing.Size(80, 23);
            this.labelKQ.TabIndex = 6;
            this.labelKQ.Text = "Kết quả:";
            // 
            // txtKetQua
            // 
            this.txtKetQua.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtKetQua.Location = new System.Drawing.Point(24, 197);
            this.txtKetQua.Multiline = true;
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.ReadOnly = true;
            this.txtKetQua.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtKetQua.Size = new System.Drawing.Size(430, 150);
            this.txtKetQua.TabIndex = 7;
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(469, 365);
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.btnCong);
            this.Controls.Add(this.btnTru);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.btnChia);
            this.Controls.Add(this.labelKQ);
            this.Controls.Add(this.txtKetQua);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Máy tính số nguyên dài (Danh sách đặc)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // === Xử lý sự kiện ===
        private void BtnCong_Click(object sender, EventArgs e)
        {
            ThucHienPhepToan("cong");
        }

        private void BtnNhan_Click(object sender, EventArgs e)
        {
            ThucHienPhepToan("nhan");
        }
        private void BtnTru_Click(object sender, EventArgs e)
        {
            ThucHienPhepToan("tru");
        }
        private void BtnChia_Click(object sender, EventArgs e)
        {
            ThucHienPhepToan("chia");
        }

        private void ThucHienPhepToan(string phep)
        {
            try
            {
                string sA = txtA.Text.Trim();
                string sB = txtB.Text.Trim();

                // Kiểm tra input hợp lệ
                if (!DanhSachDac.LaGiaTriHopLe(sA) || !DanhSachDac.LaGiaTriHopLe(sB))
                {
                    MessageBox.Show("Vui lòng nhập hai số nguyên hợp lệ!", "Lỗi nhập liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng số
                DanhSachDac A = new DanhSachDac(sA);
                DanhSachDac B = new DanhSachDac(sB);
                DanhSachDac ketQua = null;
                string phepToanText = "";

                // Thực hiện phép toán
                switch (phep)
                {
                    case "cong":
                        ketQua = A + B;
                        phepToanText = "A + B";
                        break;

                    case "tru":
                        ketQua = A - B;
                        phepToanText = "A - B";
                        break;

                    case "nhan":
                        ketQua = A * B;
                        phepToanText = "A × B";
                        break;

                    case "chia":
                        ketQua = A / B;
                        DanhSachDac soDu = A % B;

                        // Hiển thị cả thương và số dư
                        txtKetQua.Text =
                            $"A = {A.ToStringFormatted()}\r\n" +
                            $"B = {B.ToStringFormatted()}\r\n\r\n" +
                            $"A ÷ B =\r\n" +
                            $"  Thương: {ketQua.ToStringFormatted()}\r\n" +
                            $"  Số dư: {soDu.ToStringFormatted()}";
                        return;
                }

                // Hiển thị kết quả (cho cộng, trừ, nhân)
                txtKetQua.Text =
                    $"A = {A.ToStringFormatted()}\r\n" +
                    $"B = {B.ToStringFormatted()}\r\n\r\n" +
                    $"{phepToanText} =\r\t{ketQua.ToStringFormatted()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
// viet bao cao gom 3 phan :
// 1. giới thiệu đề tài
// 2. trình bày cấu trúc danh sách đặc
// 3.giải thích code 