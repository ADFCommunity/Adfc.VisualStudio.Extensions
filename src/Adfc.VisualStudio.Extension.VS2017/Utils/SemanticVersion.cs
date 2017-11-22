using System;
using System.Text.RegularExpressions;

namespace Adfc.VisualStudio.Extension.VS2017.Utils
{
    public class SemanticVersion : IComparable<SemanticVersion>
    {
        public uint Major { get; }
        public uint Minor { get; }
        public uint Patch { get; }
        public string PreRelease { get; }
        public string Build { get; }

        private static readonly Regex Regex = new Regex(@"^(?<major>\d)\.(?<minor>\d)\.(?<patch>\d)(-(?<pre>[a-zA-Z0-9-]+))?(\+(?<build>[a-zA-Z0-9-]+))?$");

        public SemanticVersion(uint major, uint minor, uint patch, string preRelease, string build)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            PreRelease = preRelease ?? string.Empty;
            Build = build ?? string.Empty;
        }

        public static bool TryParse(string input, out SemanticVersion version)
        {
            version = null;

            var result = Regex.Match(input);
            if (!result.Success)
            {
                return false;
            }

            uint major = uint.Parse(result.Groups["major"].Value);
            uint minor = uint.Parse(result.Groups["minor"].Value);
            uint patch = uint.Parse(result.Groups["patch"].Value);
            string preRelease = result.Groups["pre"].Value;
            string build = result.Groups["build"].Value;

            version = new SemanticVersion(major, minor, patch, preRelease, build);
            return true;
        }

        public int CompareTo(SemanticVersion other)
        {
            var result = Major.CompareTo(other.Major);
            if (result != 0) { return result; }

            result = Minor.CompareTo(other.Minor);
            if (result != 0) { return result; }

            result = Patch.CompareTo(other.Patch);
            if (result != 0) { return result; }

            result = PreRelease.CompareTo(other.PreRelease);
            if (result != 0) { return result; }

            return Build.CompareTo(other.Build);
        }

        public static bool operator >(SemanticVersion left, SemanticVersion right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(SemanticVersion left, SemanticVersion right)
        {
            return left.CompareTo(right) < 0;
        }
    }
}
