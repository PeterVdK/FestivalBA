﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFestivalProject.viewmodel
{
    class HomeVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Home"; }
        }
    }
}
