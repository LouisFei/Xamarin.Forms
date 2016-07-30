using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
    /// <summary>
    /// A visual element that is used to place layouts and controls on the screen.
    /// 一个可视化元素通常用来在屏幕上放置布局和控件。
    /// </summary>
    /// <remarks>
    /// https://developer.xamarin.com/api/type/Xamarin.Forms.View/
    /// The View class is a base class for the Layout class, and most of the controls that application developers will use. 
    /// View类作为布局类和开发者使用的大多数控件的基类。
    /// Nearly all UI elements that an application developer will use are derived from View class. 
    /// 开发者将使用的几乎所有的UI元素都继承自View类。
    /// Because the View class ultimately inherits from BindableObject class, application developers can use the Model-View-ViewModel architecture, 
    /// 因为View类最终继承自BindableObject类，开发者可以使用MVVM构架。
    /// as well as XAML, to develop portable user interfaces. 
    /// 和XAML一样，可以开发可移植的用户界面。
    /// </remarks>
	public class View : VisualElement, IViewController
	{
		public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create("VerticalOptions", typeof(LayoutOptions), typeof(View), LayoutOptions.Fill,
			propertyChanged: (bindable, oldvalue, newvalue) => ((View)bindable).InvalidateMeasureInternal(InvalidationTrigger.VerticalOptionsChanged));

		public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create("HorizontalOptions", typeof(LayoutOptions), typeof(View), LayoutOptions.Fill,
			propertyChanged: (bindable, oldvalue, newvalue) => ((View)bindable).InvalidateMeasureInternal(InvalidationTrigger.HorizontalOptionsChanged));

		public static readonly BindableProperty MarginProperty = BindableProperty.Create("Margin", typeof(Thickness), typeof(View), default(Thickness), propertyChanged: MarginPropertyChanged);

		readonly ObservableCollection<IGestureRecognizer> _gestureRecognizers = new ObservableCollection<IGestureRecognizer>();

		protected internal View()
		{
			_gestureRecognizers.CollectionChanged += (sender, args) =>
			{
				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						foreach (IElement item in args.NewItems.OfType<IElement>())
						{
							ValidateGesture(item as IGestureRecognizer);
							item.Parent = this;
						}
						break;
					case NotifyCollectionChangedAction.Remove:
						foreach (IElement item in args.OldItems.OfType<IElement>())
							item.Parent = null;
						break;
					case NotifyCollectionChangedAction.Replace:
						foreach (IElement item in args.NewItems.OfType<IElement>())
						{
							ValidateGesture(item as IGestureRecognizer);
							item.Parent = this;
						}
						foreach (IElement item in args.OldItems.OfType<IElement>())
							item.Parent = null;
						break;
					case NotifyCollectionChangedAction.Reset:
						foreach (IElement item in _gestureRecognizers.OfType<IElement>())
							item.Parent = this;
						break;
				}
			};
		}

		public IList<IGestureRecognizer> GestureRecognizers
		{
			get { return _gestureRecognizers; }
		}

		public LayoutOptions HorizontalOptions
		{
			get { return (LayoutOptions)GetValue(HorizontalOptionsProperty); }
			set { SetValue(HorizontalOptionsProperty, value); }
		}

        /// <summary>
        ///  Gets or sets the margin for the view.
        /// </summary>
		public Thickness Margin
		{
			get { return (Thickness)GetValue(MarginProperty); }
			set { SetValue(MarginProperty, value); }
		}

		public LayoutOptions VerticalOptions
		{
			get { return (LayoutOptions)GetValue(VerticalOptionsProperty); }
			set { SetValue(VerticalOptionsProperty, value); }
		}

		protected override void OnBindingContextChanged()
		{
			var gotBindingContext = false;
			object bc = null;

			for (var i = 0; i < GestureRecognizers.Count; i++)
			{
				var bo = GestureRecognizers[i] as BindableObject;
				if (bo == null)
					continue;

				if (!gotBindingContext)
				{
					bc = BindingContext;
					gotBindingContext = true;
				}

				SetInheritedBindingContext(bo, bc);
			}

			base.OnBindingContextChanged();
		}

		static void MarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((View)bindable).InvalidateMeasureInternal(InvalidationTrigger.MarginChanged);
		}

		void ValidateGesture(IGestureRecognizer gesture)
		{
			if (gesture == null)
				return;
			if (gesture is PinchGestureRecognizer && _gestureRecognizers.GetGesturesFor<PinchGestureRecognizer>().Count() > 1)
				throw new InvalidOperationException($"Only one {nameof(PinchGestureRecognizer)} per view is allowed");
		}
	}
}