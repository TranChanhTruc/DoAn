using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigIntCalculator
{
    
    public class DanhSachDac
    {
        private int[] ds;      
        private int n;         
        private const int MAX = 1000;  
        private bool soam=false;
     
        public DanhSachDac()
        {
            ds = new int[MAX];
            n = 0;
            soam = false;
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


        public int SoLuongChuSo => n;

        public bool LaRong => n == 0;

        public void NhapTuChuoi(string chuoi)
        {
            n = 0;
            soam = false;
            if (string.IsNullOrWhiteSpace(chuoi))
            {
                ds[0] = 0;
                n = 1;
                return;
            }
            chuoi = chuoi.Trim();
            if (chuoi[0] == '-')
            {
                soam = true;
                chuoi = chuoi.Substring(1);
            }
            else if (chuoi[0] == '+')
            {
                chuoi = chuoi.Substring(1);
            }
            for (int i = chuoi.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(chuoi[i]))
                {
                    ds[n] = chuoi[i] - '0';
                    n++;
                    if (n >= MAX) break;
                }
            }
            if (n == 0)
            {
                ds[0] = 0;
                n = 1;
                soam = false;
            }
            XoaSo0DauTien();
            if (n == 1 && ds[0] == 0) soam = false;
        }
        public void NhapTuSo(long so)
        {
            n = 0;
            soam = so < 0;
            if (so < 0) so = -so;
            if (so == 0)
            {
                ds[0] = 0;
                n = 1;
                soam = false;
                return;
            }
            while (so > 0)
            {
                ds[n] = (int)(so % 10);
                n++;
                so /= 10;
            }
        }
        private void XoaSo0DauTien()
        {
            while (n > 1 && ds[n - 1] == 0)
            {
                n--;
            }
            if (n == 0)
            {
                ds[0] = 0;
                n = 1;
            }
        }

        public override string ToString()
        {
            if (n == 0) return "0";
            StringBuilder sb = new StringBuilder();
            if (soam) sb.Append('-');
            for (int i = n - 1; i >= 0; i--)
                sb.Append(ds[i]);
            return sb.ToString();
        }

        public string ToStringFormatted()
        {
            string chuoi = ToString();
            bool am = chuoi.StartsWith("-");
            if (am)
                chuoi = chuoi.Substring(1); // bỏ dấu âm để format
            string ketQua = "";
            int dem = 0;
            // Format 3 số 1 nhóm
            for (int i = chuoi.Length - 1; i >= 0; i--)
            {
                if (dem > 0 && dem % 3 == 0)
                {
                    ketQua = "," + ketQua;
                }
                ketQua = chuoi[i] + ketQua;
                dem++;
            }
            // Thêm lại dấu âm nếu cần
            if (am)
                ketQua = "-" + ketQua;
            return ketQua;
        }

        public static DanhSachDac Cong(DanhSachDac a, DanhSachDac b)
        {
            if (a.soam == b.soam)
            {
                DanhSachDac kq = CongTuyetDoi(a, b);
                kq.soam = a.soam;
                return kq;
            }
            int ss = SoSanh(a, b);
            if (ss == 0)
            {
                return new DanhSachDac("0");
            }
            if (ss > 0)
            {
                DanhSachDac kq = TruTuyetDoi(a, b);
                kq.soam = a.soam;
                return kq;
            }
            else
            {
                DanhSachDac kq = TruTuyetDoi(b, a);
                kq.soam = b.soam;
                return kq;
            }
        }
        public static DanhSachDac CongTuyetDoi(DanhSachDac a, DanhSachDac b)
        {
            DanhSachDac ketQua = new DanhSachDac();
            int nho = 0;
            int maxN = Math.Max(a.n, b.n); 
            System.Diagnostics.Debug.WriteLine($"Cộng: a.n={a.n}, b.n={b.n}, maxN={maxN}");
            for (int i = 0; i < maxN || nho > 0; i++)
            {
                int tong = nho;
                if (i < a.n)
                    tong += a.ds[i];
                if (i < b.n)
                    tong += b.ds[i];
                ketQua.ds[ketQua.n] = tong % 10;
                ketQua.n++;
                nho = tong / 10;
                System.Diagnostics.Debug.WriteLine($"  i={i}, tong={tong}, ketQua.ds[{i}]={ketQua.ds[i]}, nho={nho}");
            }
            ketQua.XoaSo0DauTien();
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
            for (int i = 0; i < MAX; i++)
            {
                ketQua.ds[i] = 0;
            }
            ketQua.n = a.n + b.n;
            System.Diagnostics.Debug.WriteLine($"Nhân: a.n={a.n}, b.n={b.n}");
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
            ketQua.soam = a.soam ^ b.soam;
            ketQua.XoaSo0DauTien();
            System.Diagnostics.Debug.WriteLine($"Kết quả nhân: n={ketQua.n}, chuỗi={ketQua.ToString()}");
            return ketQua;
        }
        public static int SoSanh(DanhSachDac a, DanhSachDac b)
        {
            if (a.n > b.n) return 1;
            if (a.n < b.n) return -1;
            for (int i = a.n - 1; i >= 0; i--)
            {
                if (a.ds[i] > b.ds[i]) return 1;
                if (a.ds[i] < b.ds[i]) return -1;
            }
            return 0;
        }
        public static DanhSachDac TruTuyetDoi(DanhSachDac a, DanhSachDac b)
        {
            DanhSachDac ketQua = new DanhSachDac();
            int muon = 0;
            for (int i = 0; i < a.n; i++)
            {
                int chuSoA = a.ds[i];
                int chuSoB = (i < b.n) ? b.ds[i] : 0;
                int hieu = chuSoA - chuSoB - muon;
                if (hieu < 0)
                {
                    hieu += 10;
                    muon = 1;
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
        public static DanhSachDac Tru(DanhSachDac a, DanhSachDac b)
        {
            DanhSachDac kq = new DanhSachDac();
            if (!a.soam && b.soam)
            {
                kq = CongTuyetDoi(a, b);
                kq.soam = false;
                return kq;
            }
            if (a.soam && !b.soam)
            {
                kq = CongTuyetDoi(a, b);
                kq.soam = true;
                return kq;
            }
            int ss = SoSanh(a, b);

            if (ss == 0)
            {
                return new DanhSachDac("0");
            }
            if (ss > 0)
            {
                kq = TruTuyetDoi(a, b);
                kq.soam = a.soam;
            }
            else
            {
                kq = TruTuyetDoi(b, a);
                kq.soam = !a.soam;
            }
            return kq;
        }
        //Toán tử -
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
            chuoi = chuoi.Trim();
            if (chuoi.Length > 0 && (chuoi[0] == '+' || chuoi[0] == '-'))
                chuoi = chuoi.Substring(1);
            return chuoi.Length > 0 && chuoi.All(c => char.IsDigit(c));
        }
        public static DanhSachDac Chia(DanhSachDac a, DanhSachDac b)
        {
            if (b.n == 1 && b.ds[0] == 0)
                throw new DivideByZeroException("Không thể chia cho 0!");
            bool ketQuaAm = a.soam ^ b.soam;
            DanhSachDac aa = new DanhSachDac(a.ToString());
            aa.soam = false;
            DanhSachDac bb = new DanhSachDac(b.ToString());
            bb.soam = false;
            if (SoSanh(aa, bb) < 0)
                return new DanhSachDac("0");
            if (SoSanh(aa, bb) == 0)
            {
                DanhSachDac kq = new DanhSachDac("1");
                kq.soam = ketQuaAm;
                return kq;
            }
            DanhSachDac thuong = new DanhSachDac("0");
            DanhSachDac mot = new DanhSachDac("1");
            DanhSachDac du = new DanhSachDac(aa.ToString());
            while (SoSanh(du, bb) >= 0)
            {
                du = Tru(du, bb);
                thuong = Cong(thuong, mot);
            }
            thuong.soam = ketQuaAm;
            return thuong;
        }

        public static DanhSachDac operator /(DanhSachDac a, DanhSachDac b)
        {
            return Chia(a, b);
        }
        public static DanhSachDac ChiaLayDu(DanhSachDac a, DanhSachDac b)
        {
            if (b.n == 1 && b.ds[0] == 0)
            {
                throw new DivideByZeroException("Không thể chia cho 0!");
            }
            if (SoSanh(a, b) < 0)
            {
                return new DanhSachDac(a.ToString());
            }
            if (SoSanh(a, b) == 0)
            {
                return new DanhSachDac("0");
            }
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