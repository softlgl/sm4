using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm4
{
    /// <summary>
    /// Sm4算法  
    /// 对标国际DES算法
    /// </summary>
    public class Sm4Crypto
    {
        public Sm4Crypto()
        {
            Key = "98145489617106616498";
            Iv = "0000000000000000";
            HexString = false;
            CryptoMode = Sm4CryptoEnum.ECB;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 向量
        /// </summary>
        public string Iv { get; set; }

        /// <summary>
        /// 明文是否是十六进制
        /// </summary>
        public bool HexString { get; set; }

        /// <summary>
        /// 加密模式(默认ECB)
        /// </summary>
        public Sm4CryptoEnum CryptoMode { get; set; }

        #region 加密

        public string Encrypt(Sm4Crypto entity)
        {
            return entity.CryptoMode == Sm4CryptoEnum.CBC ? EncryptCBC(entity) : EncryptECB(entity);
        }

        /// <summary>
        /// ECB加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string EncryptECB(Sm4Crypto entity)
        {
            Sm4Context ctx = new Sm4Context
            {
                IsPadding = true
            };

            if (entity.HexString)
            {
                entity.Key = GenerateKey(entity.Key);
            }

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.UTF8.GetBytes(entity.Key);

            SM4Utils sm4 = new SM4Utils();
            sm4.SetKeyEnc(ctx, keyBytes);
            byte[] encrypted = sm4.Sm4CryptEcb(ctx, Encoding.UTF8.GetBytes(entity.Data));

            return Encoding.UTF8.GetString(Hex.Encode(encrypted));
        }

        /// <summary>
        /// CBC加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string EncryptCBC(Sm4Crypto entity)
        {
            Sm4Context ctx = new Sm4Context
            {
                IsPadding = true
            };

            if (entity.HexString)
            {
                entity.Key = GenerateKey(entity.Key);
            }

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.UTF8.GetBytes(entity.Key);
            byte[] ivBytes = entity.HexString ? Hex.Decode(entity.Iv) : Encoding.UTF8.GetBytes(entity.Iv);

            SM4Utils sm4 = new SM4Utils();
            sm4.SetKeyEnc(ctx, keyBytes);
            byte[] encrypted = sm4.Sm4CryptCbc(ctx, ivBytes, Encoding.UTF8.GetBytes(entity.Data));

            return Convert.ToBase64String(encrypted);
        }

        #endregion

        #region 解密

        public object Decrypt(Sm4Crypto entity)
        {
            return entity.CryptoMode == Sm4CryptoEnum.CBC ? DecryptCBC(entity) : DecryptECB(entity);
        }

        /// <summary>
        ///  ECB解密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string DecryptECB(Sm4Crypto entity)
        {
            Sm4Context ctx = new Sm4Context
            {
                IsPadding = true,
                Mode = 0
            };

            if (entity.HexString)
            {
                entity.Key = GenerateKey(entity.Key);
            }

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.UTF8.GetBytes(entity.Key);

            SM4Utils sm4 = new SM4Utils();
            sm4.Sm4SetKeyDec(ctx, keyBytes);
            byte[] decrypted = sm4.Sm4CryptEcb(ctx, Hex.Decode(entity.Data));
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// CBC解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptCBC(Sm4Crypto entity)
        {
            Sm4Context ctx = new Sm4Context
            {
                IsPadding = true,
                Mode = 0
            };

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.UTF8.GetBytes(entity.Key);
            byte[] ivBytes = entity.HexString ? Hex.Decode(entity.Iv) : Encoding.UTF8.GetBytes(entity.Iv);

            SM4Utils sm4 = new SM4Utils();
            sm4.Sm4SetKeyDec(ctx, keyBytes);
            byte[] decrypted = sm4.Sm4CryptCbc(ctx, ivBytes, Convert.FromBase64String(entity.Data));
            return Encoding.UTF8.GetString(decrypted);
        }

        #endregion

        /// <summary>
        /// 产生密钥
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string GenerateKey(string str)
        {
            char[] chars = "0123456789ABCDEF".ToCharArray();
            StringBuilder sb = new StringBuilder("");
            str += "2GOVCms";
            byte[] bs = Encoding.UTF8.GetBytes(str);
            int bit;
            for (int i = 0; i < bs.Length; i++)
            {
                bit = (bs[i] & 0x0f0) >> 4;
                sb.Append(chars[bit]);
                bit = bs[i] & 0x0f;
                sb.Append(chars[bit]);
            }
            if (sb.ToString().Trim().Length > 32)
            {
                return sb.ToString().Trim().Substring(0, 32);
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// 加密类型
        /// </summary>
        public enum Sm4CryptoEnum
        {
            /// <summary>
            /// ECB(电码本模式)
            /// </summary>
            [Description("ECB模式")]
            ECB = 0,

            /// <summary>
            /// CBC(密码分组链接模式)
            /// </summary>
            [Description("CBC模式")]
            CBC = 1
        }
    }
}
