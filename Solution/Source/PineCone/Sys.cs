﻿using System;
using NCore;

namespace PineCone
{
    public static class Sys
    {
        public static readonly IFormatting Formatting = new DefaultFormatting();

        public static readonly IStringConverter StringConverter = new StringConverter(Formatting);

        public static readonly StringComparer StringComparer = StringComparer.InvariantCultureIgnoreCase;

        public static DateTime Now()
        {
            return DateTime.Now;
        }
    }
}