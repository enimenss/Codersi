using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders
{
    public class Enums
    {
        public enum Cities : int
        {
            Nis,
            Beograd,
            London
        }

        public enum Types : byte
        {
            All,
            Museums,
            Monuments,
            Sights,
            Taverns,
            Clubs,
            Pubs,
        }
    }
}