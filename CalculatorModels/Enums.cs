﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Learning.Calculator.Models
{
    public enum Operation
    {
        Unknown = 0,
        [Display(Name = "+")] Plus,
        [Display(Name = "-")] Minus,
        [Display(Name = "*")] Multiply,
        [Display(Name = "/")] Divide,
        [Display(Name = "=")] Equal,
        [Display(Name = "(")] LeftParenthesis,
        [Display(Name = ")")] RightParenthesis,
    }

    public enum TokenType
    {
        NumberToken,
        OperationToken,
    }

    internal static class Extensions
    {
        internal static string Name(this Operation enumValue)
        {
            return enumValue.GetAttribute<DisplayAttribute>().Name;
        }
        internal static TAttribute GetAttribute<TAttribute>(this Operation enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }
        internal static Operation GetValueFromName(string name)
        {
            var type = typeof (Operation);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof (DisplayAttribute)) as DisplayAttribute;
                if (attribute != null)
                {
                    if (attribute.Name == name)
                    {
                        return (Operation) field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == name)
                        return (Operation) field.GetValue(null);
                }
            }
            return Operation.Unknown;
        }
    }
}
