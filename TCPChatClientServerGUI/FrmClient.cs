using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using OpenHardwareMonitor.Hardware;
using System.IO;
using System.Security.Cryptography;

namespace TCPChatClientServerGUI
{
    public partial class FrmClient : Form
    {
        public FrmClient()
        {
            InitializeComponent();
        }

        Socket sckClient;

        private void butKetnoi_Click(object sender, EventArgs e)
        {
            // Tạo socket
            sckClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Kết nối
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(txtServerIP.Text), (int)numServerPort.Value);
            sckClient.BeginConnect(ep, new AsyncCallback(xulyketnoi), null);
        }

        void xulyketnoi(IAsyncResult result)
        {
            sckClient.EndConnect(result);
            // Cập nhật trạng thái và bắt đầu gửi nhận dữ liệu
            lbTrangThai.Invoke(new CapNhatGiaoDien(CapNhatTrangThai), new object[] { "Kết nối thành công." });
            // Bắt đầu nhận dữ liệu
            sckClient.BeginReceive(data, 0, 1024, SocketFlags.None, new AsyncCallback(xulydulieunhanduoc), null);
        }

        // Khai báo bộ đệm để nhận dữ liệu
        byte[] data = new byte[1024];

        void xulydulieunhanduoc(IAsyncResult result)
        {
            // Gọi hàm EndReceive
            int size = sckClient.EndReceive(result);
            // Xử lý dữ liệu nhận được trong data[]
            string thongdiep = Encoding.UTF8.GetString(data, 0, size); // Sử dụng Encoding.UTF8

            // Chèn thông điệp vào textbox nội dung chat
            string decryptedMessage = Decrypt(thongdiep, "16112004");
            txtNoidungChat.Invoke(new CapNhatGiaoDien(CapNhatNoiDungChat), new object[] { "Server: " + decryptedMessage });

            // Chờ nhận tiếp
            sckClient.BeginReceive(data, 0, 1024, SocketFlags.None, new AsyncCallback(xulydulieunhanduoc), null);
        }

        private void butGui_Click(object sender, EventArgs e)
        {
            // Sử dụng Encoding.UTF8 khi gửi tin nhắn để đảm bảo tiếng Việt
            string encryptedMessage = Encrypt(txtThongdiep.Text, "111000111");
            sckClient.Send(Encoding.UTF8.GetBytes(encryptedMessage));
            CapNhatNoiDungChat("Client: " + txtThongdiep.Text);
            txtThongdiep.Text = "";
        }


        private string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Tạo khóa và IV từ chuỗi key
                using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes("SaltValueHere"), 10000))
                {
                    aesAlg.Key = keyDerivationFunction.GetBytes(32); // 256 bit key
                    aesAlg.IV = keyDerivationFunction.GetBytes(16);  // 128 bit IV
                }

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }


        private string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Tạo khóa và IV từ chuỗi key
                using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes("SaltValueHere"), 10000))
                {
                    aesAlg.Key = keyDerivationFunction.GetBytes(32); // 256 bit key
                    aesAlg.IV = keyDerivationFunction.GetBytes(16);  // 128 bit IV
                }

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        byte[] encrypted = msEncrypt.ToArray();
                        return Convert.ToBase64String(encrypted);
                    }
                }
            }
        }


        delegate void CapNhatGiaoDien(string s);
        void CapNhatTrangThai(string s)
        {
            lbTrangThai.Text = s;
        }
        void CapNhatNoiDungChat(string s)
        {
            txtNoidungChat.Text += s + "\r\n";
        }

        private void txtThongdiep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                // Gọi lại hàm gửi
                butGui_Click(null, null);
            }
        }
    }
}
