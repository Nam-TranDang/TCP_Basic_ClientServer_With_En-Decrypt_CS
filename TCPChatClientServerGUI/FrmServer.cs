using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using OpenHardwareMonitor.Hardware;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography;

namespace TCPChatClientServerGUI
{
    public partial class FrmServer : Form
    {
        public FrmServer()
        {
            InitializeComponent();
        }

        //Khai bao 2 sockets
        Socket sckServer, sckClient;

        private void butKhoitao_Click(object sender, EventArgs e)
        {
            //Tao socket
            sckServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Bind, Listen
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, (int)numServerPort.Value);
            sckServer.Bind(ep);
            sckServer.Listen(5);
            //Accept bat dong bo
            sckServer.BeginAccept(new AsyncCallback(xulyketnoi), null);
            lbTrangThai.Text = "Đang chờ kết nối ....";
        }

        void xulyketnoi(IAsyncResult result)
        {
            sckClient = sckServer.EndAccept(result);
            //Cập nhật trạng thái
            lbTrangThai.Invoke(new CapNhatGiaoDien(CapNhatTrangThai), new object[] { "Kết nối thành công." });
            //Bắt đầu nhận dữ liệu
            sckClient.BeginReceive(data, 0, 1024, SocketFlags.None, new AsyncCallback(xulydulieunhanduoc), null);
        }

        //Khai bao bo dem de nhan du lieu
        byte[] data = new byte[1024];

        async void xulydulieunhanduoc(IAsyncResult result)
        {
            //Goi ham EndReceive
            int size = sckClient.EndReceive(result);
            //Xu ly du lieu nhan duoc trong data[]
            string thongdiep = Encoding.UTF8.GetString(data, 0, size).Trim();


            //Chèn thông điệp vào textbox nội dung chat
            string decryptedMessage = Decrypt(thongdiep, "111000111");
            txtNoidungChat.Invoke(new CapNhatGiaoDien(CapNhatNoiDungChat), new object[] { "Cilent: " + decryptedMessage });

            //Cho nhận tiếp
            sckClient.BeginReceive(data, 0, 1024, SocketFlags.None, new AsyncCallback(xulydulieunhanduoc), null);
        }

        private void butGui_Click(object sender, EventArgs e)
        {
            // Sử dụng Encoding.UTF8 để gửi tin nhắn
            string encryptedMessage = Encrypt(txtThongdiep.Text, "16112004");
            sckClient.Send(Encoding.UTF8.GetBytes(encryptedMessage));
            CapNhatNoiDungChat("Server: " + txtThongdiep.Text);
            txtThongdiep.Text = "";
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
