using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class MessageModel
    {
        public int id { get; set; }
        public string msg { get; set; }
        public DateTime createTime { get; set; } = DateTime.Now;
    }
}
