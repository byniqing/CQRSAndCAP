using System;
using System.Collections.Generic;
using System.Text;
using UserRepository.Events;

namespace UserRepository.Model
{
    public class User : Entity
    {
        //public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string address { get; set; }

        //这样可以设置默认值
        //public DateTime createTime { get; set; } = DateTime.Now;
        public DateTime createTime { get; set; }

        //public User()
        //{
        //    //name = "测试";
        //    //AddCreateUserDomainEvent();
        //}

        public void AddCreateUserDomainEvent()
        {
            var userDomainEvent = new UserDomainEvent(this, "创建用户发送领域事件");
            this.AddDomainEvent(userDomainEvent);
        }
    }
}
