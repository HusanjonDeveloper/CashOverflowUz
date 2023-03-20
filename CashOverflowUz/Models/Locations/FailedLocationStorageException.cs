//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Locations
{
    public class FailedLocationStorageException:Xeption
    {
        public FailedLocationStorageException(Exception innerException)
        :base(message:"Faild location storage exception  occured, contact support",
             innerException)
        {}
    }
}
