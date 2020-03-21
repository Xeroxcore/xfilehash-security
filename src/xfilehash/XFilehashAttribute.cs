using System;

namespace xfilehash
{
    public class XFilehashAttribute : Attribute
    {
        private string Filename { get; set; }
        private XFileHasher FileHash { get; set; }
        public XFilehashAttribute(string filename)
        {
            if (!Validation.ObjectIsNull(filename))
                Filename = filename;
        }
    }
}