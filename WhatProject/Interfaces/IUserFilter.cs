using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatProject.Interfaces
{
    interface IUserFilter
    {
        void filter(ICollectionView ItemsView, int role);
    }
}
