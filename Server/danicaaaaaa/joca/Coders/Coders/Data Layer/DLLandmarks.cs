using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Data_Layer
{
    public class DLLandmarks
    {
        private static DLLandmarks data;
        CodersEntities db;

        private DLLandmarks() { }

        public static DLLandmarks Data
        {
            get
            {
                if (data == null)
                {
                    data = new DLLandmarks();
                }
                return data;
            }
        }
    }
}