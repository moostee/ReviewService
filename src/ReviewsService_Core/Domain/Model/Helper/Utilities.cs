using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ReviewsService_Core.Domain.Model.Helper
{
    public class Utilities
    {
        

        public static ResponseModel InitializeResponse()
        {
            ResponseModel response = new ResponseModel();
            string requestId = String.Format("{0}_{1:N}", "", Guid.NewGuid());
            response.RequestId = requestId;
            response.ResponseCode = "00";
            response.ResponseMessage = "Successful";
            return response;
        }

        public static ResponseModel CatchException(ResponseModel response)
        {
            response.ResponseCode = "99";
            response.ResponseMessage = "Error occurred while processing your request";
            return response;
        }

        public static ResponseModel UnsuccessfulResponse(ResponseModel response, string message, string data = null)
        {
            response.ResponseCode = "02";
            response.ResponseMessage = message;
            response.Data = data;
            return response;
        }

        public static string EncryptString(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            var IV = UTF8Encoding.UTF8.GetBytes("zT$3qIjUR$4rIj45");
            string encryptionKey = "b14ca5898a4e4133bbce2ea2315a1910";

            keyArray = UTF8Encoding.UTF8.GetBytes(encryptionKey);

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = keyArray;
            //aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.IV = IV;
            aes.Padding = PaddingMode.PKCS7;

            aes.Mode = CipherMode.CBC;
            var transform = aes.CreateEncryptor();
            var outputArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            aes.Clear();
            return Convert.ToBase64String(outputArray, 0, outputArray.Length);

        }

        public static string DecryptString(string toDecrypt)
        {
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            byte[] IV = UTF8Encoding.UTF8.GetBytes("zT$3qIjUR$4rIj45");
            string decryptionKey = "b14ca5898a4e4133bbce2ea2315a1910";

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(decryptionKey);

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = keyArray;
            //aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.IV = IV;
            aes.Padding = PaddingMode.PKCS7;

            aes.Mode = CipherMode.CBC;
            var transform = aes.CreateDecryptor();
            var outputArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            aes.Clear();
            return UTF8Encoding.UTF8.GetString(outputArray);
        }

        public static string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
    }
}
