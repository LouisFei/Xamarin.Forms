//
// Tweener.cs
//
// Author:
//       Jason Smith <jason.smith@xamarin.com>
//
// Copyright (c) 2012 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace Xamarin.Forms
{
    /// <summary>
    /// Functions that modify values non-linearly, generally used for animations.
    /// 缓动，非线性地修改值的方法，通常用于动画。
    /// </summary>
    /// <remarks>
    /// https://developer.xamarin.com/api/type/Xamarin.Forms.Easing/
    /// Easing functions are applied to input values in the range [0,1]. 
    /// The cubic easing functions are often considered to look most natural.
    /// If developers wish to use their own easing functions, 
    /// they should return a value of 0 for an input of 0 and a value of 1 for an input of 1 or the animation will have a jump.
    /// </remarks>
	public class Easing
	{
        /// <summary>
        /// Linear transformation.
        /// 线性的，匀速。
        /// </summary>
		public static readonly Easing Linear = new Easing(x => x);

        /// <summary>
        /// Smoothly decelerates.
        /// 平滑地减速。
        /// </summary>
		public static readonly Easing SinOut = new Easing(x => Math.Sin(x * Math.PI * 0.5f));
        /// <summary>
        /// Smoothly accelerates.
        /// 平滑地加速。
        /// </summary>
		public static readonly Easing SinIn = new Easing(x => 1.0f - Math.Cos(x * Math.PI * 0.5f));
        /// <summary>
        /// Accelerates in and out.
        /// 平滑加速，再平滑减速。
        /// </summary>
		public static readonly Easing SinInOut = new Easing(x => -Math.Cos(Math.PI * x) / 2.0f + 0.5f);

        /// <summary>
        /// Starts slowly and accelerates.
        /// 开始时候很慢，并且在加速
        /// </summary>
		public static readonly Easing CubicIn = new Easing(x => x * x * x);
        /// <summary>
        /// Starts quickly and the decelerates.
        /// 开始时候很快，并且在减速。
        /// </summary>
		public static readonly Easing CubicOut = new Easing(x => Math.Pow(x - 1.0f, 3.0f) + 1.0f);

        /// <summary>
        /// Accelerates and decelerates. Often a natural-looking choice.
        /// 开始时候也很慢，先加速，再减速。通常是一个很自然的选择。
        /// </summary>
		public static readonly Easing CubicInOut = new Easing(x => x < 0.5f ? Math.Pow(x * 2.0f, 3.0f) / 2.0f : (Math.Pow((x - 1) * 2.0f, 3.0f) + 2.0f) / 2.0f);

        /// <summary>
        /// Leaps to final values, bounces 3 times, and settles.
        /// 跳跃到指定值，弹跳3次，然后静止。
        /// </summary>
		public static readonly Easing BounceOut;

        /// <summary>
        /// Jumps towards, and then bounces as it settles at the final value.
        /// 向前上下弹，越弹越低，最终静止下来。
        /// </summary>
		public static readonly Easing BounceIn;

        /// <summary>
        /// Moves away and then leaps toward the final value.
        /// 反弹再加速。
        /// </summary>
		public static readonly Easing SpringIn = new Easing(x => x * x * ((1.70158f + 1) * x - 1.70158f));
        /// <summary>
        /// Overshoots and then returns.
        /// 加速，超越后返回。
        /// </summary>
		public static readonly Easing SpringOut = new Easing(x => (x - 1) * (x - 1) * ((1.70158f + 1) * (x - 1) + 1.70158f) + 1);

		readonly Func<double, double> _easingFunc;

		static Easing()
		{
			BounceOut = new Easing(p =>
			{
				if (p < 1 / 2.75f)
				{
					return 7.5625f * p * p;
				}
				if (p < 2 / 2.75f)
				{
					p -= 1.5f / 2.75f;

					return 7.5625f * p * p + .75f;
				}
				if (p < 2.5f / 2.75f)
				{
					p -= 2.25f / 2.75f;

					return 7.5625f * p * p + .9375f;
				}
				p -= 2.625f / 2.75f;

				return 7.5625f * p * p + .984375f;
			});

			BounceIn = new Easing(p => 1.0f - BounceOut.Ease(1 - p));
		}

		public Easing(Func<double, double> easingFunc)
		{
			if (easingFunc == null)
				throw new ArgumentNullException("easingFunc");

			_easingFunc = easingFunc;
		}

		public double Ease(double v)
		{
			return _easingFunc(v);
		}

		public static implicit operator Easing(Func<double, double> func)
		{
			return new Easing(func);
		}
	}
}