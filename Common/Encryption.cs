using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Common
{
    public class Encryption
    {
        /// <summary>
        /// Initialises Encryption Properties
        /// Level: Common
        /// </summary>
        public Encryption()
        {
            _Key = "TGSTestKey";
            _MD5CSP = new MD5CryptoServiceProvider();
            _TDESCSP = new TripleDESCryptoServiceProvider();
            _UTF8Encoder = new UTF8Encoding();
        }

        private string _Key;

        private MD5CryptoServiceProvider _MD5CSP;

        private UTF8Encoding _UTF8Encoder;

        private TripleDESCryptoServiceProvider _TDESCSP;

        /// <summary>
        /// Encrypts a String
        /// Level: Common
        /// </summary>
        /// <param name="toEncrypt">String to Encrypt</param>
        /// <returns>Encrypted String</returns>
        public string Encrypt(string toEncrypt)
        {
            try
            {
                string EncryptedString = null;

                if ((toEncrypt != null) && (_Key != null) && (_MD5CSP != null) && (_TDESCSP != null) && (_UTF8Encoder != null))
                {
                    if ((toEncrypt.Trim().Length > 0) && (_Key.Trim().Length > 0))
                    {
                        _TDESCSP.Key = _MD5CSP.ComputeHash(_UTF8Encoder.GetBytes(_Key));
                        _TDESCSP.Mode = CipherMode.ECB;
                        _TDESCSP.Padding = PaddingMode.PKCS7;
                        ICryptoTransform ICTransform = _TDESCSP.CreateEncryptor();

                        EncryptedString = Convert.ToBase64String(ICTransform.TransformFinalBlock(_UTF8Encoder.GetBytes(toEncrypt), 0, _UTF8Encoder.GetBytes(toEncrypt).Length));

                        return EncryptedString.Replace('+', '_');
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Decrypts an Encrypted String
        /// Level: Common
        /// </summary>
        /// <param name="toDecrypt">Encrypted String</param>
        /// <returns>Decrypted String</returns>
        public string Decrypt(string toDecrypt)
        {
            try
            {
                toDecrypt = toDecrypt.Replace('_', '+');

                string DecryptedString = null;

                if ((toDecrypt != null) && (_Key != null) && (_MD5CSP != null) && (_TDESCSP != null) && (_UTF8Encoder != null))
                {
                    if ((toDecrypt.Trim().Length > 0) && (_Key.Trim().Length > 0))
                    {
                        byte[] toDecryptBytes = Convert.FromBase64String(toDecrypt);

                        _TDESCSP.Key = _MD5CSP.ComputeHash(_UTF8Encoder.GetBytes(_Key));
                        _TDESCSP.Mode = CipherMode.ECB;
                        _TDESCSP.Padding = PaddingMode.PKCS7;

                        ICryptoTransform ICTransform = _TDESCSP.CreateDecryptor();
                        DecryptedString = _UTF8Encoder.GetString(ICTransform.TransformFinalBlock(toDecryptBytes, 0, toDecryptBytes.Length));

                        return DecryptedString;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}