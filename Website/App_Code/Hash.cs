using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Website.App_Code
{
    public class Hash
    {
        private string text;

        public Hash(string text)
        {
            setText(text);
        }

        public string getHash()
        {
            string passw = getText();
            //this is where the text is rehashed (same process as when user first entered the password during signup)
            using (SHA256 hasher = SHA256.Create())
            {
                //get the text in bytes, hash it, and put the result into a var (this process might not be 100% accurately explained, but that's what I got)
                byte[] byt = hasher.ComputeHash(Encoding.UTF8.GetBytes(passw));

                //Rebuild the hashed text in a stringbuilder object
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < byt.Length; i++)
                {
                    builder.Append(byt[i].ToString("x2"));
                }
                passw = builder.ToString();
            }
            return passw;
        }
        public void setText(string i)
        {
            this.text = i;
        }
        public string getText()
        {
            return this.text;
        }

    }
}