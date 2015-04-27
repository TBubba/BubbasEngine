using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace BubbasEngine.Engine
{
    public static class TypeConverter
    {
        //
        public static byte BoolsToByte(bool[] bools)
        {
            int length = bools.Length;

            // Correct length
            if (length != 8)
                throw new Exception("incorrect length of bool array (has to be 8)");

            // Convert
            byte val = 0;
            for (int i = 0; i < length; i++)
            {
                val <<= 1;
                if (bools[i])
                    val |= 1;
            }

            // Return
            return val;
        }
        public static byte[] BoolsToBytes(bool[] bools)
        {
            int boolsLength = bools.Length;
            int bytesLength = boolsLength / 8;

            // Correct length
            if (boolsLength % 8 != 0)
                throw new Exception("incorrect length of bool array (has to be a multiple of 8)");

            // Convert all
            byte[] bytes = new byte[bytesLength];
            for (int i = 0; i < bytesLength; i++)
            {
                // Convert bool to byte
                int cur = i * 8;
                byte val = 0;
                for (int j = 0; j < 8; j++)
                {
                    val <<= 1;
                    if (bools[cur + j])
                        val |= 1;
                }

                bytes[i] = val;
            }

            // Return
            return bytes;
        }

        public static bool[] ByteToBools(byte b)
        {
            // Convert
            bool[] bools = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                bools[7 - i] = (b & 1) == 1;
                b >>= 1;
            }

            // Return
            return bools;
        }
        public static bool[] BytesToBools(byte[] bytes)
        {
            int bytesLength = bytes.Length;
            int boolsLength = bytesLength * 8;

            // Convert all
            bool[] bools = new bool[boolsLength];
            for (int i = 0; i < bytesLength; i++)
            {
                // Convert
                byte val = bytes[i];
                int cur = i * 8;
                for (int j = 0; j < 8; j++)
                {
                    bools[cur + 7 - j] = (val & 1) == 1;
                    val >>= 1;
                }
            }

            // Return
            return bools;
        }

        //
    }
}
