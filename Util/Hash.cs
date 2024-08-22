using System.Text;

namespace DesafioSenaiCimatec.Util
{
    public class Hash
    {
        #region MD5

        /// <summary>
        /// Hash MD5 de um Conteúdo em String
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String MD5(String input)
        {
            return MD5(UTF8Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash MD5 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String MD5(Byte[] input)
        {
            using (System.Security.Cryptography.MD5 hash =
                System.Security.Cryptography.MD5.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(input))
                    .Replace("-", "").ToLower();
            }
        }

        #endregion

        #region SHA-1

        /// <summary>
        /// Hash SHA-1 de um Conteúdo em String
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String SHA1(String input)
        {
            return SHA1(UTF8Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash SHA-1 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String SHA1(Byte[] input)
        {
            using (System.Security.Cryptography.SHA1 hash =
                System.Security.Cryptography.SHA1.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(input))
                    .Replace("-", "").ToLower();
            }
        }

        #endregion

        #region SHA-256

        /// <summary>
        /// Hash SHA-256 de um Conteúdo em String
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String SHA256(String input)
        {
            return SHA256(UTF8Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash SHA-256 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String SHA256(Byte[] input)
        {
            using (System.Security.Cryptography.SHA256 hash =
                System.Security.Cryptography.SHA256.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(input))
                    .Replace("-", "").ToLower();
            }
        }

        #endregion

        #region SHA-512

        /// <summary>
        /// Hash SHA-512 de um Conteúdo em String
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String SHA512(String input)
        {
            return SHA512(UTF8Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Hash SHA-512 de um Conteúdo em Array de Bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Retorna o Hash em String Hexadecimal</returns>
        public static String SHA512(Byte[] input)
        {
            using (System.Security.Cryptography.SHA512 hash =
                System.Security.Cryptography.SHA512.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(input))
                    .Replace("-", "").ToLower();
            }
        }

        #endregion
    }
}
