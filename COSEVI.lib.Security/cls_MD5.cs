using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace COSEVI.lib.Security
{
    public static class cls_MD5
    {

        /// <summary>
        /// Genera el MD5 de una string.
        /// </summary>
        /// <param name="ps_source">String con el valor a obtener.</param>
        /// <returns>String con el MD5</returns>
        public static string GetMd5Hash(string ps_source)
        {
            StringBuilder sBuilder = new StringBuilder();

            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(ps_source));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        /// <summary>
        /// Obtiene la contraseña según se almacena en la base de datos.
        /// </summary>
        /// <param name="ps_user">String usuario.</param>
        /// <param name="ps_pass">String password</param>
        /// <returns>String con contraseña</returns>
        public static string GetPassword(string ps_user, string ps_pass)
        {
            string pass = GetMd5Hash(ps_user);

            pass = GetMd5Hash(pass + ps_pass);

            return pass;

        }

    }
}
