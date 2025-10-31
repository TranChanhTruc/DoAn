using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigIntCalculator
{
    /// Class xử lý số nguyên dài sử dụng mảng (danh sách đặc)
    public class DanhSachDac
    {
        private int[] ds;      // Mảng lưu các chữ số
        private int n;         // Số lượng chữ số hiện tại
        private const int MAX = 1000;  // Số chữ số tối đa

        /// Khởi tạo danh sách rỗng
        public DanhSachDac()
        {
            ds = new int[MAX];
            n = 0;
        }

        /// Khởi tạo từ chuỗi số
        /// VD: "12345" -> ds[0]=5, ds[1]=4, ds[2]=3, ds[3]=2, ds[4]=1, n=5
        public DanhSachDac(string chuoi)
        {
            ds = new int[MAX];
            n = 0;
            NhapTuChuoi(chuoi);
        }

        /// Khởi tạo từ số nguyên
        public DanhSachDac(long so)
        {
            ds = new int[MAX];
            n = 0;
            NhapTuSo(so);
        }

        /// Số lượng chữ số
        public int SoLuongChuSo => n;

        /// Kiểm tra có rỗng không
        public bool LaRong => n == 0;

        /// Nhập số từ chuỗi
        /// Đọc chuỗi từ PHẢI sang TRÁI để lưu vào mảng
        public void NhapTuChuoi(string chuoi)
        {
            n = 0; // Reset số lượng chữ số

            // Kiểm tra chuỗi rỗng
            if (string.IsNullOrWhiteSpace(chuoi))
            {
                ds[0] = 0;
                n = 1;
                return;
            }

            // Đọc chuỗi từ PHẢI sang TRÁI
            for (int i = chuoi.Length - 1; i >= 0; i--)
            {
                // Chỉ lấy ký tự số
                if (char.IsDigit(chuoi[i]))
                {
                    ds[n] = chuoi[i] - '0'; // Chuyển ký tự thành số
                    n++;

                    // Kiểm tra vượt quá giới hạn
                    if (n >= MAX) break;
                }
            }

            // Nếu không có chữ số hợp lệ -> đặt = 0
            if (n == 0)
            {
                ds[0] = 0;
                n = 1;
            }

            // Xóa số 0 thừa ở đầu
            XoaSo0DauTien();
        }

        /// Nhập số từ kiểu long
        public void NhapTuSo(long so)
        {
            n = 0;

            // Xử lý số âm
            if (so < 0) so = -so;

            // Trường hợp số 0
            if (so == 0)
            {
                ds[0] = 0;
                n = 1;
                return;
            }

            // Tách từng chữ số
            while (so > 0)
            {
                ds[n] = (int)(so % 10);
                n++;
                so /= 10;
            }
        }

        /// Xóa các số 0 thừa ở cuối mảng (hàng cao nhất)
        private void XoaSo0DauTien()
        {
            // Giảm n khi gặp số 0 ở cuối
            while (n > 1 && ds[n - 1] == 0)
            {
                n--;
            }

            // Trường hợp đặc biệt: mảng rỗng
            if (n == 0)
            {
                ds[0] = 0;
                n = 1;
            }
        }

        public override string ToString()
        {
            if (n == 0) return "0";

            // Đọc từ ds[n-1] về ds[0]
            string ketQua = "";
            for (int i = n - 1; i >= 0; i--)
            {
                ketQua += ds[i].ToString();
            }

            return ketQua;
        }

        public string ToStringFormatted()
        {
            string chuoi = ToString();
            string ketQua = "";
            int dem = 0;

            // Đọc từ phải sang trái và thêm dấu phẩy sau mỗi 3 chữ số
            for (int i = chuoi.Length - 1; i >= 0; i--)
            {
                if (dem > 0 && dem % 3 == 0)
                {
                    ketQua = "," + ketQua;
                }
                ketQua = chuoi[i] + ketQua;
                dem++;
            }

            return ketQua;
        }

       
        public static DanhSachDac Cong(DanhSachDac a, DanhSachDac b)
        {
            // Tạo đối tượng kết quả mới
            DanhSachDac ketQua = new DanhSachDac();

            int nho = 0; // Số nhớ
            int maxN = Math.Max(a.n, b.n);

            // Debug: In ra để kiểm tra
            System.Diagnostics.Debug.WriteLine($"Cộng: a.n={a.n}, b.n={b.n}, maxN={maxN}");

            // Vòng lặp cộng từng cặp chữ số
            for (int i = 0; i < maxN || nho > 0; i++)
            {
                int tong = nho;

                // Lấy chữ số từ a
                if (i < a.n)
                    tong += a.ds[i];

                // Lấy chữ số từ b
                if (i < b.n)
                    tong += b.ds[i];

                // Lưu kết quả
                ketQua.ds[ketQua.n] = tong % 10;
                ketQua.n++;

                // Tính số nhớ
                nho = tong / 10;

                // Debug
                System.Diagnostics.Debug.WriteLine($"  i={i}, tong={tong}, ketQua.ds[{i}]={ketQua.ds[i]}, nho={nho}");
            }

            // Xóa số 0 thừa
            ketQua.XoaSo0DauTien();

            // Debug kết quả
            System.Diagnostics.Debug.WriteLine($"Kết quả cộng: n={ketQua.n}, chuỗi={ketQua.ToString()}");

            return ketQua;
        }
        public static DanhSachDac operator +(DanhSachDac a, DanhSachDac b)
        {
            return Cong(a, b);
        }

        public static DanhSachDac Nhan(DanhSachDac a, DanhSachDac b)
        {
            DanhSachDac ketQua = new DanhSachDac();

            // Khởi tạo tất cả phần tử = 0
            for (int i = 0; i < MAX; i++)
            {
                ketQua.ds[i] = 0;
            }

            // Đặt số lượng chữ số tối đa
            ketQua.n = a.n + b.n;

            // Debug
            System.Diagnostics.Debug.WriteLine($"Nhân: a.n={a.n}, b.n={b.n}");

            // Nhân từng chữ số
            for (int i = 0; i < a.n; i++)
            {
                int nho = 0;

                for (int j = 0; j < b.n || nho > 0; j++)
                {
                    int chuSoB = (j < b.n) ? b.ds[j] : 0;
                    int tich = ketQua.ds[i + j] + a.ds[i] * chuSoB + nho;
                    ketQua.ds[i + j] = tich % 10;
                    nho = tich / 10;
                }
            }

            // Xóa số 0 thừa
            ketQua.XoaSo0DauTien();

            // Debug
            System.Diagnostics.Debug.WriteLine($"Kết quả nhân: n={ketQua.n}, chuỗi={ketQua.ToString()}");

            return ketQua;
        }
        public static int SoSanh(DanhSachDac a, DanhSachDac b)
        {
            // So sánh số chữ số
            if (a.n > b.n) return 1;
            if (a.n < b.n) return -1;

            // Cùng số chữ số → so sánh từ trái sang phải
            for (int i = a.n - 1; i >= 0; i--)
            {
                if (a.ds[i] > b.ds[i]) return 1;
                if (a.ds[i] < b.ds[i]) return -1;
            }

            return 0; // Bằng nhau
        }
        public static DanhSachDac Tru(DanhSachDac a, DanhSachDac b)
        {
            // Kiểm tra a >= b
            if (SoSanh(a, b) < 0)
            {
                throw new Exception("Không thể trừ: Số bị trừ phải lớn hơn hoặc bằng số trừ!");
            }

            DanhSachDac ketQua = new DanhSachDac();
            int muon = 0; // Số mượn

            // Trừ từng cặp chữ số
            for (int i = 0; i < a.n; i++)
            {
                int chuSoA = a.ds[i];
                int chuSoB = (i < b.n) ? b.ds[i] : 0;

                // Xử lý số mượn
                int hieu = chuSoA - chuSoB - muon;

                if (hieu < 0)
                {
                    hieu += 10; // Mượn 10
                    muon = 1;   // Đánh dấu đã mượn
                }
                else
                {
                    muon = 0;
                }

                ketQua.ds[ketQua.n] = hieu;
                ketQua.n++;
            }

            ketQua.XoaSo0DauTien();
            return ketQua;
        }

        public static DanhSachDac operator -(DanhSachDac a, DanhSachDac b)
        {
            return Tru(a, b);
        }

        /// Toán tử *
        public static DanhSachDac operator *(DanhSachDac a, DanhSachDac b)
        {
            return Nhan(a, b);
        }

        /// Kiểm tra tính hợp lệ của chuỗi số
        public static bool LaGiaTriHopLe(string chuoi)
        {
            if (string.IsNullOrWhiteSpace(chuoi))
                return false;

            return chuoi.All(c => char.IsDigit(c));
        }
        public static DanhSachDac Chia(DanhSachDac a, DanhSachDac b)
        {
            // Kiểm tra chia cho 0
            if (b.n == 1 && b.ds[0] == 0)
            {
                throw new DivideByZeroException("Không thể chia cho 0!");
            }

            // Nếu a < b → kết quả = 0
            if (SoSanh(a, b) < 0)
            {
                return new DanhSachDac("0");
            }

            // Nếu a = b → kết quả = 1
            if (SoSanh(a, b) == 0)
            {
                return new DanhSachDac("1");
            }

            // Thuật toán chia đơn giản: Trừ dần
            // LƯU Ý: Chỉ phù hợp với số không quá lớn
            // Với số rất lớn cần thuật toán phức tạp hơn

            DanhSachDac thuong = new DanhSachDac("0");
            DanhSachDac mot = new DanhSachDac("1");
            DanhSachDac du = new DanhSachDac(a.ToString());

            // Đếm số lần trừ được
            while (SoSanh(du, b) >= 0)
            {
                du = Tru(du, b);
                thuong = Cong(thuong, mot);
            }

            return thuong;
        }

        public static DanhSachDac operator /(DanhSachDac a, DanhSachDac b)
        {
            return Chia(a, b);
        }
        public static DanhSachDac ChiaLayDu(DanhSachDac a, DanhSachDac b)
        {
            // Kiểm tra chia cho 0
            if (b.n == 1 && b.ds[0] == 0)
            {
                throw new DivideByZeroException("Không thể chia cho 0!");
            }

            // Nếu a < b → số dư = a
            if (SoSanh(a, b) < 0)
            {
                return new DanhSachDac(a.ToString());
            }

            // Nếu a = b → số dư = 0
            if (SoSanh(a, b) == 0)
            {
                return new DanhSachDac("0");
            }

            // Tính: du = a - (a / b) * b
            DanhSachDac thuong = Chia(a, b);
            DanhSachDac tich = Nhan(thuong, b);
            DanhSachDac du = Tru(a, tich);

            return du;
        }

        public static DanhSachDac operator %(DanhSachDac a, DanhSachDac b)
        {
            return ChiaLayDu(a, b);
        }

    }
}