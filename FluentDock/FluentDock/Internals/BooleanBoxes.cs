// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace FluentDock.Internals
{
    /// <summary>
    /// A helper class to reduce the impact of boxing effect.
    /// </summary>
    internal class BooleanBoxes
    {
        public static object False = false;
        public static object True = true;

        public static object Box(bool value)
        {
            return value ? True : False;
        }
    }
}
