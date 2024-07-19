using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KStypemig.Models.Functions
{
    public class GenerateRandomPassword
    {
        public string GenerateRandomPassword2(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            // Tạo chuỗi ngẫu nhiên bằng cách chọn ngẫu nhiên các ký tự từ chuỗi chars
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}