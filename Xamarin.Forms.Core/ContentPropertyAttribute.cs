//
// ContentPropertyAttribute.cs
//
// Author:
//       Stephane Delcroix <stephane@delcroix.org>
//
// Copyright (c) 2013 S. Delcroix
//

using System;

namespace Xamarin.Forms
{
    /// <summary>
    /// Indicates the property of the type that is the (default) content property.
    /// https://developer.xamarin.com/api/type/Xamarin.Forms.ContentPropertyAttribute/
    /// </summary>
    /// <remarks>
    /// XAML processor uses to determine the content property.
    /// Decorating types with ContentPropertyAttribute allows shorter XAML syntax. 
    /// As ContentView has a ContentProperty attribute applied, this XAML is valid
    /// </remarks>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ContentPropertyAttribute : Attribute
	{
		internal static string[] ContentPropertyTypes = { "Xamarin.Forms.ContentPropertyAttribute", "System.Windows.Markup.ContentPropertyAttribute" };

		public ContentPropertyAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
	}
}