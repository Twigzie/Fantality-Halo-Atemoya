using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Atemoya.Classes.Helpers.Security {

    public static class Encryption {

        private readonly static byte[] ID;
        private readonly static string KEY = "Atemoya";

        static Encryption() {
            ID = GetMachineBoundKey();
        }

        private static string GetWmiValue(string wmiClass, string wmiProperty) {
            try {

                var source = $"SELECT {wmiProperty} FROM {wmiClass}";
                using (var searcher = new ManagementObjectSearcher(source)) {
                    foreach (var value in searcher.Get().Cast<ManagementObject>()) {
                        return value[wmiProperty]?.ToString() ?? "";
                    }
                }

                return "";

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        private static byte[] GetMachineBoundKey() {

            try {
                var source = $"{GetWmiValue("Win32_Processor", "ProcessorId")}-{GetWmiValue("Win32_BIOS", "SerialNumber")}-{GetWmiValue("Win32_BaseBoard", "SerialNumber")}";
                using (var SHA = SHA256.Create()) {
                    var keyDeriver = new Rfc2898DeriveBytes(KEY, Encoding.UTF8.GetBytes(BitConverter.ToString(SHA.ComputeHash(Encoding.UTF8.GetBytes(source))).Replace("-", "")), 100000);
                    return keyDeriver.GetBytes(32); // AES-256 key
                }
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public static string EncryptString(string source) {

            try {

                if (ID == null)
                    throw new Exception("State not initialized");

                using (var aes = Aes.Create()) {

                    aes.Key = ID;
                    aes.GenerateIV();
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var encryptor = aes.CreateEncryptor())
                    using (var ms = new MemoryStream()) {

                        ms.Write(aes.IV, 0, aes.IV.Length);

                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var sw = new StreamWriter(cs))
                            sw.Write(source);

                        return Convert.ToBase64String(ms.ToArray());

                    }

                }

            }
            catch (Exception ex) {
                throw ex;
            }

        }
        public static string EncryptFromSecureString(SecureString source) {

            try {

                var unmanagedString = IntPtr.Zero;
                try {
                    unmanagedString = Marshal.SecureStringToBSTR(source);
                    return EncryptString(Marshal.PtrToStringBSTR(unmanagedString));
                }
                finally {
                    if (unmanagedString != IntPtr.Zero)
                        Marshal.ZeroFreeBSTR(unmanagedString);
                }

            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public static string DecryptString(string source) {

            try {

                if (ID == null)
                    throw new Exception("State not initialized");

                byte[] fullCipher = Convert.FromBase64String(source);

                byte[] iv = new byte[16];
                byte[] cipherText = new byte[fullCipher.Length - 16];
                Array.Copy(fullCipher, 0, iv, 0, 16);
                Array.Copy(fullCipher, 16, cipherText, 0, cipherText.Length);

                using (Aes aes = Aes.Create()) {

                    aes.Key = ID;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var ms = new MemoryStream(cipherText))
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                        return sr.ReadToEnd();

                }
            }
            catch (Exception ex) {
                throw ex;
            }

        }
        public static SecureString DecryptToSecureString(string source) {
            try {

                var result = new SecureString();
                foreach (char c in DecryptString(source))
                    result.AppendChar(c);
                result.MakeReadOnly();
                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}