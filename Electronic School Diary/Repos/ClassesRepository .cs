﻿using ElectronicSchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicSchoolDiary.Repos
{
    class ClassesRepository
    {
        private static SqlCeConnection Connection = DataBaseConnection.Instance.Connection;


        public static string GetQuery()
        {
            string query;
            query = @"SELECT Number FROM Classes";
            return query;

        }


    }
}
