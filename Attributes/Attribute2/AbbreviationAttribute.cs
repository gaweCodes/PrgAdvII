using System;
using System.Reflection;

namespace Attributes.Attribute2
{
    public class AbbreviationAttribute : Attribute
    {
        public AbbreviationAttribute(string text) => Text = text;
        public string Text { get; }
    }
}
