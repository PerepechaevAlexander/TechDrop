﻿namespace TechDrop.Data;

internal static class FileConverter
{
    internal static byte[] GetBinaryFile(string filename)
    {
        byte[] bytes;
        using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
        {
            bytes = new byte[file.Length];
            file.Read(bytes, 0, (int)file.Length);
        }
        return bytes;
    }
}