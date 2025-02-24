﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensInfo1
{
    public class Employee
    {
        
        
            public int IDNum { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Position { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public byte[] QRLogin { get; set; }

            public int EmployeeIdentityNumber { get; set; }
            //public Bitmap QRCodeImage { get; set; } // Property to hold the image


    }
    public class EmployeeData {
        public static LensInfo1.EmployeeData _instance;
        public static LensInfo1.EmployeeData Instance => _instance ??= new LensInfo1.EmployeeData();

        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeeData()
        {
            Employees = new ObservableCollection<Employee>();
        }
    }
}
