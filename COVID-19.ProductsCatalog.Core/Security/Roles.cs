using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19.ProductsCatalog.Core.Security
{
    public enum Roles
    {
        [Name("Admin")]
        Admin,
        [Name("Content Contributors")]
        ContentContributors,
        [Name("Viewers")]
        Viewers
    }

    public class Name : Attribute
    {
        public Name(string value)
        {
            this._value = value;
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public static class EnumExtension
    {
        public static string GetStringValue(this Roles value)
        {
            Type type = value.GetType();
            FieldInfo field = type.GetField(value.ToString());
            Name[] customAttributes = field.GetCustomAttributes(
                 typeof(Name), false) as Name[];
            return customAttributes.Length > 0 ? customAttributes[0].Value : null;
        }
    }
}
