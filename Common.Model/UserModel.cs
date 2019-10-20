using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string address { get; set; }

        //这样可以设置默认值
        //public DateTime createTime { get; set; } = DateTime.Now;
        public DateTime createTime { get; set; }
    }
}
