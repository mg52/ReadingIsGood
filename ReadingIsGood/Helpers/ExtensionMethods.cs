using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Helpers
{
    public static class ExtensionMethods
    {
        public static CustomerDto WithoutPassword(this CustomerDto user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }

        public static Customer WithoutPassword(this Customer user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}
