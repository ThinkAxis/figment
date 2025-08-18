using System;
using System.IO;
using System.IO.Hashing;

namespace figment
{
    public class Hashing
    { 
        public static string ComputeFileHash(string filePath, int bufferSize)
        {
            var hasher = new XxHash128();
            using var stream = File.OpenRead(filePath);

            hasher.Append(stream);

            Span<byte> hashBytes = stackalloc byte[16];
            hasher.GetHashAndReset(hashBytes);

            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

    }
}