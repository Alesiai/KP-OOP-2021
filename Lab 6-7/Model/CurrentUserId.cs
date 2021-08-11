using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6_7.Model
{
    public class CurrentUserId
    {
        private static CurrentUserId instance;

        public int UserId { get; set;}
        public string UserName { get; set; }
        protected CurrentUserId(int userId, string userName) 
        {
            this.UserId = userId;
            this.UserName = userName;
        }

        public static CurrentUserId getInstance(int userId, string userName) 
        {
            if (instance == null) instance = new CurrentUserId(userId, userName);
            return instance;
        }
    }
}
