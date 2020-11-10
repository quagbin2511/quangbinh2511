using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models;

namespace WebBanHang.BLL
{
    public class UserBusinessLayer
    {
        public bool IsValidUser(LoginViewModel user)
        {

            if (user.Email != "admin@gmail.com")
                return true;
            else
                return false;
            
            
        }

    }
}